using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataGridPerformanceTest
{
    public static class Config 
    {
        public static string AssemblyDirectory { get => Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName); }
        public static string BusinessDataFileName { get => "UsefulBusinessData.xml"; }
        public static string BusinessDataFullFileName { get => Path.Combine(AssemblyDirectory, BusinessDataFileName); }
    }
}
