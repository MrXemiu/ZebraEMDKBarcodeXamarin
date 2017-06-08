using Android.App;
using Android.OS;
using BasicScanning.Core;
using MvvmCross.Droid.Views;
using Symbol.XamarinEMDK;

namespace BasicScanningTutorial
{
    [Activity(Label = "BasicScanningTutorial", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : MvxActivity<MainViewModel>, EMDKManager.IEMDKListener
    {

        //private TextView _statusView;
        //private TextView _dataView;

        //private EMDKManager _emdkManager;
        //private BarcodeManager _barcodeManager;
        //private Scanner _scanner;
        private ZebraScannerService _scannerService;


        /// <inheritdoc />
        public void OnClosed()
        {
            //_statusView.Text = "Status:  EMDK Open() failed unexpectedly.";

            //if (_emdkManager != null)
            //{
            //    _emdkManager.Release();
            //    _emdkManager = null;
            //}
        }


        /// <inheritdoc />
        public void OnOpened(EMDKManager emdkManager)
        {
            //_statusView.Text = "Status:  EMDK Open() succeeded ...";

            //_emdkManager = emdkManager;

            //InitScanner();
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            //_statusView = FindViewById<TextView>(Resource.Id.statusView);
            //_dataView = FindViewById<TextView>(Resource.Id.DataView);

            //EMDKResults results = EMDKManager.GetEMDKManager(Android.App.Application.Context, this);

            //if (results.StatusCode != EMDKResults.STATUS_CODE.Success)
            //{
            //    _statusView.Text = "Status:  EMDKManager object creation failed ...";
            //}
            //else
            //{
            //    _statusView.Text = "Status:  EMDKManager object creation succeeded ...";
            //}
        }


        /// <inheritdoc />
        protected override void OnResume()
        {
            base.OnResume();
            //InitScanner();
        }


        protected override void OnPause()
        {
            base.OnPause();
            //DeInitScanner();
        }


        /// <inheritdoc />
        protected override void OnDestroy()
        {
            base.OnDestroy();

            //if (_emdkManager != null)
            //{
            //    _emdkManager.Release();
            //    _emdkManager = null;
            //}
        }


        //private void DisplayStatus(string status)
        //{
        //    if (Looper.MainLooper.Thread == Java.Lang.Thread.CurrentThread())
        //    {
        //        _statusView.Text = status;
        //    }
        //    else
        //    {
        //        RunOnUiThread(() => _statusView.Text = status);
        //    }
        //}


        //private void DisplayData(string data)
        //{
        //    if (Looper.MainLooper.Thread == Java.Lang.Thread.CurrentThread())
        //    {
        //        _dataView.Text += $"{data}{System.Environment.NewLine}";
        //    }
        //    else
        //    {
        //        RunOnUiThread(() => _dataView.Text += $"{data}{System.Environment.NewLine}");
        //    }
        //}


        //private void InitScanner()
        //{
        //    try
        //    {
        //        _barcodeManager = (BarcodeManager)_emdkManager.GetInstance(EMDKManager.FEATURE_TYPE.Barcode);
        //        _scanner = _barcodeManager.GetDevice(BarcodeManager.DeviceIdentifier.Default);

        //        if (_scanner != null)
        //        {

        //            _scanner.Data += Scanner_Data;
        //            _scanner.Status += Scanner_Status;

        //            _scanner.Enable();

        //            var config = _scanner.GetConfig();
        //            config.SkipOnUnsupported = ScannerConfig.SkipOnUnSupported.None;
        //            config.ScanParams.DecodeLEDFeedback = true;
        //            config.ReaderParams.ReaderSpecific.ImagerSpecific.PickList = ScannerConfig.PickList.Enabled;
        //            config.DecoderParams.Code39.Enabled = true;
        //            config.DecoderParams.Code128.Enabled = false;

        //            _scanner.SetConfig(config);
        //        }
        //        else
        //        {
        //            DisplayStatus("Failed to enable scanner.\n");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        DisplayStatus("Error: " + ex.Message);
        //    }
        //}


        //private void Scanner_Data(object sender, Scanner.DataEventArgs e)
        //{
        //    ScanDataCollection scanDataCollection = e.P0;

        //    if ((scanDataCollection != null) && (scanDataCollection.Result == ScannerResults.Success))
        //    {
        //        var scanData = scanDataCollection.GetScanData();

        //        foreach (var data in scanData)
        //        {
        //            DisplayData($"{data.LabelType}:  {data.Data}");
        //        }
        //    }
        //}


        //private void Scanner_Status(object sender, Scanner.StatusEventArgs e)
        //{
        //    string statusStr = string.Empty;

        //    StatusData.ScannerStates state = e.P0.State;

        //    if (state == StatusData.ScannerStates.Idle)
        //    {
        //        statusStr = "Scanner is idle and ready to submit read.";
        //        try
        //        {
        //            if (_scanner.IsEnabled && !_scanner.IsReadPending)
        //            {
        //                _scanner.Read();
        //            }
        //        }
        //        catch (ScannerException ex)
        //        {
        //            statusStr = ex.Message;
        //        }
        //    }

        //    if (state == StatusData.ScannerStates.Waiting) statusStr = "Waiting for Trigger Press to sccan";

        //    if (state == StatusData.ScannerStates.Scanning) statusStr = "Scanning in progress ...";

        //    if (state == StatusData.ScannerStates.Disabled) statusStr = "Scanner disabled";

        //    if (state == StatusData.ScannerStates.Error) statusStr = "Error occured during scanning";

        //    DisplayStatus(statusStr);
        //}


        //private void DeInitScanner()
        //{
        //    if (_emdkManager != null)
        //    {

        //        if (_scanner != null)
        //        {
        //            try
        //            {
        //                _scanner.Data -= Scanner_Data;
        //                _scanner.Status -= Scanner_Status;
        //                _scanner.Disable();
        //            }
        //            catch (ScannerException ex)
        //            {
        //                Log.Debug(this.Class.SimpleName, $"Exception:  {ex.Result.Description}");
        //            }
        //        }

        //        if (_barcodeManager != null)
        //        {
        //            _emdkManager.Release(EMDKManager.FEATURE_TYPE.Barcode);
        //        }

        //        _barcodeManager = null;
        //        _scanner = null;
        //    }
        //}

    }
}

