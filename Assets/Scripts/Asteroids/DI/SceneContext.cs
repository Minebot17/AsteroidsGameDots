using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids.DI
{
    public class SceneContext : MonoBehaviour, ISceneContext
    {
        [SerializeField] private List<GameObject> _toInject;

        private void Awake()
        {
            ISceneContext.CurrentContext = this;

            if (ISceneContext.Container != null)
            {
                InjectAll();
            }
        }

        public void ManualInitialize(IObjectResolver container)
        {
            ISceneContext.Container = container;
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
                    ISceneContext.Container.InjectGameObject(target);
                }
            }
        }
    }
}