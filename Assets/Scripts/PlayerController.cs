using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private int lastWallJumpId;

    // Update is called once per frame
    void Update()
    {
        lastWallJumpId = 0;
        return;
    }
}
