using UnityEngine;

namespace Utils.Unity.InputCapture
{
    public class TouchInput : InputCapturer
    {
        public bool GetButtonHold()
        {
            if (Input.touchCount <= 0)
                return false;
            var phase = Input.GetTouch(0).phase;
            return phase == TouchPhase.Moved || phase == TouchPhase.Stationary;
        }

        public UnityEngine.Vector3 GetNormalizedPosition()
        {
            var touchPosition = GetPosition();
            var normalized = new UnityEngine.Vector3(touchPosition.x / Screen.width, touchPosition.y / Screen.height, 0);
            return normalized;
        }

        public bool GetButtonStart()
        {
            if (Input.touchCount <= 0)
                return false;
            return Input.GetTouch(0).phase == TouchPhase.Began;
        }

        public UnityEngine.Vector3 GetPosition()
        {
            if (Input.touchCount <= 0)
                return UnityEngine.Vector3.zero;
            return Input.GetTouch(0).position;
        }
    }
}