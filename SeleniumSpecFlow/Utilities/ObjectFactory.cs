using System;
using System.Collections.Generic;
using System.Text;
using WebLibrary;

namespace SeleniumSpecFlow.Utilities
{
    public class ObjectFactory
    {
        public Lazy<Home> Home = new Lazy<Home>(() => new Home(Hooks.Driver));
        public Lazy<Login> Login = new Lazy<Login>(() => new Login(Hooks.Driver));
        public Lazy<MyAccount> MyAccount = new Lazy<MyAccount>(() => new MyAccount(Hooks.Driver));
        public Lazy<Women> Women = new Lazy<Women>(() => new Women(Hooks.Driver));
        public Lazy<Product> Product = new Lazy<Product>(() => new Product(Hooks.Driver));
        public Lazy<Order> Order = new Lazy<Order>(() => new Order(Hooks.Driver));
        public Lazy<DriverFactory> DriverFactory = new Lazy<DriverFactory>();
    }

}
