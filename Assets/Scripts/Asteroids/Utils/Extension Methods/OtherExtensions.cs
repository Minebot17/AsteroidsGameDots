using System;
using System.Collections.Generic;
using Asteroids.DI;
using UnityEngine;
using VContainer;
using VContainer.Diagnostics;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Asteroids.Utils.Extension_Methods
{
    public static class OtherExtensions
    {
        public static TObject Nullable<TObject>(this TObject obj) where TObject : UnityEngine.Object
        {
            if (obj is null) return null;
            return !obj ? null : obj;
        }

        public static bool IsUnityNull<T>(this T obj) where T : class
        {
            if (obj is Object uObj)
            {
                return uObj.Nullable() is null;
            }

            return obj is null;
        }

        public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> pair, out TKey key, out TValue value)
        {
            key = pair.Key;
            value = pair.Value;
        }
        
        public static bool IsAssignableFromDefinition(this Type extendType, Type baseType, out Type[] genericTypes)
        {
            Type[] lastGenericType = null;
            while (!baseType.IsAssignableFrom(extendType))
            {
                if (extendType == typeof(object))
                {
                    genericTypes = Array.Empty<Type>();
                    return false;
                }
                if (extendType.IsGenericType && !extendType.IsGenericTypeDefinition)
                {
                    lastGenericType = extendType.GetGenericArguments();
                    extendType = extendType.GetGenericTypeDefinition();
                }
                else
                {
                    extendType = extendType.BaseType;
                }
            }

            genericTypes = lastGenericType;
            return true;
        }

        public static IObjectResolver BuildContainer(this IInstaller installer, ISceneContext sceneContext)
        {
            var contextObject = sceneContext as MonoBehaviour;
            var builder = new ContainerBuilder
            {
                ApplicationOrigin = contextObject,
                Diagnostics = VContainerSettings.DiagnosticsEnabled ? DiagnositcsContext.GetCollector(contextObject.name) : null,
            };
            installer.Install(builder);
            builder.RegisterInstance(sceneContext).AsSelf();
            EntryPointsBuilder.EnsureDispatcherRegistered(builder);
            return builder.Build();
        }
    }
}