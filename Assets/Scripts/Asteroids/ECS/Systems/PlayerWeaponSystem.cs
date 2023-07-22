using Asteroids.ECS.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace Asteroids.ECS.Systems
{
    public partial struct PlayerWeaponSystem : ISystem
    {
        private const float SpawnWeaponsOffset = 1f;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
            state.RequireForUpdate<PlayerInputData>();
            state.RequireForUpdate<PlayerWeaponData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var weaponData = SystemAPI.GetSingletonRW<PlayerWeaponData>();
            var playerInputData = SystemAPI.GetSingleton<PlayerInputData>();
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = state.EntityManager.GetComponentData<LocalTransform>(playerEntity);

            if (playerInputData.IsFire && weaponData.ValueRO.BulletFireNextTime < SystemAPI.Time.ElapsedTime)
            {
                var bulletEntity = state.EntityManager.Instantiate(weaponData.ValueRO.BulletPrefab);
                state.EntityManager.SetComponentData(bulletEntity, new LocalTransform
                {
                    Position = playerTransform.Position + playerTransform.Up() * SpawnWeaponsOffset,
                    Rotation = quaternion.identity,
                    Scale = 1f
                });
                state.EntityManager.SetComponentData(bulletEntity, new PhysicsVelocity
                {
                    Angular = float3.zero,
                    Linear = playerTransform.Up() * weaponData.ValueRO.BulletVelocity
                });
                state.EntityManager.SetComponentData(bulletEntity, new NeedToDestroyAfterTimeData
                {
                    TimeWhenNeedToDestroy = (float) SystemAPI.Time.ElapsedTime + weaponData.ValueRO.BulletLifeDuration
                });

                weaponData.ValueRW.BulletFireNextTime =
                    (float) SystemAPI.Time.ElapsedTime + weaponData.ValueRO.BulletFireCooldown;
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}