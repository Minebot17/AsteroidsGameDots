using Asteroids.UI.Core;
using Asteroids.Utils.Extension_Methods.RX;
using UniRx;

namespace Asteroids.UI.ViewModels
{
    public class PauseViewModel : DisposableObject, IPanelViewModel
    {
        public ReactiveCommand ContinueCommand { get; } = new();

        public PanelType PanelType => PanelType.Pause;
        public IReactiveProperty<bool> IsOpened { get; } = new ReactiveProperty<bool>();
        public IReactiveProperty<int> Score { get; } = new ReactiveProperty<int>();

        public PauseViewModel()
        {
            ContinueCommand.Subscribe(OnContinue).AddTo(this);
            IsOpened.Subscribe(IsOpenedChanged).AddTo(this);
        }

        private void IsOpenedChanged(bool isOpened)
        {
            if (isOpened)
            {
                // TODO stop time
            }
        }

        private void OnContinue(Unit _)
        {
            // TODO start time, close
        }
    }
    
}