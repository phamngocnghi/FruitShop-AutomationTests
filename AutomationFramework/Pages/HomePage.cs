using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Allure.Net.Commons;
using Allure.Net.Commons.Attributes;
using System;

namespace AutomationFramework.Pages
{
    [AllureFeature("Dashboard")]
    [AllureStory("Home")]
    public class HomePage : BasePage
    {
        // CẬP NHẬT BỘ ĐỊNH VỊ THÔNG MINH: Tìm thẻ liên kết chứa chữ 'Manage' trong thuộc tính href
        private By WelcomeMessage => By.CssSelector("a[href*='Manage']");

        // Nút Logout: Nút <button> nằm bên trong form Đăng xuất
        private By LogoutButton => By.CssSelector("form[action*='Logout'] button");

        private By CreatePostButton => By.CssSelector("a[href*='create']");

        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        [AllureStep("Check logout button displayed")]
        public bool IsLogoutButtonDisplayed()
        {
            try
            {
                return wait.Until(d => d.FindElement(LogoutButton).Displayed);
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        [AllureStep("Check welcome message displayed")]
        public bool IsWelcomeMessageDisplayed()
        {
            try
            {
                return wait.Until(d => d.FindElement(WelcomeMessage).Displayed);
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        [AllureStep("Get welcome message text")]
        public string GetWelcomeMessageText()
        {
            return wait.Until(d => d.FindElement(WelcomeMessage)).Text;
        }

        [AllureStep("Click logout")]
        public void ClickLogout()
        {
            var element = wait.Until(d => d.FindElement(LogoutButton));
            element.Click();
        }

        [AllureStep("Click create post")]
        public void ClickCreatePost()
        {
            var element = wait.Until(d => d.FindElement(CreatePostButton));
            element.Click();
        }
    }
}