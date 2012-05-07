using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FormFactory
{
    public class Logger
    {
        static Logger()
        {
            Trace = s => System.Diagnostics.Trace.WriteLine(s);
            LogError = s => System.Diagnostics.Trace.TraceError(s.ToString());
        }
        public static Action<string> Trace { get; set; }

        public static Action<Exception> LogError { get; set; }
    }
}
