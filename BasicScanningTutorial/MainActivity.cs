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
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
        }
    }
}

