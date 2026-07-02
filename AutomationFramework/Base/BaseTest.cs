using NUnit.Framework;
using OpenQA.Selenium;
using AutomationFramework.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace AutomationFramework.Base
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class BaseTest
    {
        protected IWebDriver driver;
        protected string baseUrl;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // SỬA ĐƯỜNG DẪN: Chỉ định rõ file nằm trong thư mục Config khi build ra
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("Config/appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            // SỬA FALLBACK URL: Đổi từ "https://your-web-url.com" thành localhost thực tế của bạn
            // Điều này đảm bảo ngay cả khi lỗi đọc file JSON, hệ thống vẫn dùng đúng URL của bạn
            baseUrl = config["BaseUrl"] ?? "https://fruitshop-mvc-app-hhhygeegh2fca3fm.southeastasia-01.azurewebsites.net/";
        }

        [SetUp]
        public void SetUp()
        {
            // Lấy driver thread-safe từ DriverManager
            driver = DriverManager.GetDriver();

            // Thiết lập thời gian chờ ngầm định
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            // Điều hướng tới trang chủ
            driver.Navigate().GoToUrl(baseUrl);
        }

        [TearDown]
        public void TearDown()
        {
            // Giải phóng trực tiếp driver tại đây để thỏa mãn bộ phân tích NUnit Analyzer
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                driver = null;
            }

            // Đồng thời giải phóng bộ nhớ của Thread hiện tại
            DriverManager.QuitDriver();
        }
    }
}