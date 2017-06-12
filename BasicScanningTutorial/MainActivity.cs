using Android.App;
using Android.OS;
using Android.Views;
using BasicScanning.Core;
using MvvmCross.Droid.Views;

namespace BasicScanningTutorial
{
    [Activity(Label = "BasicScanningTutorial", MainLauncher = true, Icon = "@drawable/icon", WindowSoftInputMode = SoftInput.StateHidden)]
    public class MainActivity : MvxActivity<MainViewModel>
    {
        #region Private Fields

        private ZebraScannerService _scannerService;

        #endregion Private Fields

        #region Protected Methods

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
        }

        #endregion Protected Methods
    }
}