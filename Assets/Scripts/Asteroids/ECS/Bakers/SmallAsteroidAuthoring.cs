using Asteroids.ECS.Components;
using Asteroids.Utils;
using Unity.Entities;
using UnityEngine;

namespace Asteroids.ECS.Bakers
{
    public class SmallAsteroidAuthoring : MonoBehaviour
    {
        public GameSettingAsset GameSettingAsset;
        
        public class SmallAsteroidBaker : Baker<SmallAsteroidAuthoring>
        {
            public override void Bake(SmallAsteroidAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<SmallAsteroidTag>(entity);
                AddComponent(entity, new ScoreAddOnDestroyData
                {
                    Value = authoring.GameSettingAsset.GameSettingsData.ScoreSmallAsteroid
                });
            }
        }
    }
}