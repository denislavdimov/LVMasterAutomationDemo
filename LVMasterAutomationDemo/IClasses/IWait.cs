using OpenQA.Selenium;

namespace LVPages.IClasses
{
    public interface IWait
    {
        void ForAjax();
        void ForPageToLoad();
        void ForElementToBeClickable(IWebElement element);
        void ForItemInTheGrid(int Item, int NumberOfItems);
        void ForElement(By by);
        void ForElements(By by);
        void ForNoElement(By by);
        bool ToSee(By by);
        void ForNoErrorAndException();
        void ForTheLoader();
    }
}