using Asteroids.ECS.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

namespace Asteroids.ECS.Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(PhysicsSystemGroup))]
    public partial struct TriggersHandleSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var simulation = SystemAPI.GetSingletonRW<SimulationSingleton>();
            var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);
            
            state.Dependency = new TriggersHandleJob
            {
                PlayerTagGroup = SystemAPI.GetComponentLookup<PlayerTag>(),
                AsteroidTagGroup = SystemAPI.GetComponentLookup<AsteroidTag>(),
                Ecb = ecb
            }.Schedule(simulation.ValueRW, state.Dependency);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
        
        [BurstCompile]
        public partial struct TriggersHandleJob : ITriggerEventsJob
        {
            [ReadOnly] public ComponentLookup<PlayerTag> PlayerTagGroup;
            [ReadOnly] public ComponentLookup<AsteroidTag> AsteroidTagGroup;
            public EntityCommandBuffer Ecb;
            
            public void Execute(TriggerEvent triggerEvent)
            {
                var entityA = triggerEvent.EntityA;
                var entityB = triggerEvent.EntityB;
                var entityAIsPlayer = PlayerTagGroup.HasComponent(entityA);
                var entityBIsPlayer = PlayerTagGroup.HasComponent(entityB);
                var entityAIsAsteroid = AsteroidTagGroup.HasComponent(entityA);
                var entityBIsAsteroid = AsteroidTagGroup.HasComponent(entityB);

                if (entityAIsAsteroid && entityBIsPlayer
                    || entityBIsAsteroid && entityAIsPlayer)
                {
                    var playerEntity = entityAIsPlayer ? entityA : entityB;
                    Ecb.AddComponent<NeedToDestroyTag>(playerEntity);
                }
            }
        }
    }
}