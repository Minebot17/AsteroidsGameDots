using Asteroids.Utils;
using UnityEngine;
using Unity.Entities;

namespace Asteroids.ECS.Bakers
{
    public class GameSettingsAuthoring : MonoBehaviour
    {
        [SerializeField] private GameSettingAsset _gameSettingAsset;
        
        public class GameSettingsBaker : Baker<GameSettingsAuthoring>
        {
            public override void Bake(GameSettingsAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, authoring._gameSettingAsset.GameSettingsData);
            }
        }
    }
}