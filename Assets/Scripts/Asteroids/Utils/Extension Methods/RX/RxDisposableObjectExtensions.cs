using System;
using UniRx;
using UnityEngine;

namespace Asteroids.Utils.Extension_Methods.RX
{
    public class DisposableObject : IDisposable, DisposableObject.IAdd
    {
        public interface IAdd
        {
            public void Add(IDisposable disposable);
        }

        public bool IsDisposed { get; private set; }

        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public virtual void Dispose() // TODO: Check if VContainer is handle this
        {
            if (IsDisposed) return;
            IsDisposed = true;

            _disposables.Dispose();
        }

        void IAdd.Add(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }
    }

    public static class RxDisposableObjectExtensions
    {
        public static T AddTo<T>(this T disposable, DisposableObject.IAdd disposableObject) where T : IDisposable
        {
            disposableObject.Add(disposable);
            return disposable;
        }

        public static IDisposable CreateDestroyDisposable(this GameObject gameObject)
        {
            return Disposable.CreateWithState(gameObject, obj =>
            {
                if (obj != null)
                    UnityEngine.Object.Destroy(obj);
            });
        }
    }
}