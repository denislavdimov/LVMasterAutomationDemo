using OpenQA.Selenium;

namespace LVPages.IClasses
{
    public interface IWait
    {
        void ForLoaderToDissaper();
        void ForAjax();
        void ForPageToLoad();
        void ForElementToBeClickable(IWebElement element);
        void ForItemInTheGrid(int Item, int NumberOfItems);
        void ToSeeElements(By by);
        void ForNoErrorAndException();
        void ToSeeElement(By by);
    }
}