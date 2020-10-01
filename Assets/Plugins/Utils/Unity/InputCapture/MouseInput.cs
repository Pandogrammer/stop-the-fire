using UnityEngine;

namespace Utils.Unity.InputCapture
{
    public class MouseInput : InputCapturer
    {
        public bool GetButtonHold()
        {
            return Input.GetMouseButton(0);
        }

        public UnityEngine.Vector3 GetNormalizedPosition()
        {
            var mousePosition = GetPosition();
            var normalized = new UnityEngine.Vector3(mousePosition.x / Screen.width, mousePosition.y / Screen.height, 0);
            return normalized;
        }

        public bool GetButtonStart()
        {
            return Input.GetMouseButtonDown(0);
        }

        public UnityEngine.Vector3 GetPosition()
        {
            return Input.mousePosition;
        }
    }
}