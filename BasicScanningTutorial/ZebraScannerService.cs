using Android.App;
using Android.OS;
using BasicScanning.Core;
using MvvmCross.Core.Platform;
using MvvmCross.Droid.Platform;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using Symbol.XamarinEMDK;
using Symbol.XamarinEMDK.Barcode;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Java.Lang;
using Exception = System.Exception;

namespace BasicScanningTutorial
{
    public class ZebraScannerService : Java.Lang.Object, EMDKManager.IEMDKListener, IScannerService
    {
        #region Private Fields

        private static readonly Dictionary<ScanDataCollection.LabelType, ScannerLabelType> LabelDictionary = new Dictionary<ScanDataCollection.LabelType, ScannerLabelType>
        {
            {ScanDataCollection.LabelType.Auspostal,ScannerLabelType.AUSPOSTAL },
            {ScanDataCollection.LabelType.Aztec, ScannerLabelType.AZTEC },
            {ScanDataCollection.LabelType.Bookland,  ScannerLabelType.BOOKLAND},
            {ScanDataCollection.LabelType.Canpostal, ScannerLabelType.CANPOSTAL },
            {ScanDataCollection.LabelType.Chinese2of5, ScannerLabelType.CHINESE_2OF5 },
            {ScanDataCollection.LabelType.Codabar, ScannerLabelType.CODABAR },
            {ScanDataCollection.LabelType.Code11, ScannerLabelType.CODE11 },
            {ScanDataCollection.LabelType.Code128, ScannerLabelType.CODE128 },
            {ScanDataCollection.LabelType.Code32, ScannerLabelType.CODE32 },
            {ScanDataCollection.LabelType.Code39, ScannerLabelType.CODE39 },
            {ScanDataCollection.LabelType.Code93, ScannerLabelType.CODE93 },
            {ScanDataCollection.LabelType.CompositeAb, ScannerLabelType.COMPOSITE_AB },
            {ScanDataCollection.LabelType.CompositeC, ScannerLabelType.COMPOSITE_C },
            {ScanDataCollection.LabelType.Coupon, ScannerLabelType.COUPON },
            {ScanDataCollection.LabelType.D2of5, ScannerLabelType.D2OF5 },
            {ScanDataCollection.LabelType.DatabarCoupon, ScannerLabelType.DATABAR_COUPON },
            {ScanDataCollection.LabelType.Datamatrix, ScannerLabelType.DATAMATRIX },
            {ScanDataCollection.LabelType.Dutchpostal, ScannerLabelType.DUTCHPOSTAL },
            {ScanDataCollection.LabelType.Ean128, ScannerLabelType.EAN128 },
            {ScanDataCollection.LabelType.Ean13, ScannerLabelType.EAN13 },
            {ScanDataCollection.LabelType.Ean8, ScannerLabelType.EAN8 },
            {ScanDataCollection.LabelType.Gs1Databar, ScannerLabelType.GS1_DATABAR },
            {ScanDataCollection.LabelType.Gs1DatabarExp, ScannerLabelType.GS1_DATABAR_EXP },
            {ScanDataCollection.LabelType.Gs1DatabarLim, ScannerLabelType.GS1_DATABAR_LIM },
            {ScanDataCollection.LabelType.Hanxin, ScannerLabelType.HANXIN },
            {ScanDataCollection.LabelType.I2of5, ScannerLabelType.I2OF5 },
            {ScanDataCollection.LabelType.Iata2of5, ScannerLabelType.IATA2OF5 },
            {ScanDataCollection.LabelType.Isbt128, ScannerLabelType.ISBT128 },
            {ScanDataCollection.LabelType.Jappostal, ScannerLabelType.JAPPOSTAL },
            {ScanDataCollection.LabelType.Korean3of5, ScannerLabelType.KOREAN_3OF5 },
            {ScanDataCollection.LabelType.Mailmark, ScannerLabelType.MAILMARK },
            {ScanDataCollection.LabelType.Matrix2of5, ScannerLabelType.MATRIX_2OF5 },
            {ScanDataCollection.LabelType.Maxicode, ScannerLabelType.MAXICODE },
            {ScanDataCollection.LabelType.Micropdf, ScannerLabelType.MICROPDF },
            {ScanDataCollection.LabelType.Microqr, ScannerLabelType.MICROQR },
            {ScanDataCollection.LabelType.Msi, ScannerLabelType.MSI },
            {ScanDataCollection.LabelType.Ocr, ScannerLabelType.OCR},
            {ScanDataCollection.LabelType.Pdf417, ScannerLabelType.PDF417 },
            {ScanDataCollection.LabelType.Qrcode, ScannerLabelType.QRCODE },
            {ScanDataCollection.LabelType.Signature, ScannerLabelType.SIGNATURE },
            {ScanDataCollection.LabelType.Tlc39, ScannerLabelType.TLC39 },
            {ScanDataCollection.LabelType.Trioptic39, ScannerLabelType.TRIOPTIC39 },
            {ScanDataCollection.LabelType.Ukpostal, ScannerLabelType.UKPOSTAL },
            {ScanDataCollection.LabelType.Undefined, ScannerLabelType.UNDEFINED },
            {ScanDataCollection.LabelType.Upca, ScannerLabelType.UPCA },
            {ScanDataCollection.LabelType.Upce0, ScannerLabelType.UPCE0 },
            {ScanDataCollection.LabelType.Upce1, ScannerLabelType.UPCE1 },
            {ScanDataCollection.LabelType.Us4state, ScannerLabelType.US4STATE },
            {ScanDataCollection.LabelType.Us4stateFics, ScannerLabelType.US4STATE_FICS },
            {ScanDataCollection.LabelType.Usplanet, ScannerLabelType.USPLANET },
            {ScanDataCollection.LabelType.Uspostnet, ScannerLabelType.USPOSTNET }
        };

