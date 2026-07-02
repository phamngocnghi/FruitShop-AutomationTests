using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using FluentAssertions;
using System;

namespace AutomationFramework.Pages
{
    public class BasePage
    {
        protected readonly IWebDriver driver;
        protected readonly WebDriverWait wait;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public string GetPageTitle()
        {
            return driver.Title;
        }

        public string GetCurrentUrl()
        {
            return driver.Url;
        }

        public void WaitForPageLoad()
        {
            wait.Until(d => ((IJavaScriptExecutor)d)
                .ExecuteScript("return document.readyState").Equals("complete"));
        }

        public void VerifyUrlContains(string expectedUrlPart)
        {
            GetCurrentUrl().Should().Contain(expectedUrlPart);
        }
    }
}