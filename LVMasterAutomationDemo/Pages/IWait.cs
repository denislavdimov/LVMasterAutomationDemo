using OpenQA.Selenium;

namespace LVMasterAutomationDemo.Pages
{
    public interface IWait
    {
        void IWaitForLoader();
        void WaitForAjax();
        void IWaitUntilPageLoadsCompletely();
        void IWaitPageToLoad();
        void IWaitForElementToBeClickable(IWebElement element);
    }
}