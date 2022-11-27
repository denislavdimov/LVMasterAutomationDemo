﻿using LVPages.IClasses;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace LVPages
{
    public class UserActions : IUserActions
    {
        protected IWebDriver driver;
        int timeout = 0;
        public UserActions(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void Click(IWebElement element)
        {
            try
            {
                for (int i = 5250; i > timeout;)
                {
                    if (element.Displayed && element.Enabled)
                    {
                        element.Click();
                        Thread.Sleep(1000);
                        break;
                    }
                    else if (timeout >= 5000)
                    {
                        Assert.IsTrue(element.Displayed && element.Enabled, 
                            "The Element is neither displayed nor enabled and cannot be clicked.");
                    }
                    else
                    {
                        Thread.Sleep(250);
                        timeout += 250;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"The element: {element} is not clickable");
                throw;
            }
        }

        public void FillInField(IWebElement element, string data)
        {
            try
            {
                for (int i = 5250; i > timeout;)
                {
                    if (element.Displayed && element.Enabled)
                    {
                        element.SendKeys(data);
                        Thread.Sleep(500);
                        break;
                    }
                    else if (timeout >= 5000)
                    {
                        Assert.IsTrue(element.Displayed && element.Enabled,
                            "The Element is neither displayed nor enabled and cannot fill in data.");
                    }
                    else
                    {
                        Thread.Sleep(250);
                        timeout += 250;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Cannot fill in data");
                throw;
            }
        }
        
        public void DragAndDrop(IWebElement element1, IWebElement element2)
        {
            var UserAction = new Actions(driver);
            try
            {
                var dragndrop = UserAction.ClickAndHold(element1)
                    .Pause(TimeSpan.FromSeconds(1))
                    .MoveToElement(element2)
                    .Pause(TimeSpan.FromSeconds(1))
                    .Release()
                    .Build();
                dragndrop.Perform();
            }
            catch (Exception)
            {
                Console.WriteLine("Drag and Drop element failed");
                throw;
            }
        }
    }
}