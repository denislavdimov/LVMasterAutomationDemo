using LVPages.IClasses;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace LVPages
{
    public class BasePage
    {
        private readonly Wait Wait;
        private readonly IUserActions I;
        protected IWebDriver driver;
        public int randomNumber = (int)(new Random().NextInt64(2022) + 20);
        public int NewRandomNumber = (int)(new Random().NextInt64(2022) + 20);

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            Wait = new Wait(driver);
            I = new UserActions(driver);
        }

        public enum Browsers
        {
            Chrome, IE, Firefox
        }

        public virtual string PageUrl { get; }
        public IList<IWebElement> Exception =>
          driver.FindElements(By.XPath("//div[@class='toast toast-error']")).ToList();
        public IList<IWebElement> Warning =>
            driver.FindElements(By.XPath("//div[contains(@class, 'toast toast-warning')]")).ToList();

        public IWebDriver CreateInstance(Browsers browser)
        {
            if (Browsers.Chrome == browser)
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--window-size=1440,900", "--disable-extensions", "--disable-popup-blocking");
                return new ChromeDriver(options);
            }
            else if (Browsers.IE == browser)
            {
                return new InternetExplorerDriver();
            }
            else
            {
                return new FirefoxDriver();
            }
        }

        public void StartBrowser()
        {
            driver = CreateInstance(Browsers.Chrome);
            PageHelper.PageBuilder(driver);
            driver.Manage().Cookies.DeleteAllCookies();
        }

        public void CloseBrowser()
        {
            driver.Manage().Cookies.DeleteAllCookies();
            if (driver == null)
                return;
            driver.Close();
            driver.Quit();
        }

        public void TakeScreenshotIfTestFails()
        {
            string path = "C:\\LVFailingTests";
            string testname = TestContext.CurrentContext.Test.Name;
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                System.IO.Directory.CreateDirectory(path);
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile($@"{path}\{testname}.jpg", ScreenshotImageFormat.Jpeg);
            }
        }

        public void IGoToThisPageUrl()
        {
            try
            {
                driver.Navigate().GoToUrl(PageUrl);
                Wait.ForPageToLoad();
                Wait.ForAjax();
                Wait.ForTheLoader();
                Wait.ForNoErrorAndException();
            }
            catch (Exception e)
            {
                Console.WriteLine("Pages failed to load", e.Message);
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


        public void ClearAllInputFields()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("$('input').val('');");
        }

    }
}
