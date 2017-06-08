using MvvmCross.Core.ViewModels;

namespace BasicScanning.Core
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IScannerService _scannerService;
        private string _status;
        private string _data;
        private bool _statusIsFocused;
        private bool _dataIsFocused;

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

            _scannerService.ScannerStatusChanged += (sender, args) =>
            {
                Status = args.Status;
            };

            _scannerService.ScannerDataChanged += (sender, args) =>
            {
                if (DataIsFocused)
                    Data = args.Data;
            };
        }
    }
}
