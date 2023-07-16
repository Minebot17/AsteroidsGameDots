using Asteroids.ECS.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Asteroids.ECS.Systems
{
    public partial struct DestroyEntitySystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<NeedToDestroyTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entitiesToDestroy = SystemAPI.QueryBuilder()
                .WithAll<NeedToDestroyTag>()
                .Build()
                .ToEntityArray(Allocator.Temp);

            state.EntityManager.DestroyEntity(entitiesToDestroy);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }
    }
}