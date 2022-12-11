using LVPages.IClasses;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V105.CSS;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Drawing;

namespace LVPages
{
    public class UserActions : IUserActions
    {
        protected IWebDriver driver;
        private Actions action => new Actions(driver);

        int timeout = 0;
        public UserActions(IWebDriver driver)
        {
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
        
        public void SelectFromDropdown(IWebElement dropdown, IWebElement item)
        {
            try
            {
                Click(dropdown);
                if (item.Displayed && item.Enabled)
                {
                    var SelectItem = action.MoveToElement(item).Click().Build();
;                   SelectItem.Perform();
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Cannot select item: {item} from dropdown: {dropdown}");
                throw;
            }

        }

        public void DragAndDrop(IWebElement element1, IWebElement element2)
        {
            try
            {
                var UserAction = new Actions(driver);
                //var jsFile = File.ReadAllText(@"C:\Users\Denislav\Desktop\DragDropHelper\drag_and_drop_helper.js");
                //IJavaScriptExecutor js = driver as IJavaScriptExecutor;
                //js.ExecuteScript(jsFile + "$('').simulateDragDrop({ dropTarget: ''});");

                UserAction.DragAndDrop(element1, element2).Perform();

            }
            catch (Exception)
            {
                Console.WriteLine("Drag and Drop element failed");
                throw;
            }
        }
    }
}
