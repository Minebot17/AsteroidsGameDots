using Unity.Entities;

namespace Asteroids.ECS.Components
{
    public struct PlayerWeaponData : IComponentData
    {
        public Entity BulletPrefab;
        public float BulletFireCooldown;
        public float BulletLifeDuration;
        public float BulletVelocity;

        public float BulletFireNextTime;
    }
}