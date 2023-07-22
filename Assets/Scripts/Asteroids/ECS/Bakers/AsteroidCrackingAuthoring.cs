using Asteroids.ECS.Components;
using Asteroids.Utils;
using Unity.Entities;
using UnityEngine;

namespace Asteroids.ECS.Bakers
{
    public class AsteroidCrackingAuthoring : MonoBehaviour
    {
        public GameSettingAsset GameSettingsAsset;
        public GameObject SmallAsteroidPrefab;
        
        public class AsteroidCrackingBaker : Baker<AsteroidCrackingAuthoring>
        {
            public override void Bake(AsteroidCrackingAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new AsteroidCrackingData
                {
                    SmallAsteroidPrefab = GetEntity(authoring.SmallAsteroidPrefab, TransformUsageFlags.Dynamic),
                    FragmentsCount = authoring.GameSettingsAsset.GameSettingsData.BigAsteroidsFragmentsCount
                });
            }
        }
    }
}