        private EMDKManager _emdkManager;

        private BarcodeManager _barcodeManager;

        private Scanner _scanner;

        #endregion Private Fields

        #region Internal Properties

        internal EMDKResults.STATUS_CODE CurrentStatus { get; set; }

        #endregion Internal Properties

        #region Private Properties

        private BarcodeManager BarcodeManager
        {
            get => _barcodeManager ?? (_barcodeManager = _emdkManager.GetInstance(EMDKManager.FEATURE_TYPE.Barcode) as BarcodeManager);
            set => _barcodeManager = value;
        }

        private Scanner Scanner
        {
            get => _scanner ?? (_scanner = BarcodeManager.GetDevice(BarcodeManager.DeviceIdentifier.Default));
            set => _scanner = value;
        }

        #endregion Private Properties

        #region Public Events

        public event EventHandler<ScannerStatusEventArgs> ScannerStatusChanged;

        public event EventHandler<ScannerDataEventArgs> ScannerDataChanged;

        #endregion Public Events

        #region Public Constructors

        public ZebraScannerService()
        {
            EMDKResults results = EMDKManager.GetEMDKManager(Application.Context.ApplicationContext, this);

            CurrentStatus = results.StatusCode;

            Console.WriteLine($"GetEMDKManager status: {results.StatusCode} - {results.StatusString}.  ");

            if (CurrentStatus != EMDKResults.STATUS_CODE.Success)
            {
                DisplayStatus(new ScannerStatusEventArgs("EMDKManager object creation failed."));
            }
            else
            {
                DisplayStatus(new ScannerStatusEventArgs("EMDKManager object creation succeeded."));
            }
        }

        #endregion Public Constructors

        #region Public Methods

        public static bool IsSupported()
        {
            try
            {
                return Java.Lang.Class.ForName("com.symbol.emdk.VersionManager") != null;
            }
            catch (ClassNotFoundException)
            {
                return false;
            }
        }

        public void OnPause()
        {
            DeInitScanner();
        }

        public void OnResume()
        {
            InitScanner();
        }

        /// <inheritdoc />
        public new void Dispose()
        {
            if (_emdkManager != null)
            {
                _emdkManager.Release();
                _emdkManager = null;
            }

            base.Dispose();
        }

        /// <inheritdoc />
        public void OnClosed()
        {
            DisplayStatus(new ScannerStatusEventArgs("EMDK Open() failed unexpectedly."));
            Dispose();
        }

        /// <inheritdoc />
        public void OnOpened(EMDKManager emdkManager)
        {
            DisplayStatus(new ScannerStatusEventArgs("Status:  EMDK Open() succeeded."));

            _emdkManager = emdkManager;

            InitScanner();
        }

        #endregion Public Methods

        #region Private Methods

