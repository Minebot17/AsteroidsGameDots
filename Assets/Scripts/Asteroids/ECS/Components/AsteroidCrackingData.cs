using Unity.Entities;

namespace Asteroids.ECS.Components
{
    public struct AsteroidCrackingData : IComponentData
    {
        public Entity SmallAsteroidPrefab;
        public int FragmentsCount;
    }
}