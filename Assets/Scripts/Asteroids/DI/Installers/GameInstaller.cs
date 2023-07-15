using Asteroids.Hybrid;
using Asteroids.UI.Core;
using Asteroids.UI.ViewModels;
using VContainer;
using VContainer.Unity;

namespace Asteroids.DI.Installers
{
    public class GameInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<IPanelManager, PanelManager>(Lifetime.Singleton);
            builder.Register<ISimulationToggler, SimulationToggler>(Lifetime.Singleton);
            
            builder.Register<PauseViewModel>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<HudViewModel>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}