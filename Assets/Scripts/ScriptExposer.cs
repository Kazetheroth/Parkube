using System.Collections;
using System.Collections.Generic;
using JSONReader;
using Physical;
using UnityEngine;
using UnityEngine.Serialization;

public class ScriptExposer : MonoBehaviour
{
    [SerializeField] public BasicMovement basicMovement;
    [SerializeField] public BasicCamera basicCamera;
    [SerializeField] public TweakerManager tweakerDatas;
    [SerializeField] public GravityInverter gravityInverter;

    public bool isInMenu;

}
