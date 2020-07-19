using System.Collections;
using System.Collections.Generic;
using Physical;
using UnityEngine;

public class GravityInverter : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private BasicMovement playerBasicMovement;

    // Start is called before the first frame update
    void Start()
    {
        //playerBasicMovement.gravity = -9.81f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Hello");
                //playerBasicMovement.gravity = -playerBasicMovement.gravity;
        }
            
        
    }
}
