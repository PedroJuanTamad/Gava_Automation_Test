using Generic_Test_Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;

namespace Gava_Automation_Test_Framework
{
    public class CampaignPage
    {
        public static string Url
        {
            get
            {
                string returnStr;
                string url = "discover/campaign";
                returnStr = Browser.MainUrl + url;

                return returnStr;
            }
        }

        public void HoverCampaignAndClick()
        {
            Browser.WaitUntilElementIsVisible(By.XPath("//h4[contains(text(),'SALAMAT, PRRD')]"), 10);
            IWebElement web_Element_To_Be_Hovered = Browser.WebDriver.FindElement(By.XPath("//h4[contains(text(),'SALAMAT, PRRD')]"));
            Actions builder = new Actions(Browser.WebDriver);
            builder.MoveToElement(web_Element_To_Be_Hovered).Build().Perform();

            web_Element_To_Be_Hovered.Click();

        }
    }
}
