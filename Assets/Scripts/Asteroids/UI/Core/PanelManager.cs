using System.Collections.Generic;
using System.Linq;

namespace Asteroids.UI.Core
{
    public class PanelManager : IPanelManager
    {
        private readonly Dictionary<PanelType, IPanelViewModel> _panels;

        public PanelManager(IEnumerable<IPanelViewModel> panels)
        {
            _panels = panels.ToDictionary(p => p.PanelType, p => p);
        }

        public void Show(PanelType panelType)
        {
            _panels[panelType].IsOpened.Value = true;
        }

        public void Hide(PanelType panelType)
        {
            _panels[panelType].IsOpened.Value = false;
        }
    }
}