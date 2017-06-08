using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicScanning.Core
{
    public class ScannerStatusEventArgs
    {
       public string Status { get; }


        public ScannerStatusEventArgs(string status)
        {
            Status = status;
        }
    }
}
