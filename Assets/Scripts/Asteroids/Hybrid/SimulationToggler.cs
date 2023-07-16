using System;
using System.Collections.Generic;
using Asteroids.ECS.Systems;
using Unity.Entities;
using Unity.Physics.Systems;

namespace Asteroids.Hybrid
{
    public class SimulationToggler : ISimulationToggler
    {
        private readonly Type[] _systemsToToggleTypes = new Type[]
        {
            
        };
        
        private List<ComponentSystemBase> _systemsToToggle;
        
        public bool SimulationIsEnabled { get; private set; } = true;
        

        public void ToggleSimulation(bool isEnable)
        {
            SimulationIsEnabled = isEnable;

            if (_systemsToToggle == null)
            {
                _systemsToToggle = new List<ComponentSystemBase>();
                foreach (var toggleType in _systemsToToggleTypes)
                {
                    _systemsToToggle.Add(World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged(toggleType));
                }
            }

            foreach (var system in _systemsToToggle)
            {
                system.Enabled = isEnable;
            }
            
            var unmanaged = World.DefaultGameObjectInjectionWorld.Unmanaged;
            unmanaged.GetExistingSystemState<BigAsteroidsSpawnerSystem>().Enabled = isEnable;
            unmanaged.GetExistingSystemState<PlayerMovementSystem>().Enabled = isEnable;
            unmanaged.GetExistingSystemState<ExportPhysicsWorld>().Enabled = isEnable;
        }
    }
}