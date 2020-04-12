using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCamera : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Transform player;

    private float _xRotation;
    private float _yRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        _yRotation += mouseX;

        transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
        player.Rotate(Vector3.up * mouseX);
    }
}
