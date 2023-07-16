using Asteroids.UI.Core;
using Asteroids.UI.ViewModels;
using Asteroids.Utils.Extension_Methods.RX;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.UI.Views
{
    public class GameOverView : BindableView<GameOverViewModel>
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private Button _restartButton;
        
        protected override void OnBind(CompositeDisposable disposables)
        {
            this.BindPanel(ViewModel, disposables);
            ViewModel.Score.Subscribe(OnScoreChanged).AddTo(disposables);
            _restartButton.OnClickAsObservable().Subscribe(OnRestart).AddTo(this);
        }
        
        private void OnRestart(Unit _)
        {
            ViewModel.RestartCommand.Execute();
        }

        private void OnScoreChanged(int value)
        {
            _scoreText.text = $"Score: {value}";
        }
    }
}