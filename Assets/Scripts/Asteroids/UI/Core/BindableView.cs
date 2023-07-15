using System;
using UniRx;
using UnityEngine;

namespace Asteroids.UI.Core
{
    public abstract class BindableView<TViewModel> : MonoBehaviour
    {
        private CompositeDisposable _disposables = new();

        public TViewModel ViewModel => _viewModel.Value;

        private readonly ReactiveProperty<TViewModel> _viewModel = new();

        public IDisposable BindTo(TViewModel viewModel)
        {
            _disposables.Dispose();
            _disposables = new CompositeDisposable();
            _disposables.AddTo(this);

            _viewModel.Value = viewModel;

            OnBind(_disposables);

            return _disposables;
        }

        public IObservable<TViewModel> ObserveViewModel(bool includeCurrent = true)
        {
            return includeCurrent ? _viewModel.StartWith(ViewModel) : _viewModel;
        }

        protected abstract void OnBind(CompositeDisposable disposables);
    }
}