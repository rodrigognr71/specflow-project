using Gherkin;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace DemoStore.stepsDefinitions
{
    [Binding]
    public class GatlingStepsDefinitions
    {
        RestClient client = new RestClient("http://demostore.gatling.io/api/");
        RestRequest request = new RestRequest("product/{productID}", Method.Get);
        RestRequest requestByCategoryID = new RestRequest("product?category={categoryID}", Method.Get);
        RestResponse response = new RestResponse();
        private RestClient restClient;
        private string token = null;

        [Given(@"I use the Gatling service client")]
        public void GivenIUseTheGatlingServiceClient()
        {
            restClient = new RestClient("http://demostore.gatling.io/api/");
            var request = new RestRequest("authenticate", Method.Post);

            var requestBody = new
            {
                username = "admin",
                password = "admin"
            };
            
            request.AddJsonBody(requestBody);
            response = restClient.Execute(request);

            
            var jsonResponse = response.Content;
            var responseObject = JObject.Parse(jsonResponse);
            token = responseObject["token"].ToString();

            Assert.IsNotNull(token,"The token was not generated");  
        }

        [Given(@"I have product id with value (.*)")]
        [When(@"I have product id with value (.*)")]
        public void GivenOrWhenIHaveProductIdWithValue(int productID)
        {
            request.AddUrlSegment("productID", productID);
        }

        [Given(@"I have category id with value (.*)")]
        public void GivenIHaveCategoryIdWithValue(int categoryID)
        {   
            requestByCategoryID.AddUrlSegment("categoryID", categoryID);
        }

        [When(@"I send a GET request to Gatling")]
        public void WhenISendAGETRequestToGatling()
        {
            response = client.ExecuteGet(request);
        }

        [When(@"I send a GET by category ID request to Gatling")]
        public void WhenISendAGETByCategoryIDRequestToGatling()
        {
            response = client.ExecuteGet(requestByCategoryID);
        }

        [When(@"I send a PUT request to product/\{ID} with the following json body")]
        public void WhenISendARequestToProductIDWithTheFollowingJsonBody(string body)
        {
            request.AddHeader("Authorization","Bearer " + token);
            request.AddUrlSegment("productID", request.ToString());
            request.AddJsonBody(body);
            response = client.ExecutePut(request);
        }

        [When(@"I send a POST request to products with the following json body")]
        public void WhenISendAPOSTRequestToProductsWithTheFollowingJsonBody(string body)
        {
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddJsonBody(body);
            response = client.ExecutePut(request);
        }

        [Then(@"I validate that the response status is ""([^""]*)""")]
        public void ThenIValidateThatTheResponseStatusIs(int statusCode)
        {
            Assert.That((int)response.StatusCode, Is.EqualTo(statusCode));
        }

        [Then(@"I validate that the response body contais the following values")]
        public void ThenIValidateThatTheResponseBodyContaisTheFollowingValues_(Table table)
        {
            var jsonObject = JObject.Parse(response.Content);
            var dictionary = new Dictionary<string, string>();

            foreach (var row in table.Rows)
            {
                dictionary.Add(row[0], row[1]);
            }
            foreach (var entry in dictionary)
            {
                Assert.That(jsonObject.SelectToken(entry.Key).ToString(), Is.EqualTo(entry.Value));
            }
        }
    }
}
