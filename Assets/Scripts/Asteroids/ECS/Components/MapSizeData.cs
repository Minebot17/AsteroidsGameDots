using Unity.Entities;
using Unity.Mathematics;

namespace Asteroids.ECS.Components
{
    public struct MapSizeData : IComponentData
    {
        public float2 MapSize;
        public float2 MinBorder;
        public float2 MaxBorder;
    }
}