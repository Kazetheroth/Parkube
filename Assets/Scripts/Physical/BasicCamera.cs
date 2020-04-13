using UnityEngine;

namespace Physical
{
    public class BasicCamera : MonoBehaviour
    {
        [SerializeField] private float mouseSensitivity;
        [SerializeField] private Transform player;

        private float _xRotation;
        private float _yRotation;
        private float _mouseX;
        private float _mouseY;
        private float _padAxisX;
        private float _padAxisY;
        private float _xPadRotation;
        private float _yPadRotation;
    
        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update()
        {
            _mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            _mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            _padAxisX = Input.GetAxis("HorizontalR");
            _padAxisY = Input.GetAxis("VerticalR");
            
            //Debug.Log(_padAxisX + ", " + _padAxisY);
            
            _xRotation -= _mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            
            _xPadRotation -= _padAxisY;
            _xPadRotation = Mathf.Clamp(_padAxisX, -90f, 90f);

            _yRotation += _mouseX;

            transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
            player.Rotate(Vector3.up * _mouseX);
            player.Rotate(_xPadRotation, 0,0);
        }
    }
}
