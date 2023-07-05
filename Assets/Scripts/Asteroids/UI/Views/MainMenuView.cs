using Asteroids.UI.Core;
using Asteroids.UI.ViewModels;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.UI.Views
{
    public class MainMenuView : BindableView<MainMenuViewModel>
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;
        
        protected override void OnBind(CompositeDisposable disposables)
        {
            _playButton.OnClickAsObservable()
                .Subscribe(_ => ViewModel.PlayCommand.Execute())
                .AddTo(disposables);
            
            _exitButton.OnClickAsObservable()
                .Subscribe(_ => ViewModel.ExitCommand.Execute())
                .AddTo(disposables);
        }
    }
}