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

        public Entity LaserPrefab;
        public float LaserFireCooldown;
        public float LaserLifeDuration;
        public float LaserFireNextTime;
        public int LaserMaxCharges;
        public int LaserCurrentCharges;
        public float LaserChargeCooldown;
        public float LaserChargeNextTime;
    }
}