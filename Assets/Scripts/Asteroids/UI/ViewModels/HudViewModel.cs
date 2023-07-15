using Asteroids.UI.Core;
using Asteroids.Utils.Extension_Methods.RX;
using UniRx;

namespace Asteroids.UI.ViewModels
{
    public class HudViewModel : DisposableObject, IPanelViewModel
    {
        public PanelType PanelType => PanelType.Hud;
        public IReactiveProperty<bool> IsOpened { get; } = new ReactiveProperty<bool>();
    }
}