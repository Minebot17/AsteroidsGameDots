using Asteroids.ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace Asteroids.ECS.Bakers
{
    public class BulletAuthoring : MonoBehaviour
    {
        public class BulletBaker : Baker<BulletAuthoring>
        {
            public override void Bake(BulletAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<BulletTag>(entity);
                AddComponent<NeedToDestroyAfterTimeData>(entity);
            }
        }
    }
}