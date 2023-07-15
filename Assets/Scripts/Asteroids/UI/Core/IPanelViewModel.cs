using UniRx;

namespace Asteroids.UI.Core
{
    public interface IPanelViewModel
    {
        PanelType PanelType { get; }
        IReactiveProperty<bool> IsOpened { get; }
    }
}