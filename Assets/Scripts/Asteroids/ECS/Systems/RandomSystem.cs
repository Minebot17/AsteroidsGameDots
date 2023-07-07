using Asteroids.ECS.Components;
using Unity.Burst;
using Unity.Entities;

namespace Asteroids.ECS.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct RandomSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.EntityManager.CreateSingleton(new RandomData((uint) UnityEngine.Random.Range(0, int.MaxValue - 1)));
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}