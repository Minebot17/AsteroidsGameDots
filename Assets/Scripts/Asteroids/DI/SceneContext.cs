using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids.DI
{
    public class SceneContext : MonoBehaviour, ISceneContext
    {
        [SerializeField] private List<GameObject> _autoInject;

        private IObjectResolver _container;

        private void Awake()
        {
            ISceneContext.CurrentContext = this;
        }

        public void Initialize(IObjectResolver container)
        {
            _container = container;
            AutoInjectAll();
        }
        
        private void AutoInjectAll()
        {
            if (_autoInject == null)
                return;

            foreach (var target in _autoInject)
            {
                if (target != null)
                {
                    _container.InjectGameObject(target);
                }
            }
        }
    }
}