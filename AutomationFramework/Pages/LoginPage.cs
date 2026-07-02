using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Allure.Net.Commons;
using Allure.Net.Commons.Attributes;
using System;

namespace AutomationFramework.Pages
{
    [AllureFeature("Authentication")]
    [AllureStory("Login")]
    public class LoginPage : BasePage
    {
        private readonly string _baseUrl;

        // Cập nhật bộ định vị (Locators) theo chuẩn ASP.NET Core Identity
        private By UsernameInput => By.Id("Input_Email"); // Thường là Input_Email
        private By PasswordInput => By.Id("Input_Password"); // Thường là Input_Password
        private By LoginButton => By.Id("login-submit"); // Thường là nút có id login-submit hoặc CSS button[type='submit']
        private By ErrorMessage => By.CssSelector(".validation-summary-errors");

        public LoginPage(IWebDriver driver, string baseUrl) : base(driver)
        {
            _baseUrl = baseUrl;
        }

        [AllureStep("Enter username")]
        public void EnterUsername(string username)
        {
            var element = wait.Until(d => d.FindElement(UsernameInput));
            element.Clear();
            element.SendKeys(username);
        }

        [AllureStep("Enter password")]
        public void EnterPassword(string password)
        {
            var element = wait.Until(d => d.FindElement(PasswordInput));
            element.Clear();
            element.SendKeys(password);
        }

        [AllureStep("Click login button")]
        public void ClickLoginButton()
        {
            var element = wait.Until(d => d.FindElement(LoginButton));
            element.Click();
        }

        [AllureStep("Login with credentials")]
        public HomePage Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLoginButton();
            return new HomePage(driver);
        }

        [AllureStep("Get error message")]
        public string GetErrorMessage()
        {
            var element = wait.Until(d => d.FindElement(ErrorMessage));
            return element.Text;
        }

        [AllureStep("Navigate to login page")]
        public void NavigateTo()
        {
            // SỬA ĐƯỜNG DẪN THỰC TẾ: Identity/Account/Login
            driver.Navigate().GoToUrl(_baseUrl + "/Identity/Account/Login");
        }
    }
}