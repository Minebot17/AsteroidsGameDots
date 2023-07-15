using System;
using Asteroids.ECS.Systems;
using Asteroids.UI.Core;
using Asteroids.Utils;
using Asteroids.Utils.Extension_Methods;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Asteroids.Hybrid
{
    public class InputManager : MonoBehaviour
    {
        private ISimulationToggler _simulationToggler;
        private IPanelManager _panelManager;

        [Inject]
        public void Construct(ISimulationToggler simulationToggler, IPanelManager panelManager)
        {
            _simulationToggler = simulationToggler;
            _panelManager = panelManager;
        }
        
        private GameInput _input;

        public async void Start()
        {
            await UniTaskExtensions.WaitUntilWorldInited();
            _input = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<InputGatheringSystem>().GameInput;
            
            _input.Player.Pause.performed += OnPauseClick;
        }

        private void OnDestroy()
        {
            _input.Player.Pause.performed -= OnPauseClick;
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