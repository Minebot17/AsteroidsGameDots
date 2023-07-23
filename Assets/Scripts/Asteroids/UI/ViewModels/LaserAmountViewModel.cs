using System;
using Asteroids.ECS.Components;
using Asteroids.Utils.Extension_Methods;
using Asteroids.Utils.Extension_Methods.RX;
using Asteroids.Utils.Reflection;
using UniRx;
using Unity.Collections;
using Unity.Entities;

namespace Asteroids.UI.ViewModels
{
    [SingletonViewModel]
    public class LaserAmountViewModel : DisposableObject
    {
        public readonly IReactiveCollection<float> ChargesStates = new ReactiveCollection<float>();
        
        private EntityManager _entityManager;
        private EntityQuery _weaponDataQuery;

        public LaserAmountViewModel()
        {
            Initialize();
        }

        private async void Initialize()
        {
            await UniTaskExtensions.WaitUntilWorldInited();
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            _weaponDataQuery = new EntityQueryBuilder(Allocator.Temp)
                .WithAll<PlayerWeaponData>()
                .Build(_entityManager);

            var weaponData = await _weaponDataQuery.WaitUntilSingletonCreated<PlayerWeaponData>();
            for (var i = 0; i < weaponData.LaserMaxCharges; i++)
            {
                ChargesStates.Add(1);
            }

            Observable.EveryUpdate().Subscribe(OnUpdate).AddTo(this);
        }

        private void OnUpdate(long _)
        {
            var weaponData = _weaponDataQuery.GetSingleton<PlayerWeaponData>();

            for (var i = 0; i < weaponData.LaserMaxCharges; i++)
            {
                ChargesStates[i] = weaponData.LaserCurrentCharges > i 
                    ? 1
                    : weaponData.LaserCurrentCharges < i 
                        ? 0
                        : 1 - (float) (weaponData.LaserChargeNextTime - World.DefaultGameObjectInjectionWorld.Time.ElapsedTime) / weaponData.LaserChargeCooldown;
            }
        }
    }
}