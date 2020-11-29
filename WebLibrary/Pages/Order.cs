using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WebLibrary
{
    public class Order
    {
        public IWebDriver WebDriver { get; }
        public Order(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

       
        private IWebElement ConfirmMyOrder => WebDriver.FindElement(By.XPath("//*[text()='I confirm my order']"));
        private IWebElement PayByBankWire => WebDriver.FindElement(By.XPath("//a[@title='Pay by bank wire']"));
        private IList<IWebElement> ProceedToCheckout => WebDriver.FindElements(By.XPath("//*[contains(text(),'Proceed to checkout')]"));
        private IWebElement TermsOfService => WebDriver.FindElement(By.Id("cgv"));
        public IWebElement TotalProduct => WebDriver.FindElement(By.Id("total_product"));
        public IWebElement OrderConformation => WebDriver.FindElement(By.XPath("//p[@class='cheque-indent']//strong"));
        public IWebElement TotalShipping => WebDriver.FindElement(By.Id("total_shipping"));
        public IWebElement TotalPrice => WebDriver.FindElement(By.Id("total_price_without_tax"));
        private IList<IWebElement> Items => WebDriver.FindElements(By.XPath("//table[@id='cart_summary']//tbody//tr"));
        private IList<IWebElement> ItemsName => WebDriver.FindElements(By.XPath("//table[@id='cart_summary']//tbody//p[@class='product-name']/a"));
        private IList<IWebElement> ItemsDetails => WebDriver.FindElements(By.XPath("//table[@id='cart_summary']//tbody//small/a"));
        private IList<IWebElement> ItemsUnitPrice => WebDriver.FindElements(By.XPath("//table[@id='cart_summary']//tbody//span[@class='price']/span"));

        List<Tuple<string, string, string>> cartDetails = new List<Tuple<string, string, string>>();
        public List<Tuple<string, string, string>> GetCartDetails()
        {
            Thread.Sleep(5000);
            for (int i = 0; i < Items.Count; i++)
            {
                Tuple<string, string, string> details = new Tuple<string, string, string>(
                ItemsDetails[i].Text.Split(' ').Last(), ItemsUnitPrice[i].Text, ItemsName[i].Text
                );
                cartDetails.Add(details);
            }
            return cartDetails;
        }

        public void PlaceOrder()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)WebDriver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", ProceedToCheckout.Last());
            ProceedToCheckout.Last().ClickOnIt();
            ProceedToCheckout.Last().ClickOnIt();
            TermsOfService.ClickOnIt();
            ProceedToCheckout.Last().ClickOnIt();
            PayByBankWire.ClickOnIt();
            ConfirmMyOrder.ClickOnIt();
        }
    }
}
