namespace Asteroids.Hybrid
{
    public interface ISimulationToggler
    {
        bool SimulationIsEnabled { get; }
        
        void ToggleSimulation(bool isEnable);
    }
}