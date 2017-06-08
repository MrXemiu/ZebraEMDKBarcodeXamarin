using Android.App;
using Android.OS;
using BasicScanning.Core;
using MvvmCross.Droid.Views;

namespace BasicScanningTutorial
{
    [Activity(Label = "BasicScanningTutorial", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : MvxActivity<MainViewModel>
    {
        private ZebraScannerService _scannerService;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
        }
    }
}

