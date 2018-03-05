using SilentSelenium.Utilities;
using Sysinternal.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace Sysinternal
{
    public static class Sysinternal
    {
        public static bool ExecuteSilentExeutable(string executableToRun, string argument)
        {
            try
            {
                UInt32 dwCurrentSessionId = Win32API.WTSGetActiveConsoleSessionId();
                UInt32 dwSessionId = dwCurrentSessionId == 0 ? 1u : 0u;
                ExecuteByPsExec(dwSessionId, executableToRun, argument);
                return true;
            }
            catch (Win32Exception e)
            {
                DebugEx.Log(e.ToString());
            }
            return false;
        }

        public static bool ExecuteByPsExec(UInt32 session, string executableToRun, string argument)
        {
            try
            {
                var cmdLine = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase), "SilentExecutor.exe");

                StringBuilder output = new StringBuilder();
                var si = new ProcessStartInfo()
                {
                    FileName = cmdLine,
                    Arguments = string.Format("-accepteula -d -x -s \"{1}\" {2}", session, executableToRun, argument)
                };
                Process.Start(si);
                DebugEx.Log("ExecuteByPsExec Success!!!");
                return true;
            }
            catch (Win32Exception e)
            {
                DebugEx.Log(e.ToString());
            }
            return false;
        }
    }
}
