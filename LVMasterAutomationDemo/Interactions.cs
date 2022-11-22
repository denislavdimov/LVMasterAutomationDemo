using OpenQA.Selenium;

namespace LVPages
{
    public static class Interactions
    {
        public static void Click(IWebElement element)
        {
            try
            {
                element.Click();
                Thread.Sleep(1000);
            }
            catch (Exception)
            {
                Console.WriteLine($"The {element} is not clickable");
                throw;
            }
        }

        public static void Type(IWebElement element, string data)
        {
            try
            {
                element.SendKeys(data);
            }
            catch (Exception)
            {
                Console.WriteLine("Cannot fill in data");
                throw;
            }
        }
    }
}
