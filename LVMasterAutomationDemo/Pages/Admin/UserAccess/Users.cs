using LVPages.Pages.Portal;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Reflection.Metadata;

namespace LVPages.Pages.Admin.UserAccess
{
    public class Users : BasePage
    {
        private readonly IWait _wait;
        int randomNumber = (int)(new Random().NextInt64(2022) + 20);
        int NewRandomNumber = (int)(new Random().NextInt64(2022) + 20);
        public Users(IWebDriver driver, IWait wait) : base(driver)
        {
            _wait = wait;
        }
        public override string PageUrl => "https://loanvantage.dev/IBS/master/LVWEB/Admin/#/Users/";

        private IWebElement AddUserButton => driver.FindElement(By.XPath("//button[contains(.,'Add')]"));
        private IWebElement UploadButton => driver.FindElement(By.XPath("//button[contains(.,'Upload')]"));
        private IWebElement MobileButton => driver.FindElement(By.XPath("//button[contains(.,'Mobile')]"));
        private IWebElement SearchArea => driver.FindElement(By.XPath("//input[@class='search-query form-control']"));
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
        private IWebElement HostLoanOfficerField => driver.FindElement(By.XPath("//input[@placeholder='Loan Officer']"));
        private IWebElement LoanAssistantTab => driver.FindElement(By.XPath("//span[@class='k-link'][contains(.,'Loan Assistant')]"));
        private IWebElement LoanAssistantCheckbox => driver.FindElement(By.XPath("(//input[contains(@type,'checkbox')])[104]"));
        private IWebElement ApprovalAuthorityTab => driver.FindElement(By.XPath("//span[@class='k-link'][contains(.,'Approval Authority')]"));
        private IWebElement ApprovalAuthorityCheckbox => driver.FindElement(By.XPath("(//input[contains(@type,'checkbox')])[106]"));
        private IWebElement ApprovalAuthorityAddAllLink => driver.FindElement(By.LinkText("Add All"));
        private IWebElement ApprovalAuthorityRemoveAllLink => driver.FindElement(By.LinkText("Remove All"));

        public void VerifyUsersPage()
        {
            _wait.IWaitForElementToBeClickable(AddUserButton);
            _wait.IWaitForElementToBeClickable(UploadButton);
            _wait.IWaitForElementToBeClickable(MobileButton);
            ISeeElement(SearchArea, By.XPath("//input[@class='search-query form-control']"));
            ISeeElements(By.XPath("//div[contains(@class,'k-grid-content k-auto-scrollable')]//tr"));
            AssertDriverUrlIsEqualToPageUrl();
        }
        private void VerifyUsersRequiredFields()
        {
            ISeeElement(LoginField, By.XPath("//input[@placeholder='Login']"));
            ISeeElement(DisplayNameField, By.XPath("//input[@placeholder='Display Name']"));
            ISeeElement(FirstNameField, By.XPath("//input[@placeholder='First Name']"));
            ISeeElement(LastNameField, By.XPath("//input[@placeholder='Last Name']"));
            ISeeElement(EmailField, By.XPath("//input[@placeholder='Email Address']"));
        }
        public void AddUser()
        {
            IClick(AddUserButton);
            _wait.WaitForAjax();
            VerifyUsersRequiredFields();
            IType(LoginField, "AutoLoginName" + randomNumber);
            IType(DisplayNameField, "AutoDisplayName" + randomNumber);
            IType(FirstNameField, "AutoFirstName" + randomNumber);
            IType(LastNameField, "AutoLastName" + randomNumber);
            IType(EmailField, $"AutoEmail{randomNumber}@mailinator.com");
            IClick(LoanOfficerTab);
            IClick(LoanOfficerCheckbox);
            ISeeElement(HostLoanOfficerField, By.XPath("//input[@placeholder='Loan Officer']"));
            IClick(LoanAssistantTab);
            IClick(LoanAssistantCheckbox);
            IClick(ApprovalAuthorityTab);
            IClick(ApprovalAuthorityCheckbox);
            ISeeElement(ApprovalAuthorityAddAllLink, By.LinkText("Add All"));
            ISeeElement(ApprovalAuthorityRemoveAllLink, By.LinkText("Remove All"));
            IClick(CancelButton);
            //IClick(SaveButton);
            AssertThereIsNoErrorAndException();
        }

        public void EditUser()
        {
            IType(SearchArea, "AutoLoginName" + randomNumber);
            //_wait.WaitForOneItemInTheGrid();
            IClick(EditButton);
            _wait.WaitForAjax();
            CleanAllInputFields();
            IType(LoginField, "AutoLoginName" + NewRandomNumber);
            IType(DisplayNameField, "AutoDisplayName" + NewRandomNumber);
            IType(FirstNameField, "AutoFirstName" + NewRandomNumber);
            IType(LastNameField, "AutoLastName" + NewRandomNumber);
            IType(EmailField, $"AutoEmail{NewRandomNumber}@mailinator.com");
            IClick(CancelButton);
            //IClick(SaveButton);
            AssertThereIsNoErrorAndException();
        }
    }
}
