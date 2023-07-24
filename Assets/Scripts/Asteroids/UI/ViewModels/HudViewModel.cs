using System;
using Asteroids.ECS.Components;
using Asteroids.UI.Core;
using Asteroids.Utils.Extension_Methods;
using Asteroids.Utils.Extension_Methods.RX;
using Asteroids.Utils.Reflection;
using UniRx;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace Asteroids.UI.ViewModels
{
    [SingletonViewModel]
    public class HudViewModel : DisposableObject, IPanelViewModel
    {
        public PanelType PanelType => PanelType.Hud;
        public IReactiveProperty<bool> IsOpened { get; } = new ReactiveProperty<bool>();
        public IReactiveProperty<int> Score { get; } = new ReactiveProperty<int>(); // TODO
        public IReactiveProperty<int> PositionX { get; } = new ReactiveProperty<int>();
        public IReactiveProperty<int> PositionY { get; } = new ReactiveProperty<int>();
        public IReactiveProperty<int> Angle { get; } = new ReactiveProperty<int>();
        public IReactiveProperty<int> Speed { get; } = new ReactiveProperty<int>();
        
        private EntityManager _entityManager;
        private Entity _playerEntity;
        private EntityQuery _scoreDataQuery;

        public HudViewModel()
        {
            Initialize();
        }

        private async void Initialize()
        {
            await UniTaskExtensions.WaitUntilWorldInited();
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var playerQuery = new EntityQueryBuilder(Allocator.Temp)
                .WithAll<PlayerTag>()
                .Build(_entityManager);

            _scoreDataQuery = new EntityQueryBuilder(Allocator.Temp)
                .WithAll<ScoreData>()
                .Build(_entityManager);

            _playerEntity = await playerQuery.WaitUntilSingletonEntityCreated();
            Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(0.05f)).Subscribe(OnUpdate).AddTo(this);
        }

        private void OnUpdate(long next)
        {
            try
            {
                if (!_entityManager.Exists(_playerEntity))
                {
                    return;
                }
            }
            catch (ObjectDisposedException e)
            {
                return;
            }

            var playerTransform = _entityManager.GetComponentData<LocalTransform>(_playerEntity);
            var playerVelocity = _entityManager.GetComponentData<PhysicsVelocity>(_playerEntity);
            var playerRotation = (int) math.degrees(playerTransform.Rotation.ToEuler().z);
            
            PositionX.Value = (int) playerTransform.Position.x;
            PositionY.Value = (int) playerTransform.Position.y;
            Angle.Value = playerRotation > 0 ? playerRotation % 360 : 360 + playerRotation % 360;
            Speed.Value = (int) math.length(playerVelocity.Linear);

            if (_scoreDataQuery.TryGetSingleton(out ScoreData scoreData))
            {
                Score.Value = scoreData.Score;
            }
        }
    }
}