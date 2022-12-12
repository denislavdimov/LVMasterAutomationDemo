using OpenQA.Selenium;

namespace LVPages.IClasses
{
    public interface IUserActions
    {
        void Click(IWebElement element);
        void FillInField(IWebElement element, string data);
        void SelectItemFromDropdown(IWebElement dropdown, int itemnumber);
    }
}