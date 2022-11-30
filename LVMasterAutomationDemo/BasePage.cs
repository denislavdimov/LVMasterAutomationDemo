using LVPages.IClasses;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LVPages
{
    public class BasePage
    {
        private readonly Wait Wait;
        private readonly IUserActions I;
        protected IWebDriver driver;
        //private static int secondsToLoadPage = 30;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            Wait = new Wait(driver);
            I = new UserActions(driver);
        }

        public virtual string PageUrl { get; }
        public IList<IWebElement> Exception =>
          driver.FindElements(By.XPath("//div[@class='toast toast-error']")).ToList();
        public IList<IWebElement> Warning =>
            driver.FindElements(By.XPath("//div[contains(@class, 'toast toast-warning')]")).ToList();

        public IWebElement Exception2 =>
            driver.FindElement(By.XPath("//div[@class='toast toast-error']"));
        public IWebElement Warning2 =>
            driver.FindElement(By.XPath("//div[contains(@class, 'toast toast-warning')]"));

        public void IGoToThisPageUrl()
        {
            try
            {
                driver.Navigate().GoToUrl(PageUrl);
                Wait.ForAjax();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public void AssertDriverUrlIsEqualToPageUrl()
        {
            try
            {
                Assert.That(driver.Url, Is.EqualTo(PageUrl));
            }
            catch (Exception)
            {
                Console.WriteLine("The PageUrl and DriverUrl are not equal");
                throw;
            }
        }

        public void IDragAndDrop(IWebElement element, IWebElement place)
        {
            try
            {
                Wait.ForElementToBeClickable(element);
                I.DragAndDrop(element, place);
            }
            catch (Exception)
            {
                Console.WriteLine($"The element: {element} cannot be drag and dropped to place: {place}");
                throw;
            }
        }

        public void ClearAllInputFields()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("$('input').val('');");
        }

    }
}
