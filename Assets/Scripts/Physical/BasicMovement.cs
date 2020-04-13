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
        [SerializeField] private float groundDistance;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private LayerMask plateformMask;
        [SerializeField] private LayerMask wallMask;
    
        private Vector3 _velocity;
        private float _sideDistance = 0.1f;
        private float _minusVelocity = -2f;
        private float _speed;
        private bool _isGroundCheck;
        private bool _isPlateformCheck;
        private bool _isWallCheck;
        private bool _isWallLeft;
        private Vector3 _groundCheckPosition;
        public bool isMoving;

        private void Start()
        {
            isMoving = false;
            _speed = speedExposer;
        }

        // Update is called once per frame
        void Update()
        {
            if (!Input.GetAxis("Horizontal").Equals(0f) || Input.GetAxis("Vertical") > 0)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

            CheckCollision();

            if ((_isGroundCheck || _isPlateformCheck) && _velocity.y < 0f)
            {
                _velocity.y = _minusVelocity;
            }
            
        
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 movement = transform.right * x + transform.forward * z;
            if (Input.GetButton("Run") && _isGroundCheck)
            {
                _speed = speedExposer * 2f;
            }
            else
            {
                _speed = speedExposer;
            }
            controller.Move(movement * (_speed * Time.deltaTime));
            if (Input.GetButtonDown("Jump") && (_isGroundCheck || _isPlateformCheck))
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * (_minusVelocity - 1) * gravity);
            }
            if (Input.GetButton("WallJump") && _isWallCheck)
            {
                if (_isWallLeft)
                {
                    Vector3 wallJumpMove = transform.right * 300 + transform.forward * 300;
                    controller.Move(wallJumpMove * (_speed * Time.deltaTime));
                }
            }
            _velocity.y += gravity * Time.deltaTime;
            
            controller.Move(_velocity * Time.deltaTime);
        }

        private void CheckCollision()
        {
            _groundCheckPosition = groundCheck.position;
            _isGroundCheck = Physics.CheckSphere(_groundCheckPosition, groundDistance, groundMask);
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
