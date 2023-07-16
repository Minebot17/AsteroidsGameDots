using Asteroids.Utils.Extension_Methods.RX;
using UnityEngine;
using VContainer;

namespace Asteroids.UI.Core
{
    public class ChildrenSingletonViewsBinder : MonoBehaviour
    {
        [Inject]
        public void Construct(IObjectResolver container)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.FindAndBindViewModelFor(container);
            }
        }
    }
}