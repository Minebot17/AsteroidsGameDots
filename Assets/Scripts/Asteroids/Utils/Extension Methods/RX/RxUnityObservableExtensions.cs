using System;
using Asteroids.UI.Core;
using UniRx;
using UnityEngine;
using VContainer;

namespace Asteroids.Utils.Extension_Methods.RX
{
    public static class RxUnityObservableExtensions
    {
        public static IObservable<GameObject> SelectGameObject<T>(this IObservable<T> observable) where T : Component =>
            observable.Select(c => c.gameObject);

        public static IObservable<TComponent> SelectComponent<TComponent>(this IObservable<GameObject> observable)
            where TComponent : Component =>
            observable.Select(g => g.GetComponent<TComponent>());

        public static IObservable<TResult> SelectComponentInChildren<TResult>
        (
            this IObservable<GameObject> observable,
            bool includeInactive = false
        )
            where TResult : Component =>
            observable.Select(c => c.GetComponentInChildren<TResult>(includeInactive));
        
        public static void FindAndBindViewModelFor(this GameObject go, IObjectResolver container)
        {
            var components = go.GetComponents<Component>();
            foreach (var component in components)
            {
                var viewType = component.GetType();
                if (viewType.IsAssignableFromDefinition(typeof(BindableView<>), out var genericTypes))
                {
                    var actualViewModelType = genericTypes[0];
                    var bindToMethod = viewType.GetMethod("BindTo", new[] { actualViewModelType });
                    bindToMethod.Invoke(component, new [] { container.Resolve(actualViewModelType) }); // ignore disposable because already in scene context
                    return;
                }
            }
            
            Logger.DebugLogError(null, "BindableView wasn't found in View's GO");
        }

        public static void BindPanel<T, TPanel>(this T panel, TPanel viewModel, CompositeDisposable disposables) 
            where T : BindableView<TPanel>
            where TPanel : IPanelViewModel
        {
            viewModel.IsOpened
                .Subscribe(value => panel.gameObject.SetActive(value))
                .AddTo(disposables);
        }
    }
}