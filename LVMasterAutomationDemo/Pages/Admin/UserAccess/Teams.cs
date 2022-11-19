﻿using OpenQA.Selenium;

namespace LVPages.Pages.Admin.UserAccess
{
    public class Teams : BasePage
    {
        private IWait _wait;
        int randomNumber = (int)(new Random().NextInt64(2022) + 20);
        public Teams(IWebDriver driver, IWait wait) : base(driver)
        {
            _wait = wait;
        }
        public override string PageUrl => "https://loanvantage.dev/IBS/master/LVWEB/Admin/#/Teams/";

        private IWebElement NoticeModal => driver.FindElement(By.XPath("//div[@class='k-widget k-window']"));
        private IWebElement NoticeCloseButton => driver.FindElement(By.CssSelector(".k-icon.k-i-close"));
        private IWebElement LinkAdd => driver.FindElement(By.LinkText("Add"));
        public IWebElement TeamsModal => driver.FindElement(By.XPath("//div[@class='k-widget k-window']"));
        private IWebElement EditButton => driver.FindElement(By.XPath("//a[contains(@class,'v-icon icon-edit k-grid-Edit')]"));
        private IWebElement DeleteButton => driver.FindElement(By.XPath("//button[contains(.,'Delete')]"));
        private IWebElement SearchArea => driver.FindElement(By.XPath("//input[contains(@class,'search-query form-control')]"));
        private IWebElement NameInputField => driver.FindElement(By.XPath("//input[@name='Name']"));
        private IWebElement SaveButton => driver.FindElement(By.XPath("//button[contains(.,'Save')]"));
        private IWebElement UserAssignmentTab => driver.FindElement(By.XPath("//span[contains(.,'User Assignment')]"));
        private IWebElement RoleAssignmentTab => driver.FindElement(By.XPath("//span[contains(.,'Role Assignment')]"));
        private IWebElement ApprovalsTab => driver.FindElement(By.XPath("//span[@unselectable='on'][contains(.,'Approvals')]"));
        private IList<IWebElement> AllAssignedItems => driver.FindElements(By.CssSelector("#assigned > div")).ToList();
        private IList<IWebElement> AllAvailableItems => driver.FindElements(By.CssSelector("#available > div")).ToList();
        private IWebElement AvailableItem => driver.FindElement(By.CssSelector("#available > div"));
        private IWebElement AssignedItem => driver.FindElement(By.CssSelector("#assigned > div"));
        private IWebElement ConfirmationDialog => driver.FindElement(By.CssSelector(".confimation-dialog h5"));
        private IWebElement YesButton => driver.FindElement(By.XPath("//button[contains(.,'Yes')]"));


        public void AssignUserAndRoleToTeam()
        {
            //Add verify that assigned column is empty before assigning items
            IWaitAndClick(UserAssignmentTab);
            ISeeElement(AvailableItem, By.CssSelector("#available > div"));
            IWaitAndClick(AllAvailableItems[1]);
            ISeeElement(AssignedItem, By.CssSelector("#assigned > div"));
            //IWaitAndClick(RoleAssignmentTab);
            //IWaitAndClick(AllAvailableItems[4]);
            //ISeeElement(AssignedItem, By.CssSelector("#assigned > div"));
        }

        public void AddTeamWithUserAndRole()
        {
            ISeeElement(NoticeModal, By.XPath("//div[@class='k-widget k-window']"));
            IWaitAndClick(NoticeCloseButton);
            IWaitAndClick(LinkAdd);
            _wait.WaitForAjax();
            ISeeElement(TeamsModal, By.XPath("//div[@class='k-widget k-window']"));
            IWaitForElementAndType(NameInputField, "DenisAutomationTeamTest" + randomNumber);
            AssignUserAndRoleToTeam();
            IWaitAndClick(SaveButton);
            _wait.WaitForAjax();
            AssertThereIsNoErrorAndException();
        }

        public void EditTeam()
        {
            IWaitForElementAndType(SearchArea, "DenisAutomationTeamTest");
            IWaitAndClick(EditButton);
            _wait.WaitForAjax();
            ISeeElement(TeamsModal, By.XPath("//div[@class='k-widget k-window']"));
            IWaitAndClick(UserAssignmentTab);
            ISeeElement(AvailableItem, By.CssSelector("#available > div"));
            IWaitAndClick(AllAvailableItems[10]);
            ISeeElement(AssignedItem, By.CssSelector("#assigned > div"));
            IWaitAndClick(SaveButton);
            _wait.WaitForAjax();
            AssertThereIsNoErrorAndException();
        }

        public void DeleteTeam()
        {
            //IWaitForElementAndType(SearchArea, "DenisAutomationTeam");
            IWaitAndClick(EditButton);
            _wait.WaitForAjax();
            ISeeElement(TeamsModal, By.XPath("//div[@class='k-widget k-window']"));
            IWaitAndClick(DeleteButton);
            ISeeElement(ConfirmationDialog, By.CssSelector(".confimation-dialog h5"));
            IWaitAndClick(YesButton);
            _wait.WaitForAjax();
            AssertThereIsNoErrorAndException();
        }
    }
}