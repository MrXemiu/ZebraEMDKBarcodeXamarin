using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicScanning.Core
{
    public interface IScannerInterface
    {
        event EventHandler<ScannerStatusEventArgs> ScannerStatusChanged;

        event EventHandler<ScannerDataEventArgs> ScannerDataChanged;
    }
}
