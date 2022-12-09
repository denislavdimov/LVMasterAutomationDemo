using LVPages.IClasses;
using OpenQA.Selenium;

namespace LVPages.Pages.Admin.UserAccess
{
    public class Roles : BasePage
    {
        private readonly IWait Wait;
        private readonly IUserActions I;
        public Roles(IWebDriver driver) : base(driver)
        {
            Wait = new Wait(driver);
            I = new UserActions(driver);
        }
        public override string PageUrl => "https://loanvantage.dev/IBS/master/LVWEB/Admin/#/Roles/";
        private IWebElement LinkAdd => driver.FindElement(By.CssSelector("a[data-bind='click: add']"));
        private IWebElement NameInputField => driver.FindElement(By.CssSelector("input[name='Name']"));
        private IWebElement SearchArea => driver.FindElement(By.CssSelector("input[name='GridToolbarSearch']"));
        private IWebElement DeleteButton => driver.FindElement(By.CssSelector("button[data-toggle='delete-confirmation']"));
        private IWebElement YesButton => driver.FindElement(By.CssSelector("button[data-ui='confirmation-yes']"));
        private IWebElement AddAllLink => driver.FindElement(By.CssSelector("a[data-bind='click: addAll']"));
        private IWebElement SaveButton => driver.FindElement(By.CssSelector("button[data-bind='visible: permission.AdminRolesEdit, click: save']"));
        private IWebElement EditButton => driver.FindElement(By.CssSelector("a[class='v-icon icon-edit k-grid-Edit']"));
        private IList<IWebElement> AvailableItems => driver.FindElements(By.CssSelector("#admin-menu-role-edit #available div")).ToList();
        private IList<IWebElement> AssignedItems => driver.FindElements(By.CssSelector("#admin-menu-role-edit #assigned div")).ToList();
        private IList<IWebElement> GridItems => driver.FindElements(By.CssSelector("div[class='k-grid-content k-auto-scrollable'] tr")).ToList();

        private By SearchInputArea = By.CssSelector("input[name='GridToolbarSearch']");
        private By RolesGrid = By.CssSelector("#roles-kendo-grid tr");
        private By RoleModal = By.CssSelector("div[class='k-widget k-window']");
        private By RoleAssignedItems = By.CssSelector("#admin-menu-role-edit #assigned div");
        private By ConfirmationDialog = By.CssSelector(".confimation-dialog h5");

        public void VerifyRolesPage()
        {
            Wait.ForElementToBeClickable(LinkAdd);
            Wait.ForElement(SearchInputArea);
            Wait.ForElements(RolesGrid);
            AssertDriverUrlIsEqualToPageUrl();
        }

        public void AddRole()
        {
            I.Click(LinkAdd);
            Wait.ForAjax();
            Wait.ForElement(RoleModal);
            I.Click(AddAllLink);
            Wait.ForElements(RoleAssignedItems);
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
            Wait.ForElement(RoleModal);
            I.Click(DeleteButton);
            Wait.ForElement(ConfirmationDialog);
            I.Click(YesButton);
            Wait.ForAjax();
            Wait.ForNoErrorAndException();
            Wait.ForItemInTheGrid(GridItems.Count, 0);
        }
    }
}
