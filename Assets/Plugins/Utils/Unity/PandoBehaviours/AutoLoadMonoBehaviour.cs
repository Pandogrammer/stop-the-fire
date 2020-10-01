namespace Utils.Unity.PandoBehaviours
{
    public abstract class AutoLoadMonoBehaviour : DisposableMonoBehaviour
    {
        private void OnEnable() => Load();

        protected abstract void Load();
    }
}