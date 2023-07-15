namespace Asteroids.UI.Core
{
    public interface IPanelManager
    {
        void Show(PanelType panelType);
        void Hide(PanelType panelType);
    }
}