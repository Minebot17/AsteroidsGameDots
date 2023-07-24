using Asteroids.ECS.Components;
using Asteroids.Utils;
using Unity.Entities;
using UnityEngine;

namespace Asteroids.ECS.Bakers
{
    public class AsteroidAuthoring : MonoBehaviour
    {
        public GameSettingAsset GameSettingAsset;
        
        public class AsteroidBaker : Baker<AsteroidAuthoring>
        {
            public override void Bake(AsteroidAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<AsteroidTag>(entity);
                AddComponent(entity, new ScoreAddOnDestroyData
                {
                    Value = authoring.GameSettingAsset.GameSettingsData.ScoreBigAsteroid
                });
            }
        }
    }
}