using System;
using Asteroids.ECS.Components;
using UnityEngine;

namespace Asteroids.Utils
{
    [Serializable]
    [CreateAssetMenu(fileName = "GameSettingsAsset", menuName = "ScriptableObjects/GameSettingsAsset")]
    public class GameSettingAsset : ScriptableObject
    {
        [SerializeField] private GameSettingsData _gameSettingsData;

        public GameSettingsData GameSettingsData => _gameSettingsData;
    }
}