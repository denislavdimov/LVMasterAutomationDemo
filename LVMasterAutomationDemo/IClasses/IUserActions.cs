using OpenQA.Selenium;

namespace LVPages.IClasses
{
    public interface IUserActions
    {
        void Click(IWebElement element);
        void Type(IWebElement element, string data);
        void DragAndDrop(IWebElement element1, IWebElement element2);
    }
}