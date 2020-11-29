using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumSpecFlow.Utilities
{
    public class DriverFactory
    {
        public IWebDriver WebDriver { get; private set; }

        public IWebDriver InitializeDriver(BrowserType browser)
        {
            
            switch(browser)
            {
                case BrowserType.Chrome:
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    ChromeOptions options = new ChromeOptions();
                    options.AddArguments("start-maximized");
                    WebDriver = new ChromeDriver(options);
                    break;
                default:
                    // code block
                    break;

            }
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(2);
            WebDriver.Url = Config.WebUrl;
            return WebDriver;
        }

       
    }
}
