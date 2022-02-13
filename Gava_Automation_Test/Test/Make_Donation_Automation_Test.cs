using Gava_Automation_Test_Framework;
using Generic_Test_Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Gava_Automation_Test
{
    [TestClass]
    public class Make_Donation_Automation_Test : Test_Utilities
    {
        [ClassInitialize]
        public static void TestClassinitialize(TestContext context)
        {
            Browser.TestClassInitialize(context);
        }

        [TestMethod, TestCategory("Gava_Automation_Test")]
        public void Can_Make_a_Donation()
        {
            Pages.HomePage.Goto(Browser.MainUrl);
            Assert.IsTrue(Pages.HomePage.IsAt(), "Failed to go to home page");
            Pages.HomePage.ClickBrowseCampaignsButton();
            Assert.IsTrue(Pages.HomePage.IsAtCampaignPage(), "Failed to navigate to Campaigns Page");
            Pages.CampaignPage.HoverCampaignAndClick();
            Assert.IsTrue(Pages.HomePage.IsAtGiveNowPage(), "Failed to navigate to Give Now Page");
            Pages.GiveNowPage.ClickBrowseCampaignsButton();

            Assert.IsTrue(Pages.GiveNowPage.GetTestData("DonationInfo"), "Failed to get test data for Crerdit Card.");
            Assert.IsTrue(Pages.GiveNowPage.FilloutDonationInfo(), "Failed to complete " + GiveNowPage.ErrorMessage.ToString() + ".");
            Assert.IsTrue(Pages.HomePage.IsAtCheckoutPage(), "Failed to navigate to Checkout Page");

            Pages.CheckoutPage.SelectPaymentPartner();
            Pages.CheckoutPage.ClickDonateButton();

            Assert.IsTrue(Pages.HomePage.IsAtAppCoinPage(), "Failed to navigate to Checkout Page");
        }

    }
}
