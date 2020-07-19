using System.Collections;
using System.Collections.Generic;
using Physical;
using UnityEngine;

public class GravityInverter : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private ScriptExposer scriptExposer;


    private Quaternion actualRotation;
    private bool _isInverted;

    // Start is called before the first frame update
    void Start()
    {
        scriptExposer.basicMovement.gravity = -9.81f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Hello");
            scriptExposer.basicMovement.gravity = -scriptExposer.basicMovement.gravity;
            playerTransform.Rotate(0.0f, 0.0f, 180.0f);
            scriptExposer.basicCamera.direction = -scriptExposer.basicCamera.direction;
            if(!_isInverted)
                scriptExposer.basicCamera.gravityRotation = new Vector3(0.0f, 0.0f, 180.0f);
            else
                scriptExposer.basicCamera.gravityRotation = new Vector3(0.0f, 0.0f, 0.0f);
            _isInverted = !_isInverted;
        }
    }
}
