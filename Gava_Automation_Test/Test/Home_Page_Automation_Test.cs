using Gava_Automation_Test_Framework;
using Generic_Test_Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Gava_Automation_Test
{
    [TestClass]
    public class Home_Page_Automation_Test : Test_Utilities
    {
        [ClassInitialize]
        public static void TestClassinitialize(TestContext context)
        {
            Browser.TestClassInitialize(context);
        }

        [TestMethod, TestCategory("Gava_Automation_Test")]
        public void Can_Goto_HomePage()
        {
            Pages.HomePage.Goto(Browser.MainUrl);
            Assert.IsTrue(Pages.HomePage.IsAt(), "Failed to go to home page");
        }

       
    }
}
