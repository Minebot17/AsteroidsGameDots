using Asteroids.ECS.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace Asteroids.ECS.Systems
{
    public partial struct BigAsteroidsSpawnerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<RandomData>();
            state.RequireForUpdate<MapSizeData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var rnd = SystemAPI.GetSingletonRW<RandomData>();
            var mapSize = SystemAPI.GetSingleton<MapSizeData>().MapSize;
            var asteroidsCount = SystemAPI.QueryBuilder().WithAll<AsteroidTag>().Build().CalculateEntityCount();
            foreach (var spawner in SystemAPI.Query<RefRW<BigAsteroidsSpawnerData>>())
            {
                if (SystemAPI.Time.ElapsedTime < spawner.ValueRO.NextSpawnTime 
                    || asteroidsCount >= spawner.ValueRO.MaxCount)
                {
                    continue;
                }

                asteroidsCount++;
                spawner.ValueRW.NextSpawnTime = (float) SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnPeriod;
                
                var position = GetRandomPositionOnBorder(mapSize, rnd);
                var bigAsteroidEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
                state.EntityManager.SetComponentData(bigAsteroidEntity, new LocalTransform
                {
                    Position = position,
                    Rotation = quaternion.identity,
                    Scale = 1
                });
                state.EntityManager.SetComponentData(bigAsteroidEntity, new PhysicsVelocity
                {
                    Linear = rnd.ValueRW.Random.NextFloat3(new float3(-1, -1, 0), new float3(1, 1, 0)) * spawner.ValueRO.StartVelocity,
                    Angular = new float3(0, 0, (rnd.ValueRW.Random.NextBool() ? -1 : 1) * spawner.ValueRO.StartTorque)
                });
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
        
        private float3 GetRandomPositionOnBorder(float2 mapSize, RefRW<RandomData> rnd)
        {
            var randomEdge = rnd.ValueRW.Random.NextInt(0, 4);
            var randomPosition = float3.zero;
            var randomX = rnd.ValueRW.Random.NextFloat(-mapSize.x / 2f, mapSize.x / 2f);
            var randomY = rnd.ValueRW.Random.NextFloat(-mapSize.y / 2f, mapSize.y / 2f);
            
            switch (randomEdge)
            {
                case 0:
                    randomPosition = new float3(randomX, -mapSize.y / 2f, 0);
                    break;
                case 1:
                    randomPosition = new float3(mapSize.x / 2f, randomY, 0);
                    break;
                case 2:
                    randomPosition = new float3(randomX, mapSize.y / 2f, 0);
                    break;
                case 3:
                    randomPosition = new float3(-mapSize.x / 2f, randomY, 0);
                    break;
            }
            
            return randomPosition;
        }
    }
}