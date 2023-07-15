using Asteroids.UI.Core;
using Asteroids.UI.ViewModels;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.UI.Views
{
    public class PauseView : BindableView<PauseViewModel>
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private Button _continueButton;
        
        protected override void OnBind(CompositeDisposable disposables)
        {
            ViewModel.Score.Subscribe(OnScoreChanged).AddTo(disposables);
            _continueButton.OnClickAsObservable().Subscribe(OnContinue).AddTo(this);
        }

        private void OnContinue(Unit _)
        {
            ViewModel.ContinueCommand.Execute();
        }

        private void OnScoreChanged(int value)
        {
            _scoreText.text = $"Score: {value}";
        }
    }
}