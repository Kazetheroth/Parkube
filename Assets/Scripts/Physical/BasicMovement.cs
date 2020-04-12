using System;
using UnityEngine;

namespace Physical
{
    public class BasicMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private float speedExposer;
        [SerializeField] private float jumpHeight;
        [SerializeField] private float gravity; 
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundDistance;
        [SerializeField] private LayerMask groundMask;
    
        private Vector3 _velocity;
        private float _minusVelocity = -2f;
        private float _speed;
        private bool _isGroundCheck;

        private void Start()
        {
            _speed = speedExposer;
        }

        // Update is called once per frame
        void Update()
        {
            _isGroundCheck = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (_isGroundCheck && _velocity.y < 0f)
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
            if (Input.GetButtonDown("Jump") && _isGroundCheck)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * (_minusVelocity - 1) * gravity);
            }
            _velocity.y += gravity * Time.deltaTime;
            controller.Move(_velocity * Time.deltaTime);
        }
    }
}
