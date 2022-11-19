using OpenQA.Selenium;

namespace LVPages.Pages.Portal
{
    public class PartyPage : BasePage
    {
        public PartyPage(IWebDriver driver) : base(driver)
        {
        }
        public override string PageUrl => "https://loanvantage.dev/IBS/master/lvweb/Layout/#/Party/";

        public IWebElement SearchButton => driver.FindElement(By.XPath("//button[contains(.,'Search')]"));
    }
}
