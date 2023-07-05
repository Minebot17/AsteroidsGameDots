using Asteroids.Utils.Extension_Methods;
using UnityEngine;
using VContainer;
using Logger = Asteroids.Utils.Logger;

namespace Asteroids.UI.Core
{
    public class SingletonViewsBinder : MonoBehaviour
    {
        private IObjectResolver _container;

        [Inject]
        public void Construct(IObjectResolver container)
        {
            _container = container;
        }

        private void Start()
        {
            var components = GetComponents<Component>();
            foreach (var component in components)
            {
                var viewType = component.GetType();
                if (viewType.IsAssignableFromDefinition(typeof(BindableView<>), out var genericTypes))
                {
                    var actualViewModelType = genericTypes[0];
                    var bindToMethod = viewType.GetMethod("BindTo", new[] { actualViewModelType });
                    bindToMethod.Invoke(component, new [] { _container.Resolve(actualViewModelType) }); // ignore disposable because already in scene context
                    return;
                }
            }
            
            Logger.DebugLogError(this, "BindableView wasn't found in View's GO");
        }
    }
}