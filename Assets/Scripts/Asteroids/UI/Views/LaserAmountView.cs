using System.Collections.Generic;
using Asteroids.UI.Core;
using Asteroids.UI.ViewModels;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.UI.Views
{
    public class LaserAmountView : BindableView<LaserAmountViewModel>
    {
        [SerializeField] private GameObject _laserCellPrefab;
        
        private readonly List<Image> _images = new ();

        protected override void OnBind(CompositeDisposable disposables)
        {
            ViewModel.ChargesStates.ObserveAdd().Subscribe(OnChargeAdded).AddTo(disposables);
            ViewModel.ChargesStates.ObserveReplace().Subscribe(OnChargeChanged).AddTo(disposables);
        }

        public void OnChargeAdded(CollectionAddEvent<float> e)
        {
            var laserCell = Instantiate(_laserCellPrefab, transform);
            _images.Add(laserCell.GetComponent<Image>());
        }

        public void OnChargeChanged(CollectionReplaceEvent<float> e)
        {
            _images[e.Index].fillAmount = e.NewValue;
        }
    }
}