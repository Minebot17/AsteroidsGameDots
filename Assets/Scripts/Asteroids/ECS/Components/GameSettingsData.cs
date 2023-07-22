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
        
        public float BigAsteroidsVelocity;
        public float BigAsteroidsTorque;
        public float BigAsteroidsFragmentsCount;
        public float MaxBigAsteroidsCount;
        public float SpawnBigAsteroidsPeriod;

        public float BulletFireCooldown;
        public float BulletLifeDuration;
        public float BulletVelocity;

        public int ScoreBigAsteroid;
        public int ScoreSmallAsteroid;
    }
}