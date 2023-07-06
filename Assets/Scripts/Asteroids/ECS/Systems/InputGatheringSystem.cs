using Asteroids.ECS.Components;
using Asteroids.Utils;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.ECS.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class InputGatheringSystem : SystemBase, GameInput.IPlayerActions
    {
        private EntityQuery _playerInputQuery;
        private GameInput _gameInput;

        private float3 _playerMove;
        private bool _playerFire;
        private bool _playerAltFire;

        public GameInput GameInput => _gameInput;

        protected override void OnCreate()
        {
            _gameInput = new GameInput();
            _gameInput.Player.SetCallbacks(this);

            _playerInputQuery = GetEntityQuery(typeof(PlayerInputData));
        }

        protected override void OnUpdate()
        {
            if (_playerInputQuery.CalculateEntityCount() == 0)
            {
                EntityManager.CreateEntity(typeof(PlayerInputData));
            }
            
            _playerInputQuery.SetSingleton(new PlayerInputData
            {
                Move = _playerMove,
                IsFire = _playerFire,
                IsAltFire = _playerAltFire
            });

            _playerFire = false;
            _playerAltFire = false;
        }

        public void OnMove(InputAction.CallbackContext context) => _playerMove = (Vector3) context.ReadValue<Vector2>();
        public void OnFire(InputAction.CallbackContext context) { if (context.started) _playerFire = true; }
        public void OnAltFire(InputAction.CallbackContext context) { if (context.started) _playerAltFire = true; }
        
        protected override void OnStartRunning() => _gameInput.Enable();

        protected override void OnStopRunning() => _gameInput.Disable();
    }
}