using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Physical
{
    public class BasicMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private float speedExposer;
        [SerializeField] private float jumpHeight;
        [SerializeField] public float gravity; 
        [SerializeField] private Transform groundCheck;
        [SerializeField] private Transform rightCheck;
        [SerializeField] private Transform leftCheck;
        [SerializeField] private Transform frontCheck;
        [SerializeField] private Transform player;
        [SerializeField] private Transform spawner;
        [SerializeField] private float groundDistance;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private LayerMask plateformMask;
        [SerializeField] private LayerMask wallMask;
        [SerializeField] private LayerMask climableMask;
        [SerializeField] private ScriptExposer se;
    
        private Vector3 _velocity;
        private float _sideDistance = 0.3f;
        private float _minusVelocity = -2f;
        private float _speed;
        public bool isGroundCheck;
        private bool _isPlateformCheck;
        private bool _isWallCheck;
        private bool _isWallLeft;
        private bool _isClimbing = false;
        private Vector3 _groundCheckPosition;
        public bool isMoving;
        public bool canClimb;
        public int currentLevel;

        private Vector3 _finalClimbedPos;
        private float _interp = 0.0f;

        private bool _isSliding;
        
        public float slideSpeed = 20; // slide speed
        private Vector3 slideForward; // direction of slide
        private float slideTimer = 0.0f;
        public float slideTimerMax = 1.0f;
        private float x;
        private float z;

        private void Start()
        {
            isMoving = false;
            _speed = se.tweakerDatas.DSE.Character.baseSpeed;

        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                se.isInMenu = !se.isInMenu;
            }

            
            if (se.isInMenu)
            {
                Cursor.lockState = CursorLockMode.None;
                return;
            }
            speedExposer = se.tweakerDatas.DSE.Character.baseSpeed;
            Cursor.lockState = CursorLockMode.Locked;

            if (player.position.y <= -50f)
            {
                player.position = spawner.position;
                return;
            }
            
            if (_isClimbing)
            {
                _interp += Time.deltaTime * 10;
                player.position = Vector3.Lerp(player.position,
                    _finalClimbedPos, _interp);

                Debug.Log(_interp);
                if (_interp >= 1.0f)
                {
                    _interp = 0.0f;
                    _isClimbing = false;
                }
                
                return;
            }
            
            if (canClimb && Input.GetKeyDown(KeyCode.A) && !_isClimbing)
            {
                Debug.Log("Climbing");
                _finalClimbedPos = new Vector3(player.position.x, player.position.y + 1.0f, player.position.z);
                _isClimbing = true;
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
            
            
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");

            Vector3 movement = transform.right * x + transform.forward * z;
            
            if (Input.GetKey(KeyCode.LeftShift) && isGroundCheck)
            {
                _speed = speedExposer * se.tweakerDatas.DSE.Character.sprintSpeed;
            }
            else
            {
                _speed = speedExposer;
            }
            controller.Move(movement * (_speed * Time.deltaTime));
            if (Input.GetButtonDown("Jump") && isGroundCheck)
            {
                Debug.Log("ZEBI");
                if (gravity > 0)
                {
                    _velocity.y = -
                        Mathf.Sqrt(-se.tweakerDatas.DSE.Character.heightJump * (-_speed - 1) * gravity);
                    Debug.Log(Mathf.Sqrt(-se.tweakerDatas.DSE.Character.heightJump * (-_speed - 1) * gravity));
                }
                else
                {
                    _velocity.y = Mathf.Sqrt(se.tweakerDatas.DSE.Character.heightJump * (-_speed - 1) * gravity);
                    Debug.Log(Mathf.Sqrt(se.tweakerDatas.DSE.Character.heightJump * (-_speed - 1) * gravity));
                }
            }
            if (Input.GetButton("WallJump") && _isWallCheck)
            {
                _velocity.y -= gravity * Time.deltaTime * 10;
            }
            _velocity.y += gravity * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.C) && !_isSliding && (isGroundCheck || _isPlateformCheck)) // press C to slide
            {
                slideTimer = 0f;
                _isSliding = true;
                slideForward = new Vector3(movement.x, gravity * Time.deltaTime, movement.z);
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                GetComponent<SphereCollider>().radius = 0.25f;
            }
            
            
            controller.Move(_velocity * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (_isSliding && (isGroundCheck || _isPlateformCheck))
            {
                Debug.Log(slideTimer);
                _speed = slideSpeed;
                _velocity = slideForward * _speed;
         
                slideTimer += Time.deltaTime;
            }
            else
            {
                Vector3 movement = transform.right * x + transform.forward * z;
                _velocity.x = movement.x;
                _velocity.z = movement.z;
                _velocity.y += gravity * Time.deltaTime;
                transform.localScale = new Vector3(1f, 1f, 1f);
                GetComponent<SphereCollider>().radius = 0.5f;
            }
            if (slideTimer >= slideTimerMax || (!isGroundCheck || !_isPlateformCheck))
            {
                _isSliding = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "EndLevel")
            {
                Debug.Log("Collide");
                switch (currentLevel)
                {
                    case 1:
                        SceneManager.LoadScene("Level2");
                        break;
                    case 2:
                        SceneManager.LoadScene("Level3");
                        break;
                    case 3:
                        SceneManager.LoadScene("Level4");
                        break;
                    case 4:
                        SceneManager.LoadScene("Level5");
                        break;
                    case 5:
                        SceneManager.LoadScene("Level1");
                        break;
                }
            }
        }

        private void CheckCollision()
        {
            _groundCheckPosition = groundCheck.position;
            isGroundCheck = Physics.CheckSphere(_groundCheckPosition, groundDistance, groundMask);
            canClimb = Physics.CheckSphere(frontCheck.position, _sideDistance, climableMask);
            _isPlateformCheck = Physics.CheckSphere(_groundCheckPosition, groundDistance, groundMask);
            if (Physics.CheckSphere(rightCheck.position, _sideDistance, wallMask) ||
                Physics.CheckSphere(leftCheck.position, _sideDistance, wallMask))
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
