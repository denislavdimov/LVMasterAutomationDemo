using OpenQA.Selenium;

namespace LVPages
{
    public interface IWait
    {
        void IWaitForLoaderToDissaper(int seconds);
        void WaitForAjax();
        void IWaitPageToLoad();
        void IWaitForElementToBeClickable(IWebElement element);
        void SetTimeout(int secondstowait);
        void ResetTimeoutToDefault();
        void IWaitForOneUserInTheGrid();
        //void WaitForOneUserInTheGrid();
    }
}