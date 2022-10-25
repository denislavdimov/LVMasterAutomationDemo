using OpenQA.Selenium;

namespace LVMasterAutomationDemo.Pages
{
    public class Roles : BasePage
    {
        private readonly IWait _wait;
        int randomNumber = (int)(new Random().NextInt64(11) + 20);
        public Roles(IWebDriver driver, IWait wait) : base(driver)
        {
            _wait = wait;
        }
        public override string PageUrl => "https://loanvantage.dev/IBS/master/LVWEB/Admin/#/Roles/";
        private IWebElement nameInputField => driver.FindElement(By.XPath("//input[@name='Name']"));
        private IWebElement linkAdd => driver.FindElement(By.LinkText("Add"));
        private IWebElement roleModal => driver.FindElement(By.XPath("//div[@class='k-widget k-window']"));
        private IWebElement searchArea => driver.FindElement(By.XPath("//input[contains(@class,'search-query form-control')]"));
        private IWebElement deleteButton => driver.FindElement(By.XPath("//button[contains(.,'Delete')]"));
        private IWebElement addAllLink => driver.FindElement(By.CssSelector("a[data-bind='click: addAll'] strong"));
        private IWebElement saveButton => driver.FindElement(By.XPath("//button[contains(.,'Save')]"));
        private IWebElement availableColumn => driver.FindElement(By.Id("available"));
        private IWebElement assignedColumn => driver.FindElement(By.Id("assigned"));
        private IWebElement availableItem => driver.FindElement(By.CssSelector("#available > div"));
        private IWebElement assignedItem => driver.FindElement(By.CssSelector("#assigned > div"));
        private IWebElement confirmationDialog => driver.FindElement(By.CssSelector(".confimation-dialog h5"));
        private IWebElement yesButton => driver.FindElement(By.XPath("//button[contains(.,'Yes')]"));
        private IWebElement editButton => driver.FindElement(By.XPath("(//a[contains(@class,'v-icon icon-edit k-grid-Edit')])"));
        //private IReadOnlyList<IWebElement> element => driver.FindElements(By.CssSelector("#assigned > div"));
        //private IList<IWebElement> assignedItems => driver.FindElements(By.CssSelector("#assigned > div"));


        public void AddRole()
        {
            IWaitAndClick(linkAdd);
            _wait.IWaitForLoader();
            ISeeElement(roleModal, By.XPath("//div[@class='k-widget k-window']"));
            ISeeElements(By.CssSelector("#available > div"));
            IWaitForElementAndType(nameInputField, "DenisAutomationRole" + randomNumber);
            IWaitAndClick(addAllLink);
            ISeeElements(By.CssSelector("#assigned > div"));
            IWaitAndClick(saveButton);
            _wait.IWaitForLoader();
            ISeeNoErrorAndException();
        }

        public void DeleteRole()
        {
            IWaitForElementAndType(searchArea, "DenisAutomationRole");
            IWaitAndClick(editButton);
            _wait.IWaitForLoader();
            IWaitAndClick(deleteButton);
            ISeeElement(confirmationDialog, By.CssSelector(".confimation-dialog h5"));
            IWaitAndClick(yesButton);
            _wait.IWaitForLoader();
            ISeeNoErrorAndException();
        }
    }
}
 