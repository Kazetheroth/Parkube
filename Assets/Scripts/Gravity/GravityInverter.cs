using System.Collections;
using System.Collections.Generic;
using Physical;
using UnityEngine;

public class GravityInverter : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private BasicMovement playerBasicMovement;
    [SerializeField] private BasicCamera playerBasicCamera;


    private Quaternion actualRotation;
    public bool _isInverted;

    // Start is called before the first frame update
    void Start()
    {
        playerBasicMovement.gravity = -9.81f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Hello");
            InvertGravity();
        }
    }

    public void InvertGravity()
    {
        playerBasicMovement.gravity = -playerBasicMovement.gravity;
        playerTransform.Rotate(0.0f, 0.0f, 180.0f);
        playerBasicCamera.direction = -playerBasicCamera.direction;
        if(!_isInverted)
            playerBasicCamera.gravityRotation = new Vector3(0.0f, 0.0f, 180.0f);
        else
            playerBasicCamera.gravityRotation = new Vector3(0.0f, 0.0f, 0.0f);
        _isInverted = !_isInverted;
    }
}
