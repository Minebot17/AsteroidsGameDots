using Asteroids.UI.ViewModels;
using VContainer;
using VContainer.Unity;

namespace Asteroids.DI.Installers
{
    [Preserve]
    public class MainMenuInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<MainMenuViewModel>(Lifetime.Singleton);
        }
    }
}