using System;
using TMPro;
using UniRx;
using UnityEngine;
using Utils.Unity.PandoBehaviours;

namespace Game.UI
{
    public class ScreenController : AutoLoadMonoBehaviour
    {
        [SerializeField] private GameObject _joystick;
        [SerializeField] private TextMeshProUGUI _resistedTimer;

        private string _resistedText = "Resististe";
        protected override void Load()
        {
            Screen.SetResolution(720, 1280, false);

            Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
                .Subscribe(SetTimer)
                .AddTo(_disposables);

            SetResistanceText(0.ToString());
        }

        private void SetTimer(long time)
        {
            var timeSpan = TimeSpan.FromSeconds(time);
            var timeText = $"{timeSpan.Minutes:00}" + ":" + $"{timeSpan.Seconds:00}";
            SetResistanceText(timeText);
        }

        private void SetResistanceText(string timeText)
        {
            _resistedTimer.text = _resistedText +"\n"+ timeText;
        }
    }
}
