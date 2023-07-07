using Asteroids.ECS.Components;
using Unity.Burst;
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
            new TeleporterJob {MapSize = mapSize}.ScheduleParallel();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }

    [BurstCompile]
    public partial struct TeleporterJob : IJobEntity
    {
        public MapSizeData MapSize;

        private void Execute(ref LocalTransform transform)
        {
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