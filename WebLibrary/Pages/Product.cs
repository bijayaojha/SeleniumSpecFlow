using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WebLibrary
{
   public class Product
    {
        public IWebDriver WebDriver { get; }
        public Product(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        private IList<IWebElement> Size => WebDriver.FindElements(By.XPath("//select[@id='group_1']//option"));
        private IWebElement SelectedSize => WebDriver.FindElement(By.XPath("//div[@id='uniform-group_1']/span"));
        private IWebElement AddToCartButton => WebDriver.FindElement(By.XPath("//p[@id='add_to_cart']"));
        private IWebElement TotalProducts => WebDriver.FindElement(By.XPath("//span[@class='ajax_block_products_total']"));
        private IWebElement TotalShipping => WebDriver.FindElement(By.XPath("//span[@class='ajax_cart_shipping_cost']"));
        private IWebElement Total => WebDriver.FindElement(By.XPath("//span[@class='ajax_block_cart_total']"));
        private IWebElement Price => WebDriver.FindElement(By.Id("our_price_display"));
        private IWebElement ProductName => WebDriver.FindElement(By.XPath("//h1[@itemprop='name']"));
        private IList<IWebElement> Actions => WebDriver.FindElements(By.XPath("//div[@class='button-container']//span"));

        public void SelectSize(string size)
        {
            Size.Where(a => a.GetAttribute("title").Contains(size)).FirstOrDefault().Click();
            string selectedSize = SelectedSize.Text;
        }

        public Tuple<string, string, string> GetProductDetail()
        {
            Tuple<string, string, string> details = new Tuple<string, string, string>(
                SelectedSize.Text, Price.Text, ProductName.Text
                );
            return details;
        }


        public void GetCartValues(out string totalProducts, out string totalShipping, out string total)
        {
            Thread.Sleep(5000);
            totalProducts = TotalProducts.Text;
            totalShipping = TotalShipping.Text;
            total = Total.Text;
        }

        public void AddToCart() {
            IJavaScriptExecutor js = (IJavaScriptExecutor)WebDriver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", AddToCartButton);
            AddToCartButton.Click(); 
        }

        public void GetCartValues(out double totalProducts, out double totalShipping, out object total)
        {
            throw new NotImplementedException();
        }

        public void SelectAction(string action)
        {
            Thread.Sleep(5000);
            foreach (IWebElement element in Actions)
            {
                if (element.Text.Contains(action))
                {
                    element.ClickOnIt();

                    break;
                }
            }
        }
    }
}
