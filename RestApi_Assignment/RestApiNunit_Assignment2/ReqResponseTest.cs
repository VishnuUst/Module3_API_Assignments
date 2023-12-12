using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiNunit_Assignment2
{
    [TestFixture]
    internal class ReqResApiTest
    {
        private RestClient client;
        private string? baseUrl = "https://jsonplaceholder.typicode.com/";

        [SetUp]
        public void SetUp()
        {
            client = new RestClient(baseUrl);
        }
        [Test]
        [Order(1)]
        public void GetSingleUser()
        {
            var req = new RestRequest("posts/2", Method.Get);
            var res = client.Execute(req);
            Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            var userdata = JsonConvert.DeserializeObject<UserData>(res.Content);
            Assert.NotNull(userdata);
            Assert.That(userdata.UserId, Is.EqualTo(2));
            Assert.IsNotEmpty(userdata.Title);
            Assert.IsNotEmpty(userdata.Body);
            


        }
        [Test]
        [Order(2)]
        public void CreateUser()
        {
            var req = new RestRequest("posts", Method.Post);
            req.AddHeader("Content-Type", "application/Json");
            req.AddJsonBody(new { title = "A1", body = "Lead Engineer" });
            var res = client.Execute(req);
            Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
            var user = JsonConvert.DeserializeObject<UserData>(res.Content);
            Assert.NotNull(user);

        }
        [Test]
        [Order(3)]
        public void UpdateUser()
        {
            var req = new RestRequest("posts/2", Method.Put);
            req.AddHeader("Content-Type", "Application/Json");
            req.AddJsonBody(new { title = "B1", body = "Hr Manager" });
            var res = client.Execute(req);
            Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            var user = JsonConvert.DeserializeObject<UserData>(res.Content);
            Assert.NotNull(user);

        }
        [Test]
        [Order(4)]
        public void DeleteUser()
        {
            var req = new RestRequest("posts/1", Method.Delete);
            var res = client.Execute(req);
            Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
        }
        [Test]
        [Order(5)]
        public void GetNotExistingUser()
        {
            var req = new RestRequest("posts/5670", Method.Get);
            var res = client.Execute(req);
            Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));

        }
        [Test]
        [Order(6)]
        public void GetAll()
        {
            var req = new RestRequest("posts", Method.Get);
            var res = client.Execute(req);
            Assert.That(res.StatusCode,Is.EqualTo(System.Net.HttpStatusCode.OK));
            List<UserData> user = JsonConvert.DeserializeObject<List<UserData>>(res.Content);
           
            Assert.NotNull(user);

        }
    }
}
