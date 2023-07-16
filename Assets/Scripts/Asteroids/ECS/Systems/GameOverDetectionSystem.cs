using Asteroids.ECS.Components;
using UniRx;
using Unity.Entities;

namespace Asteroids.ECS.Systems
{
    public partial class GameOverDetectionSystem : SystemBase
    {
        public readonly ReactiveCommand OnGameIsOver = new();

        private bool _playerIsSpawned;
        
        protected override void OnUpdate()
        {
            var playerIsExists = SystemAPI.HasSingleton<PlayerTag>();
            if (!playerIsExists && _playerIsSpawned)
            {
                OnGameIsOver.Execute();
            }
            else if (playerIsExists && !_playerIsSpawned)
            {
                _playerIsSpawned = true;
            }
        }
    }
}