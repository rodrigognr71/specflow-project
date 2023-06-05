using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace DemoStore.stepsDefinitions
{
    [Binding]
    public class GetPostsStepDefinitions
    {
        RestClient client = new RestClient("http://localhost:3000/");
        RestRequest request = new RestRequest("posts/{postid}", Method.Get);
        RestResponse response = new RestResponse();

        [Given(@"I have an id with value (.*)")]
        public void GivenIHaveAnIdWithValue(int id)
        {
            request.AddUrlSegment("postid", id);
        }

        [When(@"I send a get request")]
        public void WhenISendAGetRequest()
        {
             response = client.ExecuteGet(request);
        }

        [Then(@"I expected a valid code response")]
        public void ThenIExpectedAValidCodeResponse()
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
