using Asteroids.Hybrid;
using Asteroids.UI.Core;
using Asteroids.Utils.Extension_Methods.RX;
using UniRx;

namespace Asteroids.UI.ViewModels
{
    public class PauseViewModel : DisposableObject, IPanelViewModel
    {
        private readonly ISimulationToggler _simulationToggler;
        
        public ReactiveCommand ContinueCommand { get; } = new();

        public PanelType PanelType => PanelType.Pause;
        public IReactiveProperty<bool> IsOpened { get; } = new ReactiveProperty<bool>();
        public IReactiveProperty<int> Score { get; } = new ReactiveProperty<int>();

        public PauseViewModel(ISimulationToggler simulationToggler)
        {
            _simulationToggler = simulationToggler;
            
            ContinueCommand.Subscribe(OnContinue).AddTo(this);
            IsOpened.Subscribe(IsOpenedChanged).AddTo(this);
        }

        private void IsOpenedChanged(bool isOpened)
        {
            _simulationToggler.ToggleSimulation(!isOpened);
        }

        private void OnContinue(Unit _)
        {
            IsOpened.Value = false;
        }
    }
    
}