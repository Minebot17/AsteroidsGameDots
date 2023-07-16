using Asteroids.UI.Core;
using Asteroids.UI.ViewModels;
using Asteroids.Utils.Extension_Methods.RX;
using TMPro;
using UniRx;
using UnityEngine;

namespace Asteroids.UI.Views
{
    public class HudView : BindableView<HudViewModel>
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _positionXText;
        [SerializeField] private TMP_Text _positionYText;
        [SerializeField] private TMP_Text _angleText;
        [SerializeField] private TMP_Text _speedText;
        
        protected override void OnBind(CompositeDisposable disposables)
        {
            this.BindPanel(ViewModel, disposables);
            ViewModel.Score.Subscribe(value => _scoreText.text = $"Score: {value}").AddTo(disposables);
            ViewModel.PositionX.Subscribe(value => _positionXText.text = $"X: {value}").AddTo(disposables);
            ViewModel.PositionY.Subscribe(value => _positionYText.text = $"Y: {value}").AddTo(disposables);
            ViewModel.Angle.Subscribe(value => _angleText.text = $"Angle: {value}°").AddTo(disposables);
            ViewModel.Speed.Subscribe(value => _speedText.text = $"Speed: {value} m/s").AddTo(disposables);
        }
    }
}