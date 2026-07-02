using NUnit.Framework;
using FluentAssertions;
using Allure.Net.Commons;
using Allure.Net.Commons.Attributes;
using Allure.NUnit;
using AutomationFramework.Base;
using AutomationFramework.Pages;

namespace AutomationFramework.Tests
{
    [AllureEpic("Authentication")]
    [AllureFeature("Login Functionality")]
    [AllureNUnit] // Thuộc tính bắt buộc để kích hoạt báo cáo Allure khi chạy test
    [TestFixture]
    public class LoginTests : BaseTest
    {
        private LoginPage _loginPage;
        private HomePage _homePage;

        [SetUp]
        public void InitPages()
        {
            _loginPage = new LoginPage(driver, baseUrl);
            _homePage = new HomePage(driver);
        }

        [Test]
        [AllureName("TC001 - Verify Login with valid credentials")]
        [AllureDescription("Test case: Verify user can login with valid username and password")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureTag("login", "positive", "smoke")]
        [Category("Smoke")]
        public void TestLoginWithValidCredentials()
        {
            // Arrange - Tài khoản thật của bạn trên Localhost
            string username = "nghipn.work@gmail.com";
            string password = "Aa@12345678";

            // Act
            _loginPage.NavigateTo();
            _loginPage.EnterUsername(username);
            _loginPage.EnterPassword(password);
            _loginPage.ClickLoginButton();

            // Assert
            _homePage.IsLogoutButtonDisplayed()
                .Should().BeTrue("Logout button should be displayed after successful login");

            _homePage.IsWelcomeMessageDisplayed()
                .Should().BeTrue("Welcome message should be displayed");
        }

        [Test]
        [AllureName("TC002 - Verify Login with invalid credentials")]
        [AllureDescription("Test case: Verify user cannot login with invalid credentials")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureTag("login", "negative")]
        [Category("Regression")]
        public void TestLoginWithInvalidCredentials()
        {
            // Arrange
            string username = "invaliduser";
            string password = "WrongPassword";

            // Act
            _loginPage.NavigateTo();
            _loginPage.EnterUsername(username);
            _loginPage.EnterPassword(password);
            _loginPage.ClickLoginButton();

            // Assert
            _loginPage.GetCurrentUrl().Should().Contain("/Identity/Account/Login",
                "User should remain on login page with invalid credentials");
        }

        [Test]
        [AllureName("TC003 - Verify Login with empty username")]
        [AllureDescription("Test case: Verify validation when username is empty")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureTag("login", "validation")]
        [Category("Regression")]
        public void TestLoginWithEmptyUsername()
        {
            // Arrange
            string username = "";
            string password = "Test@123";

            // Act
            _loginPage.NavigateTo();
            _loginPage.EnterUsername(username);
            _loginPage.EnterPassword(password);
            _loginPage.ClickLoginButton();

            // Assert
            _loginPage.GetCurrentUrl().Should().Contain("/Identity/Account/Login",
                "Should show validation error for empty username");
        }

        [Test]
        [AllureName("TC006 - Verify Logout functionality")]
        [AllureDescription("Test case: Verify user can logout successfully")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureTag("logout", "positive")]
        [Category("Smoke")]
        public void TestLogout()
        {
            // Arrange - ĐÃ SỬA ĐĂNG NHẬP BẰNG TÀI KHOẢN THẬT CỦA BẠN
            _loginPage.NavigateTo();
            _homePage = _loginPage.Login("nghipn.work@gmail.com", "Aa@12345678");

            // Act - Đăng xuất
            _homePage.ClickLogout();

            // Assert
            _homePage.GetCurrentUrl().Should().NotContain("/Dashboard",
                "User should be logged out");
        }
    }
}