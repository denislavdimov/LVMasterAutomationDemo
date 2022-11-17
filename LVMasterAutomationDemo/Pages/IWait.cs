using OpenQA.Selenium;

namespace LVMasterAutomationDemo.Pages
{
    public interface IWait
    {
        void IWaitForLoaderToDissaper(int seconds);
        void WaitForAjax();
        void IWaitPageToLoad();
        void IWaitForElementToBeClickable(IWebElement element);
        void SetTimeout(int secondstowait);
        void ResetTimeoutToDefault();
    }
}