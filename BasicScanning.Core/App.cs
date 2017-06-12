using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace BasicScanning.Core
{
    public class App : MvxApplication
    {
        #region Public Constructors

        public App()
        {
            Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<MainViewModel>());
            Mvx.RegisterSingleton<IScannerService>(new NullScannerService());
        }

        #endregion Public Constructors
    }
}