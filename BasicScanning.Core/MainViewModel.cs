using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.WeakSubscription;

namespace BasicScanning.Core
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IScannerService _scannerService;
        private string _status;
        private string _data;
        private bool _statusIsFocused;
        private bool _dataIsFocused;
        private MvxWeakEventSubscription<IScannerService, ScannerDataEventArgs> _dataToken;
        private MvxWeakEventSubscription<IScannerService, ScannerStatusEventArgs> _statusToken;

        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public bool StatusIsFocused
        {
            get => _statusIsFocused;
            set => SetProperty(ref _statusIsFocused, value);
        }

        public string Data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }

        public bool DataIsFocused
        {
            get => _dataIsFocused;
            set => SetProperty(ref _dataIsFocused, value);
        }


        public MainViewModel(IScannerService scannerService)
        {
            _scannerService = scannerService;

           _dataToken = _scannerService.WeakSubscribe<IScannerService, ScannerDataEventArgs>(nameof(IScannerService.ScannerDataChanged), (sender, args) =>
             {
                 if (DataIsFocused)
                     Data = args.Data;
             });

            _statusToken = _scannerService.WeakSubscribe<IScannerService, ScannerStatusEventArgs>(nameof(IScannerService.ScannerStatusChanged), (sender, args) =>
            {
                Status = args.Status;
            });

            //_scannerService.ScannerStatusChanged += (sender, args) =>
            //{
            //    Status = args.Status;
            //};

            //_scannerService.ScannerDataChanged += (sender, args) =>
            //{
            //    if (DataIsFocused)
            //        Data = args.Data;
            //};
        }


        /// <inheritdoc />
        public override void Appearing()
        {
            base.Appearing();
            DataIsFocused = true;
        }
    }
}
