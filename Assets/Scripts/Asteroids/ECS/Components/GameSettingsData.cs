using System;
using Asteroids.Utils;
using Unity.Entities;

namespace Asteroids.ECS.Components
{
    [Serializable]
    public struct GameSettingsData : IComponentData
    {
        public float PlayerMovingSpeed;
        public float PlayerRotationSpeed;
        public float PlayerDragModifier;
    }
}