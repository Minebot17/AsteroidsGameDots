using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids.DI
{
    public class SceneContext : MonoBehaviour, ISceneContext
    {
        [SerializeField] private List<GameObject> _toInject;

        private IObjectResolver _container;

        private void Awake()
        {
            ISceneContext.CurrentContext = this;
        }

        public void Initialize(IObjectResolver container)
        {
            _container = container;
            InjectAll();
        }
        
        private void InjectAll()
        {
            if (_toInject == null)
                return;

            foreach (var target in _toInject)
            {
                if (target != null)
                {
                    _container.InjectGameObject(target);
                }
            }
        }
    }
}