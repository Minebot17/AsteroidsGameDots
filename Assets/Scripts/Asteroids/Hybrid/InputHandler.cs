using Asteroids.ECS.Systems;
using Asteroids.UI.Core;
using Asteroids.Utils;
using Asteroids.Utils.Extension_Methods;
using Asteroids.Utils.Extension_Methods.RX;
using Asteroids.Utils.Reflection;
using UniRx;
using Unity.Entities;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Asteroids.Hybrid
{
    [EntryPoint]
    public class InputHandler : DisposableObject, IStartable
    {
        private readonly ISimulationToggler _simulationToggler;
        private readonly IPanelManager _panelManager;
        
        private GameInput _input;

        public InputHandler(ISimulationToggler simulationToggler, IPanelManager panelManager)
        {
            _simulationToggler = simulationToggler;
            _panelManager = panelManager;
        }

        public async void Start()
        {
            await UniTaskExtensions.WaitUntilWorldInited();
            _input = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<InputGatheringSystem>().GameInput;

            Observable.FromEvent<InputAction.CallbackContext>(
                    c => _input.Player.Pause.performed += c,
                    c => _input.Player.Pause.performed -= c)
                .Subscribe(OnPauseClick)
                .AddTo(this);
        }

        private void OnPauseClick(InputAction.CallbackContext context)
        {
            if (_simulationToggler.SimulationIsEnabled)
            {
                _panelManager.Show(PanelType.Pause);
            }
        }
    }
}