using Asteroids.Hybrid;
using Asteroids.UI.Core;
using Asteroids.Utils.Extension_Methods.RX;
using Asteroids.Utils.Reflection;
using UniRx;

namespace Asteroids.UI.ViewModels
{
    [SingletonViewModel]
    public class GameOverViewModel : DisposableObject, IPanelViewModel
    {
        private readonly ISimulationToggler _simulationToggler;
        
        public ReactiveCommand RestartCommand { get; } = new();

        public PanelType PanelType => PanelType.GameOver;
        public IReactiveProperty<bool> IsOpened { get; } = new ReactiveProperty<bool>();
        public IReactiveProperty<int> Score { get; } = new ReactiveProperty<int>();
        
        public GameOverViewModel(ISimulationToggler simulationToggler)
        {
            _simulationToggler = simulationToggler;
            
            RestartCommand.Subscribe(OnRestart).AddTo(this);
            IsOpened.Subscribe(IsOpenedChanged).AddTo(this);
        }

        private void IsOpenedChanged(bool isOpened)
        {
            _simulationToggler.ToggleSimulation(!isOpened);
        }

        private void OnRestart(Unit _)
        {
            IsOpened.Value = false;
            // TODO restart
        }
    }
}