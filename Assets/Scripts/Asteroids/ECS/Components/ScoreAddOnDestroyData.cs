using Unity.Entities;

namespace Asteroids.ECS.Components
{
    public struct ScoreAddOnDestroyData : IComponentData
    {
        public int Value;
    }
}