using Android.Content;
using Android.Views;
using Autofac;
using Autofac.Extras.MvvmCross;
using BasicScanning.Core;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;

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


        /// <inheritdoc />
        protected override IMvxIoCProvider CreateIocProvider()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ZebraScannerService>().As<IScannerService>();
            var container = builder.Build();
            return new AutofacMvxIocProvider(container);
        }
    }
}