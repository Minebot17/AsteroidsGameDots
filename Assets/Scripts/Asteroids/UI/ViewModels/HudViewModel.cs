using Asteroids.UI.Core;
using Asteroids.Utils.Extension_Methods.RX;
using Asteroids.Utils.Reflection;
using UniRx;

namespace Asteroids.UI.ViewModels
{
    [SingletonViewModel]
    public class HudViewModel : DisposableObject, IPanelViewModel
    {
        public PanelType PanelType => PanelType.Hud;
        public IReactiveProperty<bool> IsOpened { get; } = new ReactiveProperty<bool>();
    }
}