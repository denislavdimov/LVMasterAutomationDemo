using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace LVMasterAutomationDemo.Pages
{
    public interface IWait
    {
        //void IWaitAndClick(IWebElement element);
        //void IWaitForElementAndType(IWebElement element, string data);
        void IWaitForLoader();
        void WaitForAjax();
        void IWaitUntilPageLoadsCompletely();
        void IWaitPageToLoad();
        void IWaitForElementToBeClickable(IWebElement element);
    }
}