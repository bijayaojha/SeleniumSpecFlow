using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebLibrary
{
    public class Women
    {
        public IWebDriver WebDriver { get; }
        public Women(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        Random rnd = new Random();
        private IList<IWebElement> Products => WebDriver.FindElements(By.XPath("//ul[@class='product_list grid row']//a[@class='product-name']"));


        public string SelectProduct()
        {
            
            var randomizedList = from item in Products
                                 orderby rnd.Next()
                                 select item;
            string productName = randomizedList.FirstOrDefault().Text;
            randomizedList.FirstOrDefault().Click();
            return productName;
        }

        public void SelectProductWhichIsNotAlreadyAddedToCart(string selectedProduct)
        {
           
            var randomizedList = from item in Products
                                 where !item.Text.Contains(selectedProduct)
                                 orderby rnd.Next()
                                 select item;
            string productName = randomizedList.FirstOrDefault().Text;
            randomizedList.FirstOrDefault().Click();
           
        }
    }
}
