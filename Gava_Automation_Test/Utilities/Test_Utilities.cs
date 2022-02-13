using Generic_Test_Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace Gava_Automation_Test
{
    [TestClass]
    public class Test_Utilities
    {
        [TestInitialize]
        public void Init()
        {
            Browser.Initialize();
        }

        [TestCleanup]
        public void Close()
        {
            foreach (var process in Process.GetProcessesByName("chromedriver.exe"))
            {
                process.Kill();
            }

            Browser.WebDriver.Quit();
        }
    }
}
