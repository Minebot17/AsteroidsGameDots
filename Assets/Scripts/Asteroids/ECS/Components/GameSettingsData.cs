using System;
using Unity.Entities;

namespace Asteroids.ECS.Components
{
    [Serializable]
    public struct GameSettingsData : IComponentData
    {
        public float PlayerMovingSpeed;
        public float PlayerRotationSpeed;
        public float PlayerDragModifier;
        public float PlayerAngularDragModifier;

        public float TeleporterBorderOffset;
    }
}