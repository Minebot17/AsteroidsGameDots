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
                SmallAsteroidTagGroup = SystemAPI.GetComponentLookup<SmallAsteroidTag>(),
                BulletTagGroup = SystemAPI.GetComponentLookup<BulletTag>(),
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
            [ReadOnly] public ComponentLookup<SmallAsteroidTag> SmallAsteroidTagGroup;
            [ReadOnly] public ComponentLookup<BulletTag> BulletTagGroup;
            public EntityCommandBuffer Ecb;
            
            public void Execute(TriggerEvent triggerEvent)
            {
                Entity entityA;
                Entity entityB;
                if (IsEntityCollided(triggerEvent, PlayerTagGroup, AsteroidTagGroup, 
                        out entityA, out entityB)
                    || IsEntityCollided(triggerEvent, PlayerTagGroup, SmallAsteroidTagGroup,
                        out entityA, out entityB))
                {
                    Ecb.AddComponent<NeedToDestroyTag>(entityA);
                }
                else if (IsEntityCollided(triggerEvent, AsteroidTagGroup, BulletTagGroup,
                             out entityA, out entityB))
                {
                    Ecb.AddComponent<NeedToCrackAsteroidTag>(entityA);
                    Ecb.AddComponent<NeedToDestroyTag>(entityB);
                }
                else if (IsEntityCollided(triggerEvent, SmallAsteroidTagGroup, BulletTagGroup,
                             out entityA, out entityB))
                {
                    Ecb.AddComponent<NeedToDestroyTag>(entityA);
                    Ecb.AddComponent<NeedToDestroyTag>(entityB);
                }
            }

            private bool IsEntityCollided<T1, T2>(
                TriggerEvent triggerEvent, 
                ComponentLookup<T1> firstTag,
                ComponentLookup<T2> secondTag,
                out Entity firstTagEntity,
                out Entity secondTagEntity)
                where T1 : unmanaged, IComponentData
                where T2 : unmanaged, IComponentData
            {
                var entityA = triggerEvent.EntityA;
                var entityB = triggerEvent.EntityB;
                var entityAIsTag1 = firstTag.HasComponent(entityA);
                var entityBIsTag1 = firstTag.HasComponent(entityB);
                var entityAIsTag2 = secondTag.HasComponent(entityA);
                var entityBIsTag2 = secondTag.HasComponent(entityB);
                var isCollided = entityAIsTag1 && entityBIsTag2 || entityAIsTag2 && entityBIsTag1;
                
                firstTagEntity = isCollided ? (entityAIsTag1 ? entityA : entityB) : new Entity();
                secondTagEntity = isCollided ? (entityAIsTag2 ? entityA : entityB) : new Entity();
                return isCollided;
            }
        }
    }
}