using Generic_Test_Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Gava_Automation_Test_Framework
{
    public class HomePage
    {

        //[FindsBy(How = How.XPath, Using = "//span[contains(text(),'BROWSE CAMPAIGNS')]")]     
        [FindsBy(How = How.XPath, Using = "//button[@class='ant-btn button_cta ant-btn-lg']")]
        private IWebElement BrowseCampaignsButton;

        public void Goto(string url)
        {
            Browser.Goto(url);
        }

        public bool IsAt()
        {
            return Browser.WebDriver.Url.Equals(Browser.MainUrl);
        }

        public void ClickBrowseCampaignsButton()
        {
            Browser.WaitUntilElementIsClickable(BrowseCampaignsButton, 5);
            BrowseCampaignsButton.Click();
        }


        public bool IsAtCampaignPage()
        {
            var campaignPage = new CampaignPage();
            PageFactory.InitElements(Browser.Driver, campaignPage);       
            Browser.WaitUntilElementExists(By.XPath("//p[@class='watermark_subtitle']"));
            return Browser.WebDriver.Url.Equals(CampaignPage.Url);
        }

        public bool IsAtGiveNowPage()
        {
            var giveNowPage = new GiveNowPage();
            PageFactory.InitElements(Browser.Driver, giveNowPage);      
            return Browser.WaitUntilElementIsVisible(By.XPath("//button[@class='ant-btn give-btn ant-btn-lg' and @style='width: 100%; font-size: 0.8rem; margin-top: 20px;' ]"));
        }

        public bool IsAtCheckoutPage()
        {
            var checkoutPage = new CheckoutPage();
            PageFactory.InitElements(Browser.Driver, checkoutPage);
            Browser.WaitUntilElementExists(By.XPath("//span[@class='watermark_heading_italic-violet']"));
            return Browser.WebDriver.Url.Equals(CheckoutPage.Url);
        }

        public bool IsAtAppCoinPage()
        {
            var appCoinPage = new AppCoinsPage();
            PageFactory.InitElements(Browser.Driver, appCoinPage);
            Browser.WaitUntilElementExists(By.XPath("//div[@class='c-navbar__logo ember-view' and @id='ember377']"));
            return Browser.WebDriver.Url.Contains(AppCoinsPage.Url);
        }

    }
}
