﻿using Unity.Entities;
using Unity.Mathematics;

namespace Asteroids.ECS.Components
{
    public struct PlayerInputData : IComponentData
    {
        public float3 Move;
        public bool IsFire;
        public bool IsAltFire;
    }
}