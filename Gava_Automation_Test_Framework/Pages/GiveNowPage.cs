using Generic_Test_Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gava_Automation_Test_Framework
{
    public class GiveNowPage
    {

        public List<IWebElement> listOfActiveOptions = new List<IWebElement>();
        public static string selectedOption = "";
        public static DataTable testDataTable;
        public static int rowCounter;
        public static string elementValue = "";
        public const int TEST_DATA_INDEX = 3;
        public static string ErrorMessage = "";

        public void ClickBrowseCampaignsButton()
        {
            Browser.WaitUntilElementIsVisible(By.XPath("//button[@class='ant-btn give-btn ant-btn-lg' and @style='width: 100%; font-size: 0.8rem; margin-top: 20px;']"),10); 
            IWebElement web_Element_To_Be_Hovered = Browser.WebDriver.FindElement(By.XPath("//button[@class='ant-btn give-btn ant-btn-lg' and @style='width: 100%; font-size: 0.8rem; margin-top: 20px;']"));
            Actions builder = new Actions(Browser.WebDriver);
            builder.MoveToElement(web_Element_To_Be_Hovered).Build().Perform();

            web_Element_To_Be_Hovered.Click();
        }

        public bool GetTestData(string SheetName)
        {
            bool result = false;
            try
            {

                string files = "";
                string filename = "\\ExcelTestData\\TestData.xlsx";

                files = AppDomain.CurrentDomain.BaseDirectory + filename;
                testDataTable = DataReader.ReadExcelFile(files, SheetName);

                if (testDataTable.Rows.Count > 0)
                {
                    result = true;
                }
            }
            catch (Exception e)
            {
                ErrorLogger.LogExceptionError(e.ToString());
            }
            return result;
        }

        public bool FilloutDonationInfo()
        {
            bool result = false;
            int pageNumber = 1;

            IWebElement selectElement;
            IWebElement textElement;
            IWebElement divElement;
            IWebElement checkboxElement;
            IWebElement nextButton;
            IWebElement conButton;
            IList<IWebElement> divElements;
            string elementType = "";
            string divID = "";
            int elementIndex = 0;


            int numElements = testDataTable.Columns.Count;

            try
            {
                for (int i = 1; i < numElements; i++)
                {
                    System.Threading.Thread.Sleep(1000);

                    int xpathVar = 0;
                    elementValue = testDataTable.Rows[TEST_DATA_INDEX][i].ToString();
                    //nothing to enter or change, then skip this element
                    if (elementValue.Equals(""))
                        continue;

                    divID = testDataTable.Rows[0][i].ToString(); //Div ID
                    elementType = testDataTable.Rows[1][i].ToString(); // Element Type
                    elementIndex = int.Parse(testDataTable.Rows[2][i].ToString()); // Element Index
                    ErrorMessage = testDataTable.Rows[4][i].ToString(); // Error Message

                    if (Browser.WaitUntilElementIsNotVisible(By.Id(divID), true, 500)) //if not visible, continue with the next element
                    {
                        if (Browser.WaitUntilElementIsNotVisible(By.XPath(divID), true, 100)) //add this conditon for xpath
                        {
                            continue;
                        }
                    }
                    if (Browser.IsElementPresent(By.XPath(divID))) //add this conditon for xpath
                    {
                        divElements = Browser.WebDriver.FindElements(By.XPath(divID));
                        xpathVar = 1;
                    }
                    else if (Browser.IsElementPresent(By.Id(divID)))
                    {
                        divElements = Browser.WebDriver.FindElements(By.Id(divID));
                    }
                    else
                    {
                        return false;
                    }

                    divElement = divElements[elementIndex];
                    if (divElement.Displayed == false) //if not displayed, continue with next element
                        continue;

                    switch (elementType)
                    {
                        case "Select":
                            if (xpathVar == 1)
                            {
                                IWebElement xpathElem = Browser.WebDriver.FindElement(By.XPath(divID));
                                xpathElem.SendKeys(elementValue);
                                System.Threading.Thread.Sleep(500);
                                xpathVar = 0;
                            }
                            else
                            {
                                selectElement = divElement.FindElement(By.TagName("select"));
                                SelectItemFromDropDownList(selectElement, elementValue);
                                System.Threading.Thread.Sleep(500);
                            }

                            break;
                        case "Text":

                            if (xpathVar == 1)
                            {
                                IWebElement xpathElem = Browser.WebDriver.FindElement(By.XPath(divID));
                                xpathElem.Clear();
                                xpathElem.SendKeys(elementValue);

                                xpathVar = 0;
                            }
                            else
                            {
                                textElement = divElement;
                                if (textElement.GetAttribute("value").ToString().Equals(""))  //is blank then fill it, otherwise, leave prefilled value
                                {
                                    textElement.Clear();
                                    textElement.SendKeys(elementValue);
                                }
                            }

                            break;
                        case "Birthdate":

                            textElement = divElement;
                            if (textElement.GetAttribute("value").ToString().Equals(""))  //is blank then fill it, otherwise, leave prefilled value
                            {
                                string bday = elementValue;
                                foreach (var item in bday)
                                {
                                    textElement.SendKeys(item.ToString());
                                    System.Threading.Thread.Sleep(100);
                                }
                            }
                            break;
                        case "TextMobile":

                            textElement = divElement;
                            //always clear current input and change it to new one
                            textElement.Clear();
                            System.Threading.Thread.Sleep(500);
                            textElement.SendKeys(elementValue);

                            break;
                        case "TextArea":

                            textElement = divElement;
                            if (textElement.GetAttribute("value").ToString().Equals(""))  //is blank then fill it, otherwise, leave prefilled value
                            {
                                textElement.Clear();
                                textElement.SendKeys(elementValue);
                            }
                            break;
                        case "Checkbox":

                            checkboxElement = divElement;
                            checkboxElement.Click();

                            break;
                        case "Radio":

                            GetAllOptions(divElement);
                            SelectAnOption(listOfActiveOptions, elementValue.ToString());

                            break;
                        case "Gender":

                            divElement.Click();

                            break;

                        case "DonateNowButton":
                            nextButton = divElement;
                            nextButton.Click();
                            System.Threading.Thread.Sleep(1000);
                            break;

                        case "UpdateDateButton":
                            nextButton = divElement;
                            nextButton.Click();
                            System.Threading.Thread.Sleep(1000);
                            break;

                        case "SelectDate":
                            divElement.Click();

                            break;
                    }
                }

                result = true;
            }
            catch (Exception e)
            {
                ErrorLogger.LogExceptionError(e.ToString() + "\n\r" + divID);
            }
            return result;
        }

        public bool SelectItemFromDropDownList(IWebElement webElement, string itemToBeSelected)
        {
            bool result = false;
            SelectElement selectElement; //generic element declared for the different select elements within a div
            try
            {
                selectElement = new SelectElement(webElement);
                Browser.WaitUntilElementIsClickable(webElement);
                if (selectElement.Options.Count > 1)
                {
                    selectElement.SelectByText(itemToBeSelected);
                    result = true;
                }
            }
            catch (Exception e)
            {
                ErrorLogger.LogExceptionError(e.ToString());
            }
            return result;
        }

        private bool GetAllOptions(IWebElement elementGroup)
        {
            bool result = false;
            try
            {
                System.Threading.Thread.Sleep(1500);

                IList<IWebElement> listOfWizardOptions;

                if (listOfActiveOptions != null)
                    listOfActiveOptions.Clear();

                Browser.WaitUntilElementIsVisible(By.Id(elementGroup.GetAttribute("id")));

                try
                {
                    listOfWizardOptions = elementGroup.FindElements(By.TagName("label"));
                }
                catch (Exception)
                {
                    listOfWizardOptions = elementGroup.FindElements(By.TagName("option"));
                }

                Browser.WaitUntilElementIsClickable(listOfWizardOptions[0]);
                int maxOptionCount = 0;
                //In some cases, other options are hidden so we must first get the number of visible options and set this to maxOptionCount
                for (int i = 0; i < listOfWizardOptions.Count; i++)
                {
                    if (listOfWizardOptions[i].Displayed) //wizard options are all options including those that are invisible and cannot be selected
                    {                                       //filter options that are only displayed or visible
                        string classAttribute = listOfWizardOptions[i].GetAttribute("class");
                        if (classAttribute.Contains("selected"))
                            selectedOption = listOfWizardOptions[i].Text; //this is the currently selected option - we can use this to compare if need to click the option or just click Next button
                        maxOptionCount++;
                        listOfActiveOptions.Add(listOfWizardOptions[i]); //active options are options that are visible and can be selected
                    }
                }
                result = true;
            }
            catch (Exception e)
            {
                ErrorLogger.LogExceptionError(e.ToString());
            }
            return result;
        }

        public bool SelectAnOption(List<IWebElement> listOfOptions, string optionToSelect)
        {
            bool result = false;
            try
            {
                System.Threading.Thread.Sleep(1500);
                if (selectedOption.Equals(optionToSelect)) //for new frameworks, previous selection are retained during session, so must click Next button to proceed
                {
                    System.Threading.Thread.Sleep(500);
                    IWebElement fieldSet;
                    fieldSet = Browser.WebDriver.FindElement(By.CssSelector("div.foot"));
                    fieldSet.FindElement(By.CssSelector("button.btn.btn-primary")).Click();
                    result = true;
                }
                else
                {
                    for (int i = 0; i < listOfOptions.Count; i++)
                    {
                        if (listOfOptions[i].Text.Equals(optionToSelect.Trim())) //found, select this option and exit out of the loop
                        {
                            listOfOptions[i].Click();
                            result = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLogger.LogExceptionError(e.ToString());
            }
            return result;
        }

    }
}
