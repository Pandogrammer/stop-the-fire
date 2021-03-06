﻿using System;
using Game.Context.Scripts;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;
using Utils.Unity.PandoBehaviours;

namespace Game.UI
{
    public class ScreenController : AutoLoadMonoBehaviour
    {
        [SerializeField] private GameObject _gameScreen;
        [SerializeField] private GameObject _defeatScreen;
        //game
        [SerializeField] private TextMeshProUGUI _resistedTimer;
        //defeat
        [SerializeField] private TextMeshProUGUI _timeResisted;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _moreInformationButton;
        
        
        private string _resistedText = "Resististe";

        private IDisposable timerSubscription;

        protected override void Load()
        {
            Screen.SetResolution(720, 1280, false);

            _restartButton
                .OnClickAsObservable()
                .Subscribe(_ => RestartGame());
            
            _moreInformationButton
                .OnClickAsObservable()
                .Subscribe(_ => OpenMoreInformation());
            
            timerSubscription = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
                .Subscribe(SetTimer)
                .AddTo(_disposables);

            EventStream.Receive<Defeat>()
                .Do(_ => SwitchToDefeat())
                .Subscribe()
                .AddTo(_disposables);

            SetResistanceText(0.ToString());
        }

        private void OpenMoreInformation()
        {
            Application.OpenURL("https://www.argentina.gob.ar/ambiente/conciencia-ambiental/incendios-delta");
        }

        private static void RestartGame()
        {
            SceneManager.LoadScene("Game");
        }

        private void SwitchToDefeat()
        {
            _gameScreen.SetActive(false);
            timerSubscription.Dispose();
            _timeResisted.text = _resistedTimer.text;
            _defeatScreen.SetActive(true);
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
