using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary
{
    public class Login
    {
        public IWebDriver WebDriver { get; }
        public Login(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        private IWebElement Email => WebDriver.FindElement(By.Id("email"));
        private IWebElement Password => WebDriver.FindElement(By.Id("passwd"));
        private IWebElement SignIn => WebDriver.FindElement(By.Id("SubmitLogin"));

        public void Signin(String userName , String password)
        {
            Email.SendKeys(userName);
            Password.SendKeys(password);
            SignIn.Click();
        }

      
    }
}
