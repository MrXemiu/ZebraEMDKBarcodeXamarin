using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicScanning.Core
{
    public class ScannerDataEventArgs
    {
        public ScannerLabelType LabelType { get; }

        public string Data { get; }


        public ScannerDataEventArgs(ScannerLabelType labelType, string data)
        {
            LabelType = labelType;
            Data = data;
        }
    }
}
