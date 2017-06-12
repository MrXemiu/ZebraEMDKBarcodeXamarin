using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicScanning.Core
{
    public class NullScannerService : IScannerService
    {
        /// <inheritdoc />
        public event EventHandler<ScannerStatusEventArgs> ScannerStatusChanged;

        /// <inheritdoc />
        public event EventHandler<ScannerDataEventArgs> ScannerDataChanged;
    }
}
