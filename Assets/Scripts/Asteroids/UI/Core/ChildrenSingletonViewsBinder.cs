using Asteroids.Utils.Extension_Methods.RX;
using UnityEngine;
using VContainer;

namespace Asteroids.UI.Core
{
    public class ChildrenSingletonViewsBinder : MonoBehaviour
    {
        private IObjectResolver _container;

        [Inject]
        public void Construct(IObjectResolver container)
        {
            _container = container;
        }

        private void Start()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.FindAndBindViewModelFor(_container);
            }
        }
    }
}