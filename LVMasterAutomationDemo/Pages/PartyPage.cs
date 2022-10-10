using OpenQA.Selenium;

namespace LVMasterAutomationDemo.Pages
{
    public class PartyPage : BasePage
    {
        public PartyPage(IWebDriver driver) : base(driver)
        {
        }
        public override string PageUrl => "https://loanvantage.dev/IBS/master/lvweb/Layout/#/Party/";

        public IWebElement searchButton => driver.FindElement(By.XPath("//button[contains(.,'Search')]"));
    }
}
