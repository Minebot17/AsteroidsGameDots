using Asteroids.ECS.Components;
using Unity.Burst;
using Unity.Entities;

namespace Asteroids.ECS.Systems
{
    public partial struct DestroyEntityAfterTimeSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();

            new DestroyEntityAfterTimeJob
            {  
                Ecb = ecb,
                TimeElapsed = (float) SystemAPI.Time.ElapsedTime
            }.ScheduleParallel();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
        
        [BurstCompile]
        public partial struct DestroyEntityAfterTimeJob : IJobEntity
        {
            public EntityCommandBuffer.ParallelWriter Ecb;
            public float TimeElapsed;
            
            public void Execute([ChunkIndexInQuery] int chunkIndex, NeedToDestroyAfterTimeData needToDestroyAfterTimeData, Entity e)
            {
                if (needToDestroyAfterTimeData.TimeWhenNeedToDestroy < TimeElapsed)
                {
                    Ecb.DestroyEntity(chunkIndex, e);
                }
            }
        }
    }
}