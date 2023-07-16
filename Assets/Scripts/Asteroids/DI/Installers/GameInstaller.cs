using Asteroids.Hybrid;
using Asteroids.UI.Core;
using Asteroids.Utils.Reflection;
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

            foreach (var type in typeof(SingletonViewModel).GetTypesWithAttribute())
            {
                builder.Register(type, Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            }
        }
    }
}