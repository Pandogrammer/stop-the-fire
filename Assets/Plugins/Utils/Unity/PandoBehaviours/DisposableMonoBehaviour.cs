using UniRx;

namespace Utils.Unity.PandoBehaviours
{
    public abstract class DisposableMonoBehaviour : UpdateableMonoBehaviour
    {
        protected CompositeDisposable _disposables = new CompositeDisposable();

        private void OnDisable() => BaseUnload();

        private void BaseUnload() { 
            _disposables.Dispose();
            Unload();
        }
        
        protected virtual void Unload() { }
  
    }
}