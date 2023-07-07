using Asteroids.ECS.Components;
using Asteroids.Utils;
using Unity.Entities;
using UnityEngine;

namespace Asteroids.ECS.Bakers
{
    public class BigAsteroidsSpawnerAuthoring : MonoBehaviour
    {
        public GameSettingAsset GameSettingAsset;
        public GameObject BigAsteroidPrefab;
        
        public class BigAsteroidsSpawnerBaker : Baker<BigAsteroidsSpawnerAuthoring>
        {
            public override void Bake(BigAsteroidsSpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new BigAsteroidsSpawnerData
                {
                    Prefab = GetEntity(authoring.BigAsteroidPrefab, TransformUsageFlags.Dynamic),
                    MaxCount = authoring.GameSettingAsset.GameSettingsData.MaxBigAsteroidsCount,
                    SpawnPeriod = authoring.GameSettingAsset.GameSettingsData.SpawnBigAsteroidsPeriod,
                    NextSpawnTime = 0,
                    StartVelocity = authoring.GameSettingAsset.GameSettingsData.BigAsteroidsVelocity,
                    StartTorque = authoring.GameSettingAsset.GameSettingsData.BigAsteroidsTorque
                });
            }
        }
    }
}