        private void InitLifetimeEventHandling()
        {
            var activityLifetimeListener = Mvx.Resolve<IMvxAndroidActivityLifetimeListener>() as MvxLifetimeMonitor;

            if (activityLifetimeListener != null)
                activityLifetimeListener.LifetimeChanged += (sender, args) =>
                {
                    switch (args.LifetimeEvent)
                    {
                        case MvxLifetimeEvent.Launching:
                            Debugger.Break();
                            break;

                        case MvxLifetimeEvent.Closing:
                            Dispose();
                            break;

                        case MvxLifetimeEvent.Deactivated:
                            Debugger.Break();
                            break;
                    }
                    ;
                };
        }

        private void InitScanner()
        {
            try
            {
                InitLifetimeEventHandling();

                if (Scanner != null)
                {
                    Scanner.Data += Scanner_Data;
                    Scanner.Status += Scanner_Status;

                    Scanner.Enable();

                    var config = Scanner.GetConfig();
                    config.SkipOnUnsupported = ScannerConfig.SkipOnUnSupported.None;
                    config.ScanParams.DecodeLEDFeedback = true;
                    config.ReaderParams.ReaderSpecific.ImagerSpecific.PickList = ScannerConfig.PickList.Enabled;
                    config.DecoderParams.Code39.Enabled = true;
                    config.DecoderParams.Code128.Enabled = false;

                    Scanner.SetConfig(config);
                }
                else
                {
                    DisplayStatus(new ScannerStatusEventArgs("Failed to enable scanner.\n"));
                }
            }
            catch (Exception ex)
            {
                DisplayStatus(new ScannerStatusEventArgs("Error: " + ex.Message));
            }
        }

        private void Scanner_Data(object sender, Scanner.DataEventArgs e)
        {
            ScanDataCollection scanDataCollection = e.P0;

            if ((scanDataCollection == null) || (scanDataCollection.Result != ScannerResults.Success)) return;

            var scanData = scanDataCollection.GetScanData();

            foreach (var data in scanData)
            {
                DisplayData(new ScannerDataEventArgs(LabelDictionary[data.LabelType], data.Data));
            }
        }

        private void Scanner_Status(object sender, Scanner.StatusEventArgs e)
        {
            string statusStr = string.Empty;

            StatusData.ScannerStates state = e.P0.State;

            if (state == StatusData.ScannerStates.Idle)
            {
                statusStr = "Scanner is idle and ready to submit read.";
                try
                {
                    if (Scanner.IsEnabled && !Scanner.IsReadPending)
                    {
                        Scanner.Read();
                    }
                }
                catch (ScannerException ex)
                {
                    statusStr = ex.Message;
                }
            }

            if (state == StatusData.ScannerStates.Waiting) statusStr = "Waiting for trigger press to scan.";

            if (state == StatusData.ScannerStates.Scanning) statusStr = "Scanning in progress ...";

            if (state == StatusData.ScannerStates.Disabled) statusStr = "Scanner disabled";

            if (state == StatusData.ScannerStates.Error) statusStr = "Error occured during scanning.";

            DisplayStatus(new ScannerStatusEventArgs(statusStr));
        }

        private void DeInitScanner()
        {
            if (_emdkManager != null)
            {
                if (Scanner != null)
                {
                    Scanner.Data -= Scanner_Data;
                    Scanner.Status -= Scanner_Status;
                    Scanner.Disable();
                }

                if (BarcodeManager != null)
                {
                    _emdkManager.Release(EMDKManager.FEATURE_TYPE.Barcode);
                }

                BarcodeManager = null;
                Scanner = null;
            }
        }

        private void DisplayStatus(ScannerStatusEventArgs eventArgs)
        {
            if (Looper.MainLooper.Thread == Java.Lang.Thread.CurrentThread())
            {
                ScannerStatusChanged?.Invoke(this, eventArgs);
            }
            else
            {
                Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity.RunOnUiThread(() => ScannerStatusChanged?.Invoke(this, eventArgs));
            }
        }

        private void DisplayData(ScannerDataEventArgs eventArgs)
        {
            if (Looper.MainLooper.Thread == Java.Lang.Thread.CurrentThread())
            {
                ScannerDataChanged?.Invoke(this, eventArgs);
            }
            else
            {
                Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity.RunOnUiThread(() => ScannerDataChanged?.Invoke(this, eventArgs));
            }
        }

        #endregion Private Methods
    }
}