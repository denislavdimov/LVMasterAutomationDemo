using LVPages.IClasses;
using OpenQA.Selenium;

namespace LVPages.Pages.Admin.UserAccess
{
    public class Roles : BasePage
    {
        private readonly IWait Wait;
        private readonly IUserActions I;
        //int randomNumber = (int)(new Random().NextInt64(2022) + 20);
        public Roles(IWebDriver driver) : base(driver)
        {
            Wait = new Wait(driver);
            I = new UserActions(driver);
        }
        public override string PageUrl => "https://loanvantage.dev/IBS/master/LVWEB/Admin/#/Roles/";
        private IWebElement NameInputField => driver.FindElement(By.XPath("//input[@name='Name']"));
        private IWebElement LinkAdd => driver.FindElement(By.LinkText("Add"));
        private IWebElement SearchArea => driver.FindElement(By.XPath("//input[contains(@class,'search-query form-control')]"));
        private IWebElement DeleteButton => driver.FindElement(By.XPath("//button[contains(.,'Delete')]"));
        private IWebElement AddAllLink => driver.FindElement(By.CssSelector("a[data-bind='click: addAll'] strong"));
        private IWebElement SaveButton => driver.FindElement(By.XPath("//button[contains(.,'Save')]"));
        private IWebElement YesButton => driver.FindElement(By.XPath("//button[contains(.,'Yes')]"));
        private IWebElement EditButton => driver.FindElement(By.XPath("(//a[contains(@class,'v-icon icon-edit k-grid-Edit')])"));
        private IList<IWebElement> AvailableItems => driver.FindElements(By.CssSelector("#admin-menu-role-edit #available div")).ToList();
        private IList<IWebElement> AssignedItems => driver.FindElements(By.CssSelector("#admin-menu-role-edit #assigned div")).ToList();
        private IList<IWebElement> GridItems => driver.FindElements(By.XPath("//div[contains(@class,'k-grid-content k-auto-scrollable')]//tr")).ToList();

        private By SearchInputArea = By.XPath("//input[contains(@class,'search-query form-control')]");
        private By RolesGrid = By.CssSelector("#roles-kendo-grid tr");
        private By RoleModal = By.XPath("//div[@class='k-widget k-window']");
        private By RoleAssignedItems = By.CssSelector("#admin-menu-role-edit #assigned div");
        private By ConfirmationDialog = By.CssSelector(".confimation-dialog h5");

        public void VerifyRolesPage()
        {
            Wait.ForElementToBeClickable(LinkAdd);
            Wait.ToSeeElement(SearchInputArea);
            Wait.ToSeeElements(RolesGrid);
            AssertDriverUrlIsEqualToPageUrl();
        }

        public void AddRole()
        {
            I.Click(LinkAdd);
            Wait.ForAjax();
            Wait.ToSeeElement(RoleModal);
            //ISeeElements(By.CssSelector("#admin-menu-role-edit #available div"));
            I.Click(AddAllLink);
            Wait.ToSeeElements(RoleAssignedItems);
            I.FillInField(NameInputField, $"DenisAutomationRoleTest{randomNumber}");
            I.Click(SaveButton);
            Wait.ForAjax();
            Wait.ForNoErrorAndException();
        }

        public void DeleteRole()
        {
            I.FillInField(SearchArea, $"DenisAutomationRoleTest{randomNumber}");
            Wait.ForItemInTheGrid(GridItems.Count, 1);
            I.Click(EditButton);
            Wait.ForAjax();
            Wait.ToSeeElement(RoleModal);
            Wait.ToSeeElements(RoleAssignedItems);
            I.Click(DeleteButton);
            Wait.ToSeeElement(ConfirmationDialog);
            I.Click(YesButton);
            Wait.ForAjax();
            Wait.ForNoErrorAndException();
        }
    }
}
