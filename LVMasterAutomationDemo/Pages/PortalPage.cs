using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVMasterAutomationDemo.Pages
{
    public class PortalPage : BasePage
    {
        public PortalPage (IWebDriver driver) : base(driver)
        {
        }
        public override string PageUrl => "url";


    }
}
