using Unity.Entities;

namespace Asteroids.ECS.Components
{
    public struct ScoreData : IComponentData
    {
        public int Score;
    }
}