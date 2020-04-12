using UnityEngine;

namespace Physical
{
    public class BasicMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private float speed;
        [SerializeField] private float jumpHeight;
        [SerializeField] private float gravity; 
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundDistance;
        [SerializeField] private LayerMask groundMask;
    
        private Vector3 _velocity;
        private float _minusVelocity = -2f;
        private bool _isGroundCheck;

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
            controller.Move(movement * (speed * Time.deltaTime));
            if (Input.GetButtonDown("Jump") && _isGroundCheck)
            {
                Debug.Log("Je saute zebi");
                _velocity.y = Mathf.Sqrt(jumpHeight * _minusVelocity * gravity);
            }
            _velocity.y += gravity * Time.deltaTime;

            controller.Move(_velocity * Time.deltaTime);
        }
    }
}
