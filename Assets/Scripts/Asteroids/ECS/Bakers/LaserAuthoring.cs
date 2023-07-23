using Asteroids.ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace Asteroids.ECS.Bakers
{
    public class LaserAuthoring : MonoBehaviour
    {
        public class LaserBaker : Baker<LaserAuthoring>
        {
            public override void Bake(LaserAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<LaserInitializeTag>(entity);
                AddComponent<NeedToDestroyAfterTimeData>(entity);
            }
        }
    }
}