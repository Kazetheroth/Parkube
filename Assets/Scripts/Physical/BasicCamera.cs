using UnityEngine;

namespace Physical
{
    public class BasicCamera : MonoBehaviour
    {
        [SerializeField] private float mouseSensitivity;
        [SerializeField] private Transform player;
        [SerializeField] private ScriptExposer se;
        
        [HideInInspector] public Vector3 gravityRotation = new Vector3(0.0f, 0.0f, 0.0f); 

        private float _xRotation;
        private float _yRotation;
        private float _mouseX;
        private float _mouseY;
        private float _padAxisX;
        private float _padAxisY;
        private float _xPadRotation;
        private float _yPadRotation;
        
        //Sens de la rotation
        public int direction = 1;
    
        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            
        }

        // Update is called once per frame
        void Update()
        {
            if (se.isInMenu)
            {
                return;
            }
            _mouseX = (Input.GetAxis("Mouse X") * direction) * mouseSensitivity * Time.deltaTime;
            _mouseY = (Input.GetAxis("Mouse Y") * direction) * mouseSensitivity * Time.deltaTime;

            _padAxisX = (Input.GetAxis("HorizontalR") * direction);
            _padAxisY = (Input.GetAxis("VerticalR") * direction);

            _xRotation -= _mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            
            _xPadRotation -= _padAxisY;
            _xPadRotation = Mathf.Clamp(_padAxisX, -90f, 90f);

            _yRotation += _mouseX;

            transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
            player.Rotate(Vector3.up * _mouseX);
            player.Rotate(_xPadRotation, 0,0);
            player.Rotate(gravityRotation);
            
        }
    }
}
