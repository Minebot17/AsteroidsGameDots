using Asteroids.ECS.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Transforms;

namespace Asteroids.ECS.Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    public partial struct PlayerMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GameSettingsData>();
            state.RequireForUpdate<PlayerInputData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var input = SystemAPI.GetSingleton<PlayerInputData>();
            var settings = SystemAPI.GetSingleton<GameSettingsData>();
            
            foreach (var (velocity, mass, transform, _) 
                     in SystemAPI.Query<RefRW<PhysicsVelocity>, PhysicsMass, LocalTransform, PlayerTag>())
            {
                if (input.Move.y != 0)
                {
                    velocity.ValueRW.ApplyLinearImpulse(mass, transform.Up() * input.Move.y * settings.PlayerMovingSpeed);
                }

                if (input.Move.x != 0)
                {
                    velocity.ValueRW.ApplyAngularImpulse(mass, transform.Forward() * -input.Move.x * settings.PlayerRotationSpeed);
                }
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}