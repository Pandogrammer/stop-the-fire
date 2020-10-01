using System;
using UniRx;
using UnityEngine;

namespace Utils.Unity
{
    public class UnityGarbageCollector : MonoBehaviour
    {
        [SerializeField] private int _framesForCollection;
            
        private CompositeDisposable _disposables = new CompositeDisposable();

        private void OnDisable() => _disposables.Clear();

        private void OnEnable() => Load();

        public void Load()
        {
            Observable
                .EveryEndOfFrame()
                .Where(it => it % _framesForCollection == 0)
                .Subscribe(_ => { GC.Collect(); })
                .AddTo(_disposables);
        }
    }
}