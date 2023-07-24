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
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PhysicsWorldSingleton>();
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
            
            _hitsList = new NativeList<RaycastHit>(Allocator.Persistent);
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
            var beginEcb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);
            var endEcb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);

            new LaserRaycastJob
            {
                BeginEcb = beginEcb,
                EndEcb = endEcb,
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
            public EntityCommandBuffer BeginEcb;
            public EntityCommandBuffer EndEcb;
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
                    BeginEcb.AddComponent<NeedToDestroyTag>(hit.Entity);
                }
                
                HitsList.Clear();
                EndEcb.RemoveComponent<LaserInitializeTag>(entity);
            }
        }
    }
}