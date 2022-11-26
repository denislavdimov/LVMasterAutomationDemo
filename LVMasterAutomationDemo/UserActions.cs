using LVPages.IClasses;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace LVPages
{
    public class UserActions : IUserActions
    {
        protected IWebDriver driver;
        public UserActions(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void Click(IWebElement element)
        {
            try
            {
                element.Click();
                Thread.Sleep(1000);
            }
            catch (Exception)
            {
                Console.WriteLine($"The {element} is not clickable");
                throw;
            }
        }

        public void Type(IWebElement element, string data)
        {
            try
            {
                element.SendKeys(data);
            }
            catch (Exception)
            {
                Console.WriteLine("Cannot fill in data");
                throw;
            }
        }
        
        public void DragAndDrop(IWebElement element1, IWebElement element2)
        {
            var UserAction = new Actions(driver);
            try
            {
                var dragndrop = UserAction.ClickAndHold(element1)
                    .Pause(TimeSpan.FromSeconds(1))
                    .MoveToElement(element2)
                    .Pause(TimeSpan.FromSeconds(1))
                    .Release()
                    .Build();
                dragndrop.Perform();
            }
            catch (Exception)
            {
                Console.WriteLine("Drag and Drop element failed");
                throw;
            }
        }
    }
}
