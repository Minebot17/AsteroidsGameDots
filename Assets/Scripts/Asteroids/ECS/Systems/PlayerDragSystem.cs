using Asteroids.ECS.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics;

namespace Asteroids.ECS.Systems
{
    public partial struct PlayerDragSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GameSettingsData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var settings = SystemAPI.GetSingleton<GameSettingsData>();
            
            foreach (var (damping, _) in SystemAPI.Query<RefRW<PhysicsDamping>, PlayerTag>())
            {
                if (damping.ValueRO.Angular != settings.PlayerAngularDragModifier)
                {
                    damping.ValueRW.Angular = settings.PlayerAngularDragModifier;
                }
                
                if (damping.ValueRO.Linear != settings.PlayerDragModifier)
                {
                    damping.ValueRW.Linear = settings.PlayerDragModifier;
                }
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}