using Asteroids.ECS.Systems;
using Unity.Entities;

namespace Asteroids.Hybrid
{
    public class SimulationToggler : ISimulationToggler
    {
        public bool SimulationIsEnabled { get; private set; } = true;

        public void ToggleSimulation(bool isEnable)
        {
            SimulationIsEnabled = isEnable;
            World.DefaultGameObjectInjectionWorld.Unmanaged.GetExistingSystemState<MapBordersTeleporterSystem>()
                .Enabled = isEnable;
        }
    }
}