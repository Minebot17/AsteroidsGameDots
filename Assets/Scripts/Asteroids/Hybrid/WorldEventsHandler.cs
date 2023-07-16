using System.Threading;
using Asteroids.ECS.Systems;
using Asteroids.UI.Core;
using Asteroids.Utils.Extension_Methods;
using Asteroids.Utils.Extension_Methods.RX;
using Asteroids.Utils.Reflection;
using UniRx;
using Unity.Entities;
using VContainer.Unity;
using UniTask = Cysharp.Threading.Tasks.UniTask;

namespace Asteroids.Hybrid
{
    [EntryPoint]
    public class WorldEventsHandler : DisposableObject, IAsyncStartable
    {
        private readonly IPanelManager _panelManager;

        public WorldEventsHandler(IPanelManager panelManager)
        {
            _panelManager = panelManager;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await UniTaskExtensions.WaitUntilWorldInited();
            var world = World.DefaultGameObjectInjectionWorld;
            var gameOverDetectionSystem = world.GetExistingSystemManaged<GameOverDetectionSystem>();
            gameOverDetectionSystem.OnGameIsOver.Subscribe(OnGameIsOver).AddTo(this);
        }

        private void OnGameIsOver(Unit _)
        {
            _panelManager.Show(PanelType.GameOver);
        }
    }
}