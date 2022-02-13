using Generic_Test_Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Gava_Automation_Test_Framework
{
    public class CheckoutPage
    {

        public static string Url
        {
            get
            {
                string returnStr;
                string url = "paymentdetails";
                returnStr = Browser.MainUrl + url;

                return returnStr;
            }
        }

        public void SelectPaymentPartner()
        {
            Browser.WaitUntilElementIsVisible(By.XPath("//span[@class='watermark_heading_italic-violet']"), 10);
            IWebElement web_Element_To_Be_Hovered = Browser.WebDriver.FindElement(By.XPath("//p[contains(text(),'7-Eleven')]"));
            Actions builder = new Actions(Browser.WebDriver);
            builder.MoveToElement(web_Element_To_Be_Hovered).Build().Perform();

            web_Element_To_Be_Hovered.Click();
        }

        public void ClickDonateButton()
        {
            Browser.WaitUntilElementIsVisible(By.XPath("//span[@class='watermark_heading_italic-violet']"), 10);
            IWebElement web_Element_To_Be_Hovered = Browser.WebDriver.FindElement(By.XPath("//button[@class='ant-btn ant-btn-primary ant-btn-lg' and @type='button']"));
            Actions builder = new Actions(Browser.WebDriver);
            builder.MoveToElement(web_Element_To_Be_Hovered).Build().Perform();

            web_Element_To_Be_Hovered.Click();
        }
    }
}
