using Unity.Entities;

namespace Asteroids.ECS.Components
{
    public struct NeedToDestroyAfterTimeData : IComponentData
    {
        public float TimeWhenNeedToDestroy;
    }
}