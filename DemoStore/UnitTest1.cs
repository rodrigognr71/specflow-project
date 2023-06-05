using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;

namespace DemoStore
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts/{postid}",Method.Get);
            request.AddUrlSegment("postid", 1);
            var response = client.ExecuteGet(request);
            var content = response.Content;
            var jsonObject = JObject.Parse(content);
            var result = jsonObject.SelectToken("author").ToString();
           // Assert.That(response.StatusCode,Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Equals("typicode") ,"Author is not correct");
        }

        [Test]
        public void Test2()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts/", Method.Post);
            request.AddJsonBody(new {tittle = "test2" });

            var response = client.Execute(request);
            var content = response.Content;
            var jsonObject = JObject.Parse(content);
            var result = jsonObject.SelectToken("tittle").ToString();
            // Assert.That(response.StatusCode,Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Equals("test2"), "Title is not correct");
        }
    }
}