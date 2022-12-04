using LVPages.IClasses;
using OpenQA.Selenium;

namespace LVPages.Pages.Admin.UserAccess
{
    public class Users : BasePage
    {
        private readonly IWait Wait;
        private readonly IUserActions I;

        public Users(IWebDriver driver) : base(driver)
        {
            Wait = new Wait(driver);
            I = new UserActions(driver);
        }

        public override string PageUrl => "https://loanvantage.dev/IBS/master/LVWEB/Admin/#/Users/";

        private IWebElement AddUserButton => driver.FindElement(By.XPath("//button[contains(.,'Add')]"));
        private IWebElement UploadButton => driver.FindElement(By.XPath("//button[contains(.,'Upload')]"));
        private IWebElement MobileButton => driver.FindElement(By.XPath("//button[contains(.,'Mobile')]"));
        private IWebElement SearchInputArea => driver.FindElement(By.XPath("//input[@class='search-query form-control']"));
        private IList<IWebElement> GridItems => driver.FindElements(By.XPath("//div[contains(@class,'k-grid-content k-auto-scrollable')]//tr")).ToList();
        public IWebElement UsersModal => driver.FindElement(By.XPath("//div[@class='k-widget k-window']"));
        private IWebElement EditButton => driver.FindElement(By.XPath("//a[contains(@class,'v-icon icon-edit k-grid-Edit')]"));
        private IWebElement SaveButton => driver.FindElement(By.XPath("//button[contains(.,'Save')]"));
        private IWebElement CancelButton => driver.FindElement(By.XPath("//button[contains(.,'Cancel')]"));
        private IWebElement LoginField => driver.FindElement(By.XPath("//input[@placeholder='Login']"));
        private IWebElement DisplayNameField => driver.FindElement(By.XPath("//input[@placeholder='Display Name']"));
        private IWebElement FirstNameField => driver.FindElement(By.XPath("//input[@placeholder='First Name']"));
        private IWebElement LastNameField => driver.FindElement(By.XPath("//input[@placeholder='Last Name']"));
        private IWebElement EmailField => driver.FindElement(By.XPath("//input[@placeholder='Email Address']"));
        private IWebElement LoanOfficerTab => driver.FindElement(By.XPath("//span[@class='k-link'][contains(.,'Loan Officer')]"));
        private IWebElement LoanOfficerCheckbox => driver.FindElement(By.XPath("(//input[contains(@type,'checkbox')])[103]"));
        private IWebElement LoanAssistantTab => driver.FindElement(By.XPath("//span[@class='k-link'][contains(.,'Loan Assistant')]"));
        private IWebElement LoanAssistantCheckbox => driver.FindElement(By.XPath("(//input[contains(@type,'checkbox')])[104]"));
        private IWebElement ApprovalAuthorityTab => driver.FindElement(By.XPath("//span[@class='k-link'][contains(.,'Approval Authority')]"));
        private IWebElement ApprovalAuthorityCheckbox => driver.FindElement(By.XPath("(//input[contains(@type,'checkbox')])[106]"));

        private By SearchArea = By.XPath("//input[@class='search-query form-control']");
        private By UsersInTheGrid = By.XPath("//div[contains(@class,'k-grid-content k-auto-scrollable')]//tr");
        private By HostLoanOfficerField = By.XPath("//input[@placeholder='Loan Officer']");
        private By ApprovalAuthorityAddAllLink = By.LinkText("Add All");
        private By ApprovalAuthorityRemoveAllLink = By.LinkText("Remove All");

        private IList<string> RequiredFields()
        {
            List<string> requiredfields = new List<string>()
            {
                "Login",
                "Display Name",
                "First Name",
                "Last Name"
            };
            return requiredfields.AsReadOnly();
        }
        public void VerifyUsersPage()
        {
            Wait.ForElementToBeClickable(AddUserButton);
            Wait.ForElementToBeClickable(UploadButton);
            Wait.ForElementToBeClickable(MobileButton);
            Wait.ForElement(SearchArea);
            Wait.ForElements(UsersInTheGrid);
            AssertDriverUrlIsEqualToPageUrl();
        }

        private void FillInTheRequiredFields(IList<string> ReqFields)
        {
            foreach (var field in ReqFields)
            {
                var RequiredField = driver.FindElement(By.XPath($"//input[@placeholder='{field}']"));
                I.FillInField(RequiredField, $"Auto{field}{randomNumber}");
            }
            I.FillInField(EmailField, $"AutoEmail{randomNumber}@mailinator.com");
        }

        public void AddUser()
        {
            I.Click(AddUserButton);
            Wait.ForAjax();
            FillInTheRequiredFields(RequiredFields());
            I.Click(LoanOfficerTab);
            I.Click(LoanOfficerCheckbox);
            Wait.ForElement(HostLoanOfficerField);
            I.Click(LoanAssistantTab);
            I.Click(LoanAssistantCheckbox);
            I.Click(ApprovalAuthorityTab);
            I.Click(ApprovalAuthorityCheckbox);
            Wait.ForElement(ApprovalAuthorityAddAllLink);
            Wait.ForElement(ApprovalAuthorityRemoveAllLink);
            I.Click(CancelButton);
            //IClick(SaveButton);
            Wait.ForNoErrorAndException();
        }

        public void EditUser()
        {
            I.FillInField(SearchInputArea, $"AutoLoginName{randomNumber}");
            I.FillInField(SearchInputArea, "DDimov");
            Wait.ForAjax();
            Wait.ForItemInTheGrid(GridItems.Count, 1);
            I.Click(EditButton);
            Wait.ForAjax();
            ClearAllInputFields();
            I.FillInField(LoginField, $"AutoLoginName{NewRandomNumber}");
            I.FillInField(DisplayNameField, $"AutoDisplayName{NewRandomNumber}");
            I.FillInField(FirstNameField, $"AutoFirstName{NewRandomNumber}");
            I.FillInField(LastNameField, $"AutoLastName{NewRandomNumber}");
            I.FillInField(EmailField, $"AutoEmail{NewRandomNumber}@mailinator.com");
            I.Click(CancelButton);
            //IClick(SaveButton);
            Wait.ForNoErrorAndException();
        }
    }
}
