namespace Utils.Unity.InputCapture
{
    public static class InputCapturerDealer
    {
        private static MouseInput mouseInput = new MouseInput();
        private static TouchInput touchInput = new TouchInput();

        public static InputCapturer Retrieve()
        {
#if UNITY_EDITOR
            return mouseInput;
#elif UNITY_IOS
            return touchInput;
#elif UNITY_ANDROID
            return touchInput;
#else
            return mouseInput;
#endif
        }
    }
}