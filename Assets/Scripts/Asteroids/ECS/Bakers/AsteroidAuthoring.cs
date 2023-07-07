using Asteroids.ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace Asteroids.ECS.Bakers
{
    public class AsteroidAuthoring : MonoBehaviour
    {
        public class AsteroidBaker : Baker<AsteroidAuthoring>
        {
            public override void Bake(AsteroidAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<AsteroidTag>(entity);
            }
        }
    }
}