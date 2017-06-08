using Android.Content;
using Android.Views;
using BasicScanning.Core;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;

namespace BasicScanningTutorial
{
    public class Setup : MvxAndroidSetup
    {
        /// <inheritdoc />
        public Setup(Context applicationContext) : base(applicationContext){}


        /// <inheritdoc />
        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            registry.RegisterCustomBindingFactory<View>("IsFocused", view => new FocusChangedTargetBinding(view));
            base.FillTargetFactories(registry);
        }
    }
}