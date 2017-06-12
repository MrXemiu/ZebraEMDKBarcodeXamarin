namespace BasicScanning.Core
{
    public class ScannerDataEventArgs
    {
        #region Public Properties

        public ScannerLabelType LabelType { get; }

        public string Data { get; }

        #endregion Public Properties

        #region Public Constructors

        public ScannerDataEventArgs(ScannerLabelType labelType, string data)
        {
            LabelType = labelType;
            Data = data;
        }

        #endregion Public Constructors
    }
}