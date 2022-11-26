using OpenQA.Selenium;

namespace LVPages.IClasses
{
    public interface IWait
    {
        void ForLoaderToDissaper(int seconds);
        void ForAjax();
        void ForPageToLoad();
        void ForElementToBeClickable(IWebElement element);
        void SetTimeout(int secondstowait);
        void ResetTimeoutToDefault();
        void ForOneItemInTheGrid(int item);
    }
}