using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Physical
{
    public class BasicMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private float speedExposer;
        [SerializeField] private float jumpHeight;
        [SerializeField] private float gravity; 
        [SerializeField] private Transform groundCheck;
        [SerializeField] private Transform rightCheck;
        [SerializeField] private Transform leftCheck;
        [SerializeField] private Transform player;
        [SerializeField] private Transform spawner;
        [SerializeField] private float groundDistance;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private LayerMask plateformMask;
        [SerializeField] private LayerMask wallMask;
        [SerializeField] private ScriptExposer se;
    
        private Vector3 _velocity;
        private float _sideDistance = 0.3f;
        private float _minusVelocity = -2f;
        private float _speed;
        public bool isGroundCheck;
        private bool _isPlateformCheck;
        private bool _isWallCheck;
        private bool _isWallLeft;
        private Vector3 _groundCheckPosition;
        public bool isMoving;

        private void Start()
        {
            isMoving = false;
            _speed = se.tweakerDatas.DSE.Character.baseSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            speedExposer = se.tweakerDatas.DSE.Character.baseSpeed;
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                se.isInMenu = !se.isInMenu;
            }

            if (se.isInMenu)
            {
                Cursor.lockState = CursorLockMode.None;
                return;
            }
            Cursor.lockState = CursorLockMode.Locked;

            if (player.position.y <= -50f)
            {
                player.position = spawner.position;
                return;
            }
            
            if (!Input.GetAxis("Horizontal").Equals(0f) || Input.GetAxis("Vertical") > 0)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

            CheckCollision();

            if ((isGroundCheck || _isPlateformCheck) && _velocity.y < 0f)
            {
                _velocity.y = _minusVelocity;
            }
            
        
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 movement = transform.right * x + transform.forward * z;
            if (Input.GetButton("Run") && isGroundCheck)
            {
                _speed = speedExposer * se.tweakerDatas.DSE.Character.sprintSpeed;
            }
            else
            {
                _speed = speedExposer;
            }
            controller.Move(movement * (_speed * Time.deltaTime));
            if (Input.GetButtonDown("Jump") && (isGroundCheck || _isPlateformCheck))
            {
                _velocity.y = Mathf.Sqrt(se.tweakerDatas.DSE.Character.heightJump * (-_speed - 1) * gravity);
            }
            if (Input.GetButton("WallJump") && _isWallCheck)
            {
                _velocity.y -= gravity * Time.deltaTime * 10;
            }
            _velocity.y += gravity * Time.deltaTime;
            
            controller.Move(_velocity * Time.deltaTime);
        }

        private void CheckCollision()
        {
            _groundCheckPosition = groundCheck.position;
            isGroundCheck = Physics.CheckSphere(_groundCheckPosition, groundDistance, groundMask);
            _isPlateformCheck = Physics.CheckSphere(_groundCheckPosition, groundDistance, plateformMask);
            if (Physics.CheckSphere(rightCheck.position, _sideDistance, wallMask) || Physics.CheckSphere(leftCheck.position, _sideDistance, wallMask))
            {
                _isWallLeft = Physics.CheckSphere(leftCheck.position, _sideDistance, wallMask);
                _isWallCheck = true;
            }
            else
            {
                _isWallCheck = false;
            }
        }
    }
}
