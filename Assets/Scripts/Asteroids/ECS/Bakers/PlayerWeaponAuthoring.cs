using Asteroids.ECS.Components;
using Asteroids.Utils;
using Unity.Entities;
using UnityEngine;

namespace Asteroids.ECS.Bakers
{
    public class PlayerWeaponAuthoring : MonoBehaviour
    {
        public GameSettingAsset GameSettingAsset;
        public GameObject BulletPrefab;
        
        public class PlayerWeaponBaker : Baker<PlayerWeaponAuthoring>
        {
            public override void Bake(PlayerWeaponAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new PlayerWeaponData
                {
                    BulletPrefab = GetEntity(authoring.BulletPrefab, TransformUsageFlags.Dynamic),
                    BulletFireCooldown = authoring.GameSettingAsset.GameSettingsData.BulletFireCooldown,
                    BulletLifeDuration = authoring.GameSettingAsset.GameSettingsData.BulletLifeDuration,
                    BulletVelocity = authoring.GameSettingAsset.GameSettingsData.BulletVelocity,
                });
            }
        }
    }
}