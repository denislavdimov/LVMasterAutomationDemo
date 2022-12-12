using LVPages.IClasses;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace LVPages
{
    public class UserActions : IUserActions
    {
        protected IWebDriver driver;
        private readonly IWait Wait;
        private Actions action => new Actions(driver);
        int timeout = 0;

        public UserActions(IWebDriver driver)
        {
            Wait = new Wait(driver);
            this.driver = driver;
        }

        public void Click(IWebElement element)
        {
            try
            {
                for (int i = 5250; i > timeout;)
                {
                    if (element.Displayed && element.Enabled)
                    {
                        element.Click();
                        Thread.Sleep(700);
                        break;
                    }
                    else if (timeout >= 5000)
                    {
                        Assert.IsTrue(element.Displayed && element.Enabled, 
                            "The Element is neither displayed nor enabled and cannot be clicked.");
                    }
                    else
                    {
                        Thread.Sleep(250);
                        timeout += 250;
                    }
                }
            }
            catch (StaleElementReferenceException)
            {
                Console.WriteLine($"The element: {element} is not clickable");
                throw;
            }
        }

        public void FillInField(IWebElement element, string data)
        {
            try
            {
                for (int i = 5250; i > timeout;)
                {
                    if (element.Displayed && element.Enabled)
                    {
                        element.SendKeys(data);
                        Thread.Sleep(500);
                        break;
                    }
                    else if (timeout >= 5000)
                    {
                        Assert.IsTrue(element.Displayed && element.Enabled,
                            "The Element is neither displayed nor enabled and cannot fill in data.");
                    }
                    else
                    {
                        Thread.Sleep(250);
                        timeout += 250;
                    }
                }
            }
            catch (StaleElementReferenceException)
            {
                Console.WriteLine("Cannot fill in data");
                throw;
            }
        }
        
        public void SelectItemFromDropdown(IWebElement dropdown, int itemnumber)
        {
            SelectDropdown(dropdown);
            SelectDropdownItem(dropdown, itemnumber);
        }

        private void SelectDropdown(IWebElement dropdown)
        {
            try
            {
                if (dropdown.Displayed && dropdown.Enabled)
                {
                    action.MoveToElement(dropdown).Click(dropdown).Build().Perform();
                    var items = driver.FindElements(By.XPath("//div[contains(@class,'lv-select__option')]")).ToList();
                    var multiItems = driver.FindElements(By.XPath("//div[contains(@class,'lv-multi-select__option')]")).ToList();
                    if (items.Count > 0)
                    {
                        Wait.ForElements(By.XPath("//div[contains(@class,'lv-select__option')]"));
                        return;
                    }
                    else if (multiItems.Count > 0)
                    {
                        Wait.ForElements(By.XPath("//div[contains(@class,'lv-multi-select__option')]"));
                        return;
                    }
                }
                else
                {
                    Click(dropdown);
                    var items = driver.FindElements(By.XPath("//div[contains(@class,'lv-select__option')]")).ToList();
                    var multiItems = driver.FindElements(By.XPath("//div[contains(@class,'lv-multi-select__option')]")).ToList();
                    if (items.Count > 0)
                    {
                        Wait.ForElements(By.XPath("//div[contains(@class,'lv-select__option')]"));
                        return;
                    }
                    else if (multiItems.Count > 0)
                    {
                        Wait.ForElements(By.XPath("//div[contains(@class,'lv-multi-select__option')]"));
                        return;
                    }
                }
                Wait.ForTheLoader();
            }
            catch (Exception)
            {
                Console.WriteLine($"There is no such dropdown: {dropdown}");
                throw;
            }
        }

        private void SelectDropdownItem(IWebElement dropdown, int itemnumber)
        {
            try
            {
                var items = driver.FindElements(By.XPath("//div[contains(@class,'lv-select__option')]")).ToList();
                var multiItems = driver.FindElements(By.XPath("//div[contains(@class,'lv-multi-select__option')]")).ToList();
                if (items.Count > 0)
                {
                    action.MoveToElement(items[itemnumber]).Click(items[itemnumber]).Build().Perform();
                    Wait.ForTheLoader();
                }
                else if (multiItems.Count > 0)
                {
                    action.MoveToElement(multiItems[itemnumber]).Click(multiItems[itemnumber]).Build().Perform();
                    Wait.ForTheLoader();
                }
                else
                {
                    Wait.ForElementToBeClickable(dropdown);
                    action.MoveToElement(dropdown).Click(dropdown).Build().Perform();
                    if (items.Count > 0)
                    {
                        action.MoveToElement(items[itemnumber]).Click(items[itemnumber]).Build().Perform();
                        Wait.ForTheLoader();
                    }
                    else if (multiItems.Count > 0)
                    {
                        action.MoveToElement(multiItems[itemnumber]).Click(multiItems[itemnumber]).Build().Perform();
                        Wait.ForTheLoader();
                    }
                    else
                    {
                        Assert.IsTrue(items.Count == 0, "The dropdown is not opened");
                        Assert.IsTrue(multiItems.Count == 0, "The dropdown is not opened");
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Cannot select item from the dropdown: {dropdown} ");
                throw;
            }

        }

    }
}
