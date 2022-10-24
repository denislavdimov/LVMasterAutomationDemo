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
        private IWebElement searchArea => driver.FindElement(By.XPath("//input[contains(@class,'search-query form-control')]"));
        private IWebElement availableSearchField => driver.FindElement(By.Id("available-search-field"));
        private IWebElement addAllLink => driver.FindElement(By.CssSelector("a[data-bind='click: addAll'] strong"));
        private IWebElement saveButton => driver.FindElement(By.XPath("//button[contains(.,'Save')]"));
        private IWebElement availableColumn => driver.FindElement(By.Id("available"));
        private IWebElement assignedColumn => driver.FindElement(By.Id("assigned"));
        private IWebElement availableItem => driver.FindElement(By.CssSelector("#available > div"));
        private IWebElement assignedItem => driver.FindElement(By.CssSelector("#assigned > div"));
        private IWebElement editButton => driver.FindElement(By.XPath("(//a[contains(@class,'v-icon icon-edit k-grid-Edit')])"));
        private IReadOnlyList<IWebElement> element => driver.FindElements(By.CssSelector("#assigned > div"));
        private IList<IWebElement> assignedItems => driver.FindElements(By.CssSelector("#assigned > div"));


        public void AddRole()
        {
            IWaitAndClick(linkAdd);
            _wait.IWaitForLoader();
            ISee(saveButton, By.XPath("//button[contains(.,'Save')]"));
            ISee(nameInputField, By.XPath("//input[@name='Name']"));
            ISee(addAllLink, By.CssSelector("a[data-bind='click: addAll'] strong"));
            ISee(availableColumn, By.Id("available"));
            ISeeElements(By.CssSelector("#available > div"));
            ISee(assignedColumn, By.Id("assigned"));
            //IWaitForElementAndType(nameInputField, "DenisAutomationRole" + randomNumber);
            IWaitForElementAndType(nameInputField, "DenisAutomationRole2");
            IWaitAndClick(addAllLink);
            ISeeElements(By.CssSelector("#assigned > div"));
            IWaitAndClick(saveButton);
            _wait.IWaitForLoader();
            _wait.WaitForAjax();
            IWaitForElementAndType(searchArea, "DenisAutomationRole2");
            IWaitAndClick(editButton);
            _wait.IWaitForLoader();
            ISeeElements(By.CssSelector("#assigned > div"));
        }
    }
}
 