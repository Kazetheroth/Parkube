using System.Globalization;
using JSONReader;
using UnityEngine;
using UnityEngine.UI;

namespace Tweaker
{
    public class TweakerController : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private InputField baseSpeedField;
        [SerializeField] private Button baseSpeedButton;
        
        [SerializeField] private InputField sprintSpeedField;
        [SerializeField] private Button sprintSpeedButton;
        
        [SerializeField] private InputField jumpHeightField;
        [SerializeField] private Button jumpHeightButton;
        
        [SerializeField] private InputField slidingLengthField;
        [SerializeField] private Button slidingButton;
        
        [SerializeField] private InputField blurfField;
        [SerializeField] private Button blurButton;
        
        [SerializeField] private InputField fovField;
        [SerializeField] private Button fovButton;
        
        [SerializeField] private TweakerManager tweakerDatas;
        [SerializeField] private UnityEngine.Camera camera;
    
    
    
        void Start()
        {
            tweakerDatas.loadJSON();
            camera.fieldOfView = tweakerDatas.DSE.Camera.FOV;
            baseSpeedField.text = tweakerDatas.DSE.Character.baseSpeed.ToString(CultureInfo.InvariantCulture);
            sprintSpeedField.text = tweakerDatas.DSE.Character.sprintSpeed.ToString(CultureInfo.InvariantCulture);
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
        }
    }
}
