using System;
using System.Reflection;
using Asteroids.Utils.Extension_Methods;
using UnityEngine;
using VContainer.Unity;
using Logger = Asteroids.Utils.Logger;

namespace Asteroids.DI
{
    [RequireComponent(typeof(ISceneContext))]
    public class SceneAutoInitializer : MonoBehaviour
    {
        [SerializeField] private string _installerClassName;

        private void Awake()
        {
            var installerType = Assembly.GetExecutingAssembly().GetType(_installerClassName, false);
            if (installerType == null)
            {
                Logger.DebugLogError(this, $"Installer with name of {_installerClassName} wasn't found");
                return;
            }
            
            var installerConstructor = installerType.GetConstructor(Array.Empty<Type>());
            if (installerConstructor == null)
            {
                Logger.DebugLogError(this, $"Installer {_installerClassName} must have an empty constructor");
                return;
            }
            
            var installerObj = installerConstructor.Invoke(Array.Empty<object>());
            if (installerObj is not IInstaller installer)
            {
                Logger.DebugLogError(this, $"Installer {_installerClassName} must have an IInstaller interface");
                return;
            }

            var sceneContext = GetComponent<ISceneContext>();
            sceneContext.ManualInitialize(installer.BuildContainer(sceneContext));
        }
    }
}