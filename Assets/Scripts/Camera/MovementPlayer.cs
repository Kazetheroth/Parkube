using UnityEngine;

namespace Camera
{
    public class MovementPlayer : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Transform start;
        [SerializeField] private Transform end;
        [SerializeField] private float speed;
        [SerializeField] private RadialBlur radialBlur;
        [SerializeField] private ScriptExposer se;

        private float _counterIdle;
        private float _counterRun;
        private float _counterSpeed;
        private bool _switchIdle;
        private bool _switchRun;
        private bool _onStart;
    
        private void Update()
        {
        
            if (se.basicMovement.isMoving && !se.basicMovement.isGroundCheck)
            {
                UpdateCounter(ref _switchRun, ref _counterRun);
                _counterIdle = 0;
                _counterSpeed += Time.deltaTime;
                cameraTransform.rotation = Quaternion.Slerp(start.rotation, end.rotation, _counterRun);
                //playerTransform.Translate(speed * _counterSpeed * Time.deltaTime * Vector3.forward );
            
            }
            else
            {
                _counterSpeed -= Time.deltaTime * 4;
                _onStart = false;
                _counterRun = 0;
                UpdateCounter(ref _switchIdle, ref _counterIdle);
                cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, start.rotation,
                    _counterIdle);
            }
            _counterSpeed = Mathf.Clamp(_counterSpeed, 0, 1);
            radialBlur.blurStrength = _counterSpeed * 0.8f;
        }

    
        private void UpdateCounter(ref bool inverse,ref float counter)
        {
            if (counter > 1)
                inverse = true;
            else if (counter < 0) 
                inverse = false;
        

            if (inverse)
                counter -= Time.deltaTime * 4;
            else
                counter += Time.deltaTime * 4;

        }
    }
}
