using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary
{
    public class Home
    {

        public IWebDriver WebDriver { get; }

        public Home(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        private IWebElement SighIn => WebDriver.FindElement(By.XPath("//a[@title='Log in to your customer account']"));
        private IWebElement SighOut => WebDriver.FindElement(By.XPath("//a[@title='Log me out']"));
        public void ClickSignIn() => SighIn.Click();
        public void ClickSignOut() => SighOut.Click();
    }
}
