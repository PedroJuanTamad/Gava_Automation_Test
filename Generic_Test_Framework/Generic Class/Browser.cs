using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace Generic_Test_Framework
{
    public static class Browser
    {
        //in these two constants, we set the browser type and the test environment where we want to work with
        public static BrowserType browserType = BrowserType.Chrome;
        //public const TestEnvironment TEST_ENVIRONMENT = TestEnvironment.QA;
        public static TestEnvironment testEnvironment = TestEnvironment.QA;

        public const int FIVE_MINUTES = 300; //60 seconds x 5
        public const int THREE_MINUTES = 180; //60 seconds x 3

        public const int DEFAULT_WAITING_TIME = THREE_MINUTES;

        static IWebDriver webDriver;
        static WebDriverWait webDriverWait;

        public static string TestError = "";
        public static string TestResultFile = "";
        static string mainUrl;

        //from test.runsettings
        static string envToTest;
        static string domainToTest;
        static string browserTypeString; 

        public static void TestClassInitialize(TestContext context)
        {
            envToTest = context.Properties["EnvToTest"].ToString();
            domainToTest = context.Properties["DomainToTest"].ToString();
            browserTypeString = context.Properties["BrowserForTest"].ToString();
        }

        public static string MainUrl
        {
            get
            {
                return mainUrl;
            }
        }

        public static string Title
        {
            get
            {
                return webDriver.Title;
            }
        }

        public static object navigate()
        {
            throw new NotImplementedException();
        }

        public static TestEnvironment TestEnvironment
        {
            get
            {
                return testEnvironment;
            }
        }

        public static void Goto(string url)
        {
            webDriver.Url = url;
        }

        public static ISearchContext Driver
        {
            get
            {
                return webDriver;
            }
        }

        //for managing elements that are dynamic or takes more time to load than usual, example, wizard results
        public static WebDriverWait WebDriverWait
        {
            get
            {
                return webDriverWait;
            }
        }


        public static IWebDriver WebDriver
        {
            get
            {
                return webDriver;
            }
        }

        public static void Initialize()
        {

            if (envToTest == "www")
            {
                mainUrl = "https://www." + domainToTest;
            }
            //else
            //{
            //    mainUrl = "https://" + "www-" + envToTest + "." + domainToTest;
            //}

            mainUrl = mainUrl + "/";

            try
            {
                switch (envToTest)
                {
                    case "test":
                        testEnvironment = TestEnvironment.TEST;
                        break;
                    case "qa":
                        testEnvironment = TestEnvironment.QA;
                        break;
                    case "www":
                        testEnvironment = TestEnvironment.PROD;
                        break;
                }

                switch (browserTypeString)
                {
                    case "Chrome":
                        browserType = BrowserType.Chrome;
                        break;
                    case "Firefox":
                        browserType = BrowserType.Firefox;
                        break;
                    case "IE":
                        browserType = BrowserType.IE;
                        break;
                }

                if (browserType == BrowserType.Chrome)
                {
                    ChromeDriverService serv = ChromeDriverService.CreateDefaultService(); //to check if this is okay
                    webDriver = new ChromeDriver(serv, new ChromeOptions(), TimeSpan.FromSeconds(180)); //default timeout is 60 seconds, would want to extend
                    ChromeOptions options = new ChromeOptions();
                    options.AddArguments("--disable-extensions"); // to disable extension
                    options.AddArguments("--disable-notifications"); // to disable notification
                    options.AddArguments("--disable-application-cache"); // to disable cache
                }
                else if (browserType == BrowserType.Firefox)
                    webDriver = new FirefoxDriver();
                else if (browserType == BrowserType.IE)
                    webDriver = new InternetExplorerDriver();

                //webDriver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(Browser.DEFAULT_WAITING_TIME)); //wait for sometime for page load time outs
                webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(DEFAULT_WAITING_TIME); //wait for sometime for page load time outs
                webDriver.Manage().Window.Maximize();


                webDriverWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(120));

                TurnOnImplicitWait();
            }
            catch (Exception e)
            {
                ErrorLogger.LogExceptionError(e.ToString());
                System.Diagnostics.Debug.Print(e.Message);
            }
        }

        public static void NoWait(Action action)
        {
            TurnOffImplicitWait();
            action();
            TurnOnImplicitWait();
        }

        //Implicit wait for page factory elements - calls to find elements        
        public static void TurnOnImplicitWait()
        {
            //webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(120));
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
        }

        public static void TurnOnImplicitWait(int seconds)
        {
            //webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(seconds));
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);
        }

        public static void TurnOffImplicitWait()
        {
            //webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
        }

        public static bool WaitUntilElementIsVisible(By byElement)
        {
            bool visible = false;
            TurnOffImplicitWait();
            webDriverWait.Timeout = TimeSpan.FromSeconds(120);
            try
            {
                WebDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(byElement));
                visible = true;
            }
            catch (NoSuchElementException e)
            {
                ErrorLogger.LogExceptionError(e.ToString());
                System.Diagnostics.Debug.Print(e.Message);
            }
            TurnOnImplicitWait();
            return visible;
        }

        public static bool WaitUntilElementIsVisible(By byElement, int timeOutInSeconds)
        {
            bool visible = false;
            TurnOffImplicitWait();
            webDriverWait.Timeout = TimeSpan.FromSeconds(timeOutInSeconds);
            try
            {
                WebDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(byElement));
                visible = true;
            }
            catch (NoSuchElementException e)
            {
                ErrorLogger.LogExceptionError(e.ToString());
                System.Diagnostics.Debug.Print(e.Message);
            }
            TurnOnImplicitWait();
            return visible;
        }

        public static bool WaitUntilElementIsNotVisible(By byElement, int timeOutInSeconds)
        {
            bool invisible = false;
            TurnOffImplicitWait();
            webDriverWait.Timeout = TimeSpan.FromSeconds(timeOutInSeconds);
            try
            {
                WebDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(byElement));
                invisible = true;
            }
            catch (NoSuchElementException)
            {
                invisible = true;
                //ErrorLogger.LogExceptionError(e.ToString());
                //System.Diagnostics.Debug.Print(e.Message);
            }
            catch (Exception)
            {
                invisible = false;
            }
            TurnOnImplicitWait();
            return invisible;
        }

        public static bool WaitUntilElementIsNotVisible(By byElement, bool isMillisecond, int timeOutInMilliseconds)
        {
            bool invisible = false;
            TurnOffImplicitWait();
            webDriverWait.Timeout = TimeSpan.FromMilliseconds(timeOutInMilliseconds);
            try
            {
                WebDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(byElement));
                invisible = true;
            }
            catch (NoSuchElementException)
            {
                invisible = true;
                //ErrorLogger.LogExceptionError(e.ToString());
                //System.Diagnostics.Debug.Print(e.Message);
            }
            catch (Exception)
            {
                invisible = false;
            }
            TurnOnImplicitWait();
            return invisible;
        }

        public static bool WaitUntilElementIsClickable(IWebElement webElement, int timeOutInSeconds)
        {
            bool clickable = false;
            TurnOffImplicitWait();
            webDriverWait.Timeout = TimeSpan.FromSeconds(timeOutInSeconds);
            try
            {
                WebDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(webElement));
                clickable = true;
            }
            catch (NoSuchElementException e)
            {
                ErrorLogger.LogExceptionError(e.ToString());
                System.Diagnostics.Debug.Print(e.Message);
            }
            TurnOnImplicitWait();
            return clickable;
        }

        public static bool WaitUntilElementIsClickable(IWebElement webElement)
        {
            bool clickable = false;
            TurnOffImplicitWait();
            webDriverWait.Timeout = TimeSpan.FromSeconds(120);
            try
            {
                WebDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(webElement));
                clickable = true;
            }
            catch (NoSuchElementException e)
            {
                ErrorLogger.LogExceptionError(e.ToString());
                System.Diagnostics.Debug.Print(e.Message);
            }
            TurnOnImplicitWait();
            return clickable;
        }

        public static bool WaitUntilElementExists(By byElement, int timeOutInSeconds)
        {
            bool clickable = false;
            TurnOffImplicitWait();
            webDriverWait.Timeout = TimeSpan.FromSeconds(timeOutInSeconds);
            try
            {
                WebDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(byElement));
                clickable = true;
            }
            catch (NoSuchElementException e)
            {
                ErrorLogger.LogExceptionError(e.ToString());
                System.Diagnostics.Debug.Print(e.Message);
            }
            TurnOnImplicitWait();
            return clickable;
        }

        public static bool WaitUntilElementExists(By byElement)
        {
            bool clickable = false;
            TurnOffImplicitWait();
            webDriverWait.Timeout = TimeSpan.FromSeconds(120);
            try
            {
                WebDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(byElement));
                clickable = true;
            }
            catch (NoSuchElementException e)
            {
                ErrorLogger.LogExceptionError(e.ToString());
                System.Diagnostics.Debug.Print(e.Message);
            }
            TurnOnImplicitWait();
            return clickable;
        }

        public static bool IsElementPresent(By by)
        {
            TurnOffImplicitWait();
            try
            {
                webDriver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException e)
            {
                ErrorLogger.LogExceptionError(e.ToString());
                System.Diagnostics.Debug.Print(e.Message);
                TurnOnImplicitWait();
                return false;
            }
        }

        public static bool IsElementDisplayed(IWebElement element)
        {
            TurnOffImplicitWait();
            try
            {
                return element.Displayed;
            }
            catch (NoSuchElementException e)
            {
                ErrorLogger.LogExceptionError(e.ToString());
                System.Diagnostics.Debug.Print(e.Message);
                TurnOnImplicitWait();
                return false;
            }

        }

        public static bool IsElementPresentAndDisplayed(By by)
        {
            try
            {
                return webDriver.FindElement(by).Displayed;
            }
            catch (NoSuchElementException e)
            {
                ErrorLogger.LogExceptionError(e.ToString());
                System.Diagnostics.Debug.Print(e.Message);
                return false;
            }
        }

        public static bool IsElementPresentWithinAnElement(IWebElement elementToBeSearchedWithin, By by)
        {
            bool result = false;
            TurnOnImplicitWait(3); //we will only set 3 seconds to find the element since we already found the main element so it should not take long
            try
            {
                return (elementToBeSearchedWithin.FindElement(by).Displayed); //element is found so return false
            }
            catch (Exception e)//NoSuchElementException
            {
                result = false;
                //no need to log because we only want to check if element is present or not
                //ErrorLogger.LogExceptionError(e.ToString());
                //System.Diagnostics.Debug.Print(e.Message);
            }
            TurnOnImplicitWait();
            return result;
        }

        public static bool IsElementPresentWithinAnElement(IWebElement elementToBeSearchedWithin, By by, int timeOutInSeconds)
        {
            bool result = false;
            TurnOnImplicitWait(timeOutInSeconds); //we will only set 3 seconds to find the element since we already found the main element so it should not take long
            try
            {
                return (elementToBeSearchedWithin.FindElement(by).Displayed); //element is found so return false
            }
            catch (NoSuchElementException)
            {
                //no need to log because we only want to check if element is present or not
                //ErrorLogger.LogExceptionError(e.ToString());
                //System.Diagnostics.Debug.Print(e.Message);
            }
            TurnOnImplicitWait();
            return result;
        }

        public static bool IsElementNotPresentWithinAnElement(IWebElement elementToBeSearchedWithin, By by)
        {
            bool result = false;
            TurnOnImplicitWait(3); //we will only set 3 seconds to find the element since we already found the main element so it should not take long
            try
            {
                return !(elementToBeSearchedWithin.FindElement(by).Displayed); //element is found so return false
            }
            catch (NoSuchElementException)
            {
                result = true; //no element was found and this is what we want
                //ErrorLogger.LogExceptionError(e.ToString());
                //System.Diagnostics.Debug.Print(e.Message);
            }
            TurnOnImplicitWait();
            return result;
        }

        public static bool SwitchToNewlyOpenedTab()
        {
            bool result = false;
            try
            {
                var newTab = webDriver.WindowHandles.Last();
                webDriver.SwitchTo().Window(newTab);
                result = true;
            }
            catch (Exception e)
            {
                ErrorLogger.LogExceptionError(e.ToString());
            }
            return result;
        }

        public static bool SwitchToFirstTab()
        {
            bool result = false;
            try
            {
                var newTab = webDriver.WindowHandles.First();
                webDriver.SwitchTo().Window(newTab);
                result = true;
            }
            catch (Exception e)
            {
                ErrorLogger.LogExceptionError(e.ToString());
            }
            return result;
        }

        public static bool CanCloseExistingTab()
        {
            bool result = false;
            try
            {
                Browser.Close();
                Browser.SwitchToFirstTab();
                result = true;
            }
            catch (Exception e)
            {
                ErrorLogger.LogExceptionError(e.ToString());
            }
            return result;
        }

        public static void Close()
        {
            webDriver.Close();
        }
    }

    public enum BrowserType
    {
        Chrome,
        Firefox,
        IE
    }

    public enum TestEnvironment
    {
        TEST,
        QA,
        PROD
    }

}
