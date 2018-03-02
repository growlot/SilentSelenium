using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilentSelenium.Utilities
{
    public static class DebugEx
    {
        private static string LogPath { get; } = Path.Combine(Path.GetTempPath() + "SilentSelenium.log");
        
        public static void Log(string log)
        {
            File.AppendAllText(Path.Combine(Path.GetTempPath() + "SilentSelenium.log"), Environment.NewLine + log);
        }
    }
}
