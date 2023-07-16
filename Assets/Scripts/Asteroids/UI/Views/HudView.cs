using Asteroids.UI.Core;
using Asteroids.UI.ViewModels;
using Asteroids.Utils.Extension_Methods.RX;
using UniRx;

namespace Asteroids.UI.Views
{
    public class HudView : BindableView<HudViewModel>
    {
        protected override void OnBind(CompositeDisposable disposables)
        {
            this.BindPanel(ViewModel, disposables);
        }
    }
}