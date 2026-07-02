using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace AutomationFramework.Utils
{
    public static class DriverManager
    {
        private static readonly ThreadLocal<IWebDriver> _driver = new ThreadLocal<IWebDriver>();

        public static IWebDriver GetDriver()
        {
            if (_driver.Value == null)
            {
                InitDriver();
            }
            return _driver.Value;
        }

        private static void InitDriver()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-infobars");

            // --- CẤU HÌNH QUAN TRỌNG ĐỂ TEST LOCALHOST HTTPS ---
            // Bỏ qua lỗi chứng chỉ SSL của localhost (Sửa lỗi UnknownError)
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--allow-insecure-localhost");
            // --------------------------------------------------

            // Bỏ comment dòng dưới nếu muốn chạy không giao diện (headless) trên CI/CD
            // options.AddArgument("--headless");

            _driver.Value = new ChromeDriver(options);
        }

        public static void QuitDriver()
        {
            if (_driver.Value != null)
            {
                _driver.Value.Quit();
                _driver.Value.Dispose();
                _driver.Value = null;
            }
        }
    }
}