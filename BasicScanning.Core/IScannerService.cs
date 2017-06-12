using System;

namespace BasicScanning.Core
{
    public interface IScannerService
    {
        #region Public Events

        event EventHandler<ScannerStatusEventArgs> ScannerStatusChanged;

        event EventHandler<ScannerDataEventArgs> ScannerDataChanged;

        #endregion Public Events
    }
}