using NUnit.Framework;
using SeleniumSpecFlow.Utilities;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;


namespace SeleniumSpecFlow.Steps
{
    [Binding]
    public class AutomationPracticeTestScenariosSteps : ObjectFactory
    {
        private string selectedProduct;
        private string totalProducts;
        private string totalShipping;
        private string total;
        List<Tuple<string, string, string>> totalProductDetails = new List<Tuple<string, string, string>>();
        private List<Tuple<string, string, string>> cartDetails;
        private string orderDate;
        private string orderNumber;
        private string orderMessage;

        [When(@"User logged into application")]
        public void WhenUserLoggedIntoApplication()
        {
            Home.Value.ClickSignIn();
            Login.Value.Signin(Config.Username, Config.Password);
        }

        [Given(@"I select ""(.*)"" from the menu")]
        public void GivenISelectFromTheMenu(string menu)
        {
            MyAccount.Value.SelectMenu(menu);
        }

        [When(@"I select a product from catalogue")]
        public void WhenISelectAProductFromCatalogue()
        {
            selectedProduct = Women.Value.SelectProduct();
        }

        [When(@"I select size ""(.*)"" from size")]
        public void WhenISelectSizeFromSize(string size)
        {
            Product.Value.SelectSize(size);
        }

        [Then(@"I click on Add To Cart")]
        public void ThenIClickOnAddToCart()
        {
            Product.Value.AddToCart();
        }

        [Then(@"I selected ""(.*)"" action")]
        public void ThenISelectedAction(string action)
        {
            Product.Value.SelectAction(action);
        }

        [When(@"I select a product from catalogue which is not already added to cart")]
        public void WhenISelectAProductFromCatalogueWhichIsNotAlreadyAddedToCart()
        {
            Women.Value.SelectProductWhichIsNotAlreadyAddedToCart(selectedProduct);
        }

        [Then(@"I get the details of Cart")]
        public void ThenIGetTheDetailsOfCart()
        {
            Product.Value.GetCartValues(out totalProducts, out totalShipping, out total);
        }

        [When(@"I get the product details")]
        public void WhenIGetTheProductDetails()
        {
            Tuple<string, string, string> productDetail = Product.Value.GetProductDetail();
            totalProductDetails.Add(productDetail);
        }

        [When(@"I get the cart details")]
        public void WhenIGetTheCartDetails()
        {
            cartDetails = Order.Value.GetCartDetails();
        }

        [Then(@"I validate cart details")]
        public void ThenIValidateCartDetails()
        {
            Assert.AreEqual(totalProductDetails, cartDetails);
            Assert.AreEqual(totalProducts, Order.Value.TotalProduct.Text);
            Assert.AreEqual(totalShipping, Order.Value.TotalShipping.Text);
            Assert.AreEqual(total, Order.Value.TotalPrice.Text);
        }

        [When(@"I placed order using ""(.*)""")]
        public void WhenIPlacedOrderUsing(string orderMethod)
        {
            Order.Value.PlaceOrder();
        }

        [Then(@"I validate order conformation")]
        public void ThenIValidateOrderConformation()
        {
            Assert.AreEqual("Your order on My Store is complete.", Order.Value.OrderConformation.Text);
        }
        [Then(@"I sign out of the application")]
        public void ThenISignOutOfTheApplication()
        {
            Home.Value.ClickSignOut();
        }
        [When(@"I select Order history and details")]
        public void WhenISelectOrderHistoryAndDetails()
        {
            MyAccount.Value.ClickOrderHistory();
        }
        [When(@"I select a random order from existing orders")]
        public void WhenISelectARandomOrderFromExistingOrders()
        {
            MyAccount.Value.SelectRandomOrder(out orderNumber, out orderDate);
        }
        [Then(@"I validate the message")]
        public void ThenIValidateTheMessage()
        {
            Assert.AreEqual(orderMessage, MyAccount.Value.GetOrderMessage());
        }
        [When(@"I enter a message")]
        public void WhenIEnterAMessage()
        {
            orderMessage = MyAccount.Value.EnterOrderMessage();
        }
        [Then(@"I validate order details")]
        public void ThenIValidateOrderDetails()
        {
            Assert.IsTrue(MyAccount.Value.SelectedOrder.Text.Contains(orderDate));
            Assert.IsTrue(MyAccount.Value.SelectedOrder.Text.Contains(orderNumber));
        }
        [Then(@"I validate the message for negative scenario")]
        public void ThenIValidateTheMessageForNegativeScenario()
        {
            Assert.AreEqual("Wrong Message", MyAccount.Value.GetOrderMessage());
        }

    }
}
