using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WebLibrary
{
    public class MyAccount
    {
        public IWebDriver WebDriver { get; }
        public MyAccount(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        Random rnd = new Random();
        private string message;

        private IList<IWebElement> Menu => WebDriver.FindElements(By.XPath("//div[@id='block_top_menu']//a"));
        private IList<IWebElement> Orders => WebDriver.FindElements(By.XPath("//a[@class='color-myaccount']"));
        private IList<IWebElement> OrderDates => WebDriver.FindElements(By.XPath("//td[@class='history_date bold']"));
        private IWebElement OrderHistory => WebDriver.FindElement(By.XPath("//a[@title='Orders']"));
        public IWebElement SelectedOrder => WebDriver.FindElement(By.XPath("//form[@id='submitReorder']//p//strong"));
        public IWebElement EnterMessage => WebDriver.FindElement(By.XPath("//textarea[@name='msgText']"));
        public IWebElement SendMessage => WebDriver.FindElement(By.XPath("//*[text()='Send']"));
        private string ConfirmMessage => "//*[contains(text(),'{0}')]";
        public void SelectMenu(string menu)
        {
            Menu.Where(a => a.GetAttribute("title").Equals(menu)).FirstOrDefault().Click();
        }
        public void ClickOrderHistory() => OrderHistory.Click();
        public void SelectRandomOrder(out string orderNumber, out string orderDate)
        {
           
            IWebElement order = Orders[rnd.Next(Orders.Count)];
            order.Click();
            int index = Orders.IndexOf(order);
            orderNumber = Orders[index].Text;
            orderDate = OrderDates[index].Text;
        }

        public string EnterOrderMessage()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
             message=  new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
            EnterMessage.SendKeys(message);
            SendMessage.ClickOnIt();
            return message;
        }

        public string GetOrderMessage()
        {
            return WebDriver.FindElement(By.XPath(String.Format(ConfirmMessage, message))).Text;
        }
    }
}
