﻿using System;
using Tweaker;
using UnityEngine;

namespace JSONReader
{
    public class TweakerManager : MonoBehaviour
    {
        [HideInInspector]
        public DataSavedExposer DSE = new DataSavedExposer();

        public void Awake()
        {
            DSE.Camera = new CameraTweaker();
            DSE.Character = new CharacterTweaker();
            DSE.Controls = new ControlsTweaker();
            loadJSON();
        }

        public void saveJSON()
        {
            string parsedObject = JsonUtility.ToJson(DSE);
            Debug.Log(parsedObject);
            System.IO.File.WriteAllText("Tweaker.json", parsedObject);
        }

        public void loadJSON()
        {
            string jsonObject = System.IO.File.ReadAllText("Tweaker.json");
            JsonUtility.FromJsonOverwrite(jsonObject, DSE);
            
        }
    }
}

