using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SilentSelenium.Utilities;
using System;
using System.Diagnostics;

namespace SilentSelenium
{
    class Program
    {
        static void Main(string[] args)
        {
            bool interactive = false;
            bool selenium = false;
            if (args.Length == 1)
            {
                if (string.Compare(args[0], "interactive", true) == 0)
                    interactive = true;
                else if (string.Compare(args[0], "selenium", true) == 0)
                    selenium = true;
            }
            if (interactive || selenium)
            {
                try
                {
                    IWebDriver driver = new ChromeDriver();
                    driver.Navigate().GoToUrl("https://www.google.com");
                    DebugEx.Log("SilentSelenium Success!!!");
                }
                catch (Exception e)
                {
                    DebugEx.Log(e.ToString());
                }
            }
            else if (interactive == false)
            {
                var process = Process.GetCurrentProcess();
                Sysinternal.Sysinternal.ExecuteSilentExeutable(process.MainModule.FileName, "selenium");
            }
        }
    }
}
