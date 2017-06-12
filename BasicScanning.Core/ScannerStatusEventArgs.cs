namespace BasicScanning.Core
{
    public class ScannerStatusEventArgs
    {
        #region Public Properties

        public string Status { get; }

        #endregion Public Properties

        #region Public Constructors

        public ScannerStatusEventArgs(string status)
        {
            Status = status;
        }

        #endregion Public Constructors
    }
}