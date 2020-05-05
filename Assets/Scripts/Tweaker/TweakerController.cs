using System;
using System.Globalization;
using JSONReader;
using UnityEngine;
using UnityEngine.UI;

namespace Tweaker
{
    public class TweakerController : MonoBehaviour
    {
        /* Characters */
        [SerializeField] private InputField baseSpeedField;
        [SerializeField] private Button baseSpeedButton;
        
        [SerializeField] private InputField sprintSpeedField;
        [SerializeField] private Button sprintSpeedButton;
        
        [SerializeField] private InputField jumpHeightField;
        [SerializeField] private Button jumpHeightButton;
        
        [SerializeField] private InputField slidingLengthField;
        [SerializeField] private Button slidingButton;
        
        /* Controls */
        [SerializeField] private Text forwardField;
        [SerializeField] private Button forwardButton;
        
        [SerializeField] private Text backField;
        [SerializeField] private Button backButton;
        
        [SerializeField] private Text leftField;
        [SerializeField] private Button leftButton;
        
        [SerializeField] private Text rightField;
        [SerializeField] private Button rightButton;
        
        /* Camera */
        [SerializeField] private UnityEngine.Camera camera;
        [SerializeField] private InputField blurfField;
        [SerializeField] private Button blurButton;
        
        [SerializeField] private InputField fovField;
        [SerializeField] private Button fovButton;
        
        /* Tweaker Datas */
        [SerializeField] private TweakerManager tweakerDatas;
        [SerializeField] private GameObject pressKey;

        private bool _changingKey = false;
        private int _keyChoosed;
        
    
        
    
        void Start()
        {
            tweakerDatas.loadJSON();
            camera.fieldOfView = tweakerDatas.DSE.Camera.FOV;
            baseSpeedField.text = tweakerDatas.DSE.Character.baseSpeed.ToString(CultureInfo.InvariantCulture);
            sprintSpeedField.text = tweakerDatas.DSE.Character.sprintSpeed.ToString(CultureInfo.InvariantCulture);
            forwardField.text = tweakerDatas.DSE.Controls.forward.ToString();
            backField.text = tweakerDatas.DSE.Controls.backward.ToString();
            leftField.text = tweakerDatas.DSE.Controls.left.ToString();
            rightField.text = tweakerDatas.DSE.Controls.right.ToString();
            jumpHeightField.text = tweakerDatas.DSE.Character.heightJump.ToString(CultureInfo.InvariantCulture);
            slidingLengthField.text = tweakerDatas.DSE.Character.lengthSlide.ToString(CultureInfo.InvariantCulture);

            baseSpeedButton.onClick.AddListener(delegate
            {
                tweakerDatas.DSE.Character.baseSpeed = float.Parse(baseSpeedField.text);
                tweakerDatas.saveJSON();
            });
            sprintSpeedButton.onClick.AddListener(delegate
            {
                tweakerDatas.DSE.Character.sprintSpeed = float.Parse(sprintSpeedField.text);
                tweakerDatas.saveJSON();
            });
            jumpHeightButton.onClick.AddListener(delegate
            {
                tweakerDatas.DSE.Character.heightJump = float.Parse(jumpHeightField.text);
                tweakerDatas.saveJSON();
            });
            slidingButton.onClick.AddListener(delegate
            {
                tweakerDatas.DSE.Character.lengthSlide = float.Parse(slidingLengthField.text);
                tweakerDatas.saveJSON();
            });
            
            blurfField.text = tweakerDatas.DSE.Camera.blurModifier.ToString(CultureInfo.InvariantCulture);
            fovField.text = tweakerDatas.DSE.Camera.FOV.ToString(CultureInfo.InvariantCulture);
            
            blurButton.onClick.AddListener(delegate
            {
                tweakerDatas.DSE.Camera.blurModifier = float.Parse(blurfField.text);
                tweakerDatas.saveJSON();
            });
            fovButton.onClick.AddListener(delegate
            {
                tweakerDatas.DSE.Camera.FOV = float.Parse(fovField.text);
                camera.fieldOfView = tweakerDatas.DSE.Camera.FOV;
                tweakerDatas.saveJSON();
            });
            
            forwardButton.onClick.AddListener(delegate
            {
                ChangeKey(1);
            });
            
        }

        private void OnGUI()
        {
            if (_changingKey)
            {
                Event e = Event.current;
                if (e.isKey)
                {
                    print("A");
                    Debug.Log("Detected key code: " + e.keyCode);
                    switch (_keyChoosed)
                    {
                        case 1:
                            print("Wesh");
                            _keyChoosed = 0;
                            _changingKey = false;
                            break;
                    }
                    pressKey.SetActive(false);
                }
            }
        }

        void ChangeKey(int value)
        {
            pressKey.SetActive(true);
            _keyChoosed = value;
            _changingKey = true;
        }
    }
}
