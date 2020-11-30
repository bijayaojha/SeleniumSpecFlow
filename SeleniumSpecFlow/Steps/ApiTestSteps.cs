using ApiLibrary;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using SeleniumSpecFlow;
using SeleniumSpecFlow.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using TestLibrary.Utilities;

namespace TestLibrary.Steps
{
    [Binding]
    public class ApiTestSteps : ObjectFactory
    {
        public IRestResponse restResponse { get; private set; }

      
        [Given(@"I create request body for endpoint")]
        public void GivenICreateRequestBodyForEndpoint()
        {
            CreateUser.Value.job = Util.RandomString();
            CreateUser.Value.name = Util.RandomString();
        }
        [When(@"I requested ""(.*)"" for ""(.*)""")]
        public void WhenIRequestedFor(string method, string endPoint)
        {
            restResponse = ApiHelper.CreateRequest(CreateUser, method, Hooks.restClient, endPoint);
        }

        [Then(@"I validate the response for ""(.*)""")]
        public void ThenIValidateTheResponseFor(string p0)
        {
            var jObject = JObject.Parse(restResponse.Content);
            string value = jObject.GetValue("Value").ToString();
            string id = jObject.GetValue("id").ToString();
            string createdDate = jObject.GetValue("createdAt").ToString();

            Assert.IsTrue(value.Contains(CreateUser.Value.name));
            Assert.IsTrue(value.Contains(CreateUser.Value.job));
            Assert.IsNotNull(id);
            Assert.IsNotNull(createdDate);
        }

        [Then(@"I validate the response for /api/users/2")]
        public void ThenIValidateTheResponseForApiUsers2()
        {
            var jObject = JObject.Parse(restResponse.Content);

            string data = jObject.GetValue("data").ToString();
            string support = jObject.GetValue("support").ToString();

            Assert.IsNotNull(support);
            Assert.IsNotNull(data);
        }

        [Then(@"I validate the response for /api/users/23")]
        public void ThenIValidateTheResponseForApiUsers()
        {
            var jObject = JObject.Parse(restResponse.Content);
            Assert.IsEmpty(jObject);
        }


        [Then(@"I validate response status should be (.*)")]
        public void ThenIValidateResponseStatusShouldBe(int status)
        {
            int StatusCode = (int)restResponse.StatusCode;
            Assert.AreEqual(status, StatusCode, "Status code is not " + status);
        }
        [Then(@"I validate the response for updated /api/users/2")]
        public void ThenIValidateTheResponseForUpdatedApiUsers()
        {
            var jObject = JObject.Parse(restResponse.Content);
            string value = jObject.GetValue("Value").ToString();
            string updatedDate = jObject.GetValue("updatedAt").ToString();

            Assert.IsTrue(value.Contains(CreateUser.Value.name));
            Assert.IsTrue(value.Contains(CreateUser.Value.job));
            Assert.IsNotNull(updatedDate);
        }
        [Then(@"I validate the response for /api/users/2 after delete")]
        public void ThenIValidateTheResponseForApiUsersAfterDelete()
        {
            Assert.IsEmpty(restResponse.Content);
        }

    }
}
