using System.Collections.Generic;
using Asteroids.UI.Core;
using UnityEngine;
using VContainer;

namespace Asteroids.Hybrid
{
    public class StartPanelEnabler : MonoBehaviour
    {
        [SerializeField] private List<PanelType> _panelsToEnableOnStart;

        private IPanelManager _panelManager;
        
        [Inject]
        public void Construct(IPanelManager panelManager)
        {
            _panelManager = panelManager;
        }

        private void Start()
        {
            foreach (var panelType in _panelsToEnableOnStart)
            {
                _panelManager.Show(panelType);
            }
        }
    }
}