using Asteroids.ECS.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;

namespace Asteroids.ECS.Systems
{
    [UpdateAfter(typeof(PlayerWeaponSystem))]
    public partial struct LaserInitializeSystem : ISystem
    {
        private NativeList<RaycastHit> _hitsList;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            _hitsList = new NativeList<RaycastHit>(Allocator.Persistent);
            state.RequireForUpdate<PhysicsWorldSingleton>();
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);

            new LaserRaycastJob
            {
                Ecb = ecb,
                PhysicsWorld = physicsWorld,
                HitsList = _hitsList
            }.Schedule();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
        
        [BurstCompile]
        public partial struct LaserRaycastJob : IJobEntity
        {
            public EntityCommandBuffer Ecb;
            public PhysicsWorldSingleton PhysicsWorld;
            public NativeList<RaycastHit> HitsList;

            public void Execute(
                LaserInitializeTag laserInitializeTag, 
                LocalTransform transform,
                Entity entity
            )
            {
                var raycastInput = new RaycastInput
                {
                    Start = transform.Position,
                    End = transform.Up() * 10000,
                    Filter = new CollisionFilter
                    {
                        BelongsTo = 1u << 2,
                        CollidesWith = 1u << 0
                    }
                };

                PhysicsWorld.CastRay(raycastInput, ref HitsList);
                
                foreach (var hit in HitsList)
                {
                    Ecb.DestroyEntity(hit.Entity);
                }
                
                Ecb.RemoveComponent<LaserInitializeTag>(entity);
            }
        }
    }
}