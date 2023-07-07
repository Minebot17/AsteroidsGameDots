using Unity.Entities;
using Unity.Mathematics;

namespace Asteroids.ECS.Components
{
    public struct RandomData : IComponentData
    {
        public Random Random;
        
        public RandomData(uint seed)
        {
            Random = new Random(seed);
        }
    }
}