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
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--allow-insecure-localhost");

            // --- TỰ ĐỘNG CHUYỂN CHẾ ĐỘ KHI CHẠY TRÊN CI/CD (GITHUB ACTIONS) ---
            // Phát hiện biến môi trường "GITHUB_ACTIONS" do GitHub tự sinh ra
            bool isCiEnvironment = Environment.GetEnvironmentVariable("GITHUB_ACTIONS") == "true";
            if (isCiEnvironment)
            {
                options.AddArgument("--headless=new"); // Sử dụng engine headless mới của Chrome
                options.AddArgument("--no-sandbox"); // Vượt qua cơ chế sandbox bảo mật của Linux
                options.AddArgument("--disable-dev-shm-usage"); // Tránh lỗi tràn bộ nhớ dùng chung /dev/shm
                options.AddArgument("--window-size=1920,1080"); // Đặt kích thước màn hình chuẩn để không bị vỡ giao diện

                // --- CẤU HÌNH QUAN TRỌNG: CHẶN TẢI ẢNH ĐỂ TIẾT KIỆM BĂNG THÔNG AZURE ---
                // Giá trị 2 có nghĩa là chặn (Block) toàn bộ các tệp tin hình ảnh (.jpg, .png, .webp, v.v.)
                options.AddUserProfilePreference("profile.default_content_settings.images", 2);
            }
            // ------------------------------------------------------------------

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