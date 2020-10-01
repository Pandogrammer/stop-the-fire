
namespace Utils.Unity.InputCapture
{
    public interface InputCapturer
    {
        UnityEngine.Vector3 GetPosition();
        UnityEngine.Vector3 GetNormalizedPosition();
        bool GetButtonStart();
        bool GetButtonHold();
    }
}