using OpenQA.Selenium;

namespace LVMasterAutomationDemo.Pages
{
    public interface IWait
    {
        void IWaitForLoaderToDissaper();
        void WaitForAjax();
        void IWaitUntilPageLoadsCompletely();
        void IWaitPageToLoad();
        void IWaitForElementToBeClickable(IWebElement element);
        void WaitForAjax2();
    }
}