using System;
using UniRx;
using UnityEngine;

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
    }
}