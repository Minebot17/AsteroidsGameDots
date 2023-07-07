using Asteroids.ECS.Components;
using Asteroids.Utils;
using Asteroids.Utils.Extension_Methods;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Asteroids.Hybrid
{
    [RequireComponent(typeof(Camera))]
    public class MapSizeInitializer : MonoBehaviour
    {
        [SerializeField] private GameSettingAsset _gameSettingAsset;
        
        private async void Awake()
        {
            var cam = GetComponent<Camera>();
            var upperRightCameraPoint = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
            var borderOffset = _gameSettingAsset.GameSettingsData.TeleporterBorderOffset;
            var mapSize = new float2(upperRightCameraPoint.x * 2f, upperRightCameraPoint.y * 2f);
            
            await UniTaskExtensions.WaitUntilWorldInited();
            World.DefaultGameObjectInjectionWorld.EntityManager.CreateSingleton(new MapSizeData
            {
                MapSize = mapSize,
                MinBorder = new float2(
                    -mapSize.x / 2f - borderOffset, 
                    -mapSize.y / 2f - borderOffset),
                MaxBorder = new float2(
                    mapSize.x / 2f + borderOffset, 
                    mapSize.y / 2f + borderOffset)
            });
        }
    }
}