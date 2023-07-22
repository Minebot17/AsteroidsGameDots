using Asteroids.ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace Asteroids.ECS.Bakers
{
    public class SmallAsteroidAuthoring : MonoBehaviour
    {
        public class SmallAsteroidBaker : Baker<SmallAsteroidAuthoring>
        {
            public override void Bake(SmallAsteroidAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<SmallAsteroidTag>(entity);
            }
        }
    }
}