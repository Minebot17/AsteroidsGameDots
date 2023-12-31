﻿using Asteroids.Utils.Extension_Methods.RX;
using UnityEngine;
using VContainer;

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
            gameObject.FindAndBindViewModelFor(_container);
        }
    }
}