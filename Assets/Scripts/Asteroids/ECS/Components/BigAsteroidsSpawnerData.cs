using Unity.Entities;

namespace Asteroids.ECS.Components
{
    public struct BigAsteroidsSpawnerData : IComponentData
    {
        public Entity Prefab;
        public float MaxCount;
        public float SpawnPeriod;
        public float NextSpawnTime;
        public float StartVelocity;
        public float StartTorque;
    }
}