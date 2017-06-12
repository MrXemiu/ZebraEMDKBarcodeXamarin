using System;

namespace BasicScanning.Core
{
    public class NullScannerService : IScannerService
    {
        #region Public Events

        /// <inheritdoc />
        public event EventHandler<ScannerStatusEventArgs> ScannerStatusChanged;

        /// <inheritdoc />
        public event EventHandler<ScannerDataEventArgs> ScannerDataChanged;

        #endregion Public Events
    }
}