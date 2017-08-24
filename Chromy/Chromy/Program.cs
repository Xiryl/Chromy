using Chromy.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Chromy
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Kill All Chrome Process
            Process[] chromeInstances = Process.GetProcessesByName("chrome");

            foreach (Process p in chromeInstances)
                p.Kill();

            #endregion
        }
    }
}
