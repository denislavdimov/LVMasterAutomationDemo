using OpenQA.Selenium;

namespace LVMasterAutomationDemo.Pages
{
    public interface IWait
    {
        void IWaitForLoaderToDissaper();
        void WaitForAjax();
        void IWaitPageToLoad();
        void IWaitForElementToBeClickable(IWebElement element);
        void SetTimeout(int secondstowait);
        void ResetTimeoutToDefault();
    }
}