using Asteroids.ECS.Components;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace Asteroids.ECS.Systems
{
    public partial struct AsteroidCrackingSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<AsteroidCrackingData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var rnd = SystemAPI.GetSingletonRW<RandomData>();
            var crackingSettings = SystemAPI.GetSingleton<AsteroidCrackingData>();
            var beginEcb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
            var endEcb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();

            new CrackAsteroidsJob
            {
                Rnd = rnd,
                CrackingSettings = crackingSettings,
                BeginEcb = beginEcb,
                EndEcb = endEcb
            }.ScheduleParallel();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
        
        [BurstCompile]
        public partial struct CrackAsteroidsJob : IJobEntity
        {
            [NativeDisableUnsafePtrRestriction] public RefRW<RandomData> Rnd;
            public AsteroidCrackingData CrackingSettings;
            public EntityCommandBuffer.ParallelWriter BeginEcb;
            public EntityCommandBuffer.ParallelWriter EndEcb;

            public void Execute(
                [ChunkIndexInQuery] int chunkIndex, 
                NeedToCrackAsteroidTag tag, 
                LocalTransform transform, 
                PhysicsVelocity velocity, 
                Entity asteroidEntity
            ) {
                var smallAsteroidsVelocityScale = math.length(velocity.Linear) / CrackingSettings.FragmentsCount;
                BeginEcb.AddComponent<NeedToDestroyTag>(chunkIndex, asteroidEntity);
                EndEcb.RemoveComponent<NeedToCrackAsteroidTag>(chunkIndex, asteroidEntity);

                for (var i = 0; i < CrackingSettings.FragmentsCount; i++)
                {
                    var velocityVector = Rnd.ValueRW.Random.NextFloat2Direction() * smallAsteroidsVelocityScale;
                    var smallAsteroidEntity = BeginEcb.Instantiate(chunkIndex, CrackingSettings.SmallAsteroidPrefab);
                    BeginEcb.SetComponent(chunkIndex, smallAsteroidEntity, new LocalTransform
                    {
                        Position = transform.Position + new float3(velocityVector.x, velocityVector.y, 0) / 2,
                        Rotation = quaternion.identity,
                        Scale = 1f
                    });
                    BeginEcb.SetComponent(chunkIndex, smallAsteroidEntity, new PhysicsVelocity
                    {
                        Linear = new float3(velocityVector.x, velocityVector.y, 0),
                        Angular = velocity.Angular
                    });
                }
            }
        }
    }
}