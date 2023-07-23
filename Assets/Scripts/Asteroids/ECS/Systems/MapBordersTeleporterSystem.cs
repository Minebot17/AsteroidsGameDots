using Asteroids.ECS.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Asteroids.ECS.Systems
{
    public partial struct MapBordersTeleporterSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<MapSizeData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var mapSize = SystemAPI.GetSingleton<MapSizeData>();
            new TeleporterJob
            {
                MapSize = mapSize,
                AsteroidGroup = SystemAPI.GetComponentLookup<AsteroidTag>(),
                SmallAsteroidGroup = SystemAPI.GetComponentLookup<SmallAsteroidTag>(),
                PlayerGroup = SystemAPI.GetComponentLookup<PlayerTag>(),
                BulletGroup = SystemAPI.GetComponentLookup<BulletTag>(),
            }.ScheduleParallel();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }

    [BurstCompile]
    public partial struct TeleporterJob : IJobEntity
    {
        [ReadOnly] public ComponentLookup<AsteroidTag> AsteroidGroup;
        [ReadOnly] public ComponentLookup<SmallAsteroidTag> SmallAsteroidGroup;
        [ReadOnly] public ComponentLookup<PlayerTag> PlayerGroup;
        [ReadOnly] public ComponentLookup<BulletTag> BulletGroup;
        public MapSizeData MapSize;

        private void Execute(ref LocalTransform transform, Entity entity)
        {
            if (!AsteroidGroup.HasComponent(entity)
                && !SmallAsteroidGroup.HasComponent(entity)
                && !PlayerGroup.HasComponent(entity)
                && !BulletGroup.HasComponent(entity))
            {
                return;
            }
            
            var pos = transform.Position;
            if (pos.x < MapSize.MinBorder.x)
            {
                transform = transform.WithPosition(MapSize.MaxBorder.x, pos.y, pos.z);
            }
            else if (pos.x > MapSize.MaxBorder.x)
            {
                transform = transform.WithPosition(MapSize.MinBorder.x, pos.y, pos.z);
            }
            else if (pos.y < MapSize.MinBorder.y)
            {
                transform = transform.WithPosition(pos.x, MapSize.MaxBorder.y, pos.z);
            }
            else if (pos.y > MapSize.MaxBorder.y)
            {
                transform = transform.WithPosition(pos.x, MapSize.MinBorder.y, pos.z);
            }
        }
    }
}