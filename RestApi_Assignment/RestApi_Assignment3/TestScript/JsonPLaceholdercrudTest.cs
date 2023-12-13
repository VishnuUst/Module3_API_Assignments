using AventStack.ExtentReports.Model;
using Newtonsoft.Json;
using RestApi_Assignment3.Utilities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Log = Serilog.Log;

namespace RestApi_Assignment3.TestScript
{
    public class JsonPLaceholdercrudTest:CoreCodes
    {
        [Test]
        [Order(1)]
        [TestCase("2")]
        public void GetSingleUser(string id)
        {
            test = extent.CreateTest("Single User Test");
            var req = new RestRequest("posts/"+id+"", Method.Get);
            var res = client.Execute(req);
            try
            {
                Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"Api Error:{res.Content}");
                var userdata = JsonConvert.DeserializeObject<UserData>(res.Content);
                
                Assert.NotNull(userdata);
                Log.Information("Get UserData Test Passed");
                Assert.That(userdata.UserId, Is.EqualTo(2));
                Log.Information("Fetched User Id Is Correct");
                Assert.IsNotEmpty(userdata.Title);
                Log.Information("User Title Is Correct");
                Assert.IsNotEmpty(userdata.Body);
                Log.Information("User Body Is Correct");
                
                test.Pass("GetSingle User Test Passed");
            }
            catch (AssertionException)
            {
                
                test.Fail("GetSingle User Test Fail");
            }



        }
        [Test]
        [Order(2)]
        public void CreateUser()
        {
            test=extent.CreateTest("Create user test");
            var req = new RestRequest("posts", Method.Post);
            req.AddHeader("Content-Type", "application/Json");
            req.AddJsonBody(new { title = "A1", body = "Lead Engineer" });
            var res = client.Execute(req);
            try
            {
                Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information($"Api Error: {res.Content}");
                var user = JsonConvert.DeserializeObject<UserData>(res.Content);
                Assert.NotNull(user);
                Log.Information("Create User Test Passed");
               
                test.Pass("Create user test passed");
            }
            catch (AssertionException)
            {
                
                test.Pass("Create user test Failed");
            }

        }
        [Test]
        [Order(3)]
        [TestCase("2")]
        public void UpdateUser(string id)
        {
            test = extent.CreateTest("Update user test");
            var req = new RestRequest("posts/"+id+"", Method.Put);
            req.AddHeader("Content-Type", "Application/Json");
            req.AddJsonBody(new { title = "B1", body = "Hr Manager" });
            var res = client.Execute(req);
            try
            {


                Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"Api Error:{res.Content}");
                var user = JsonConvert.DeserializeObject<UserData>(res.Content);
                Assert.NotNull(user);
                Log.Information("Update User test passed");
                
                test.Pass("Update User Test Passed");
            }
            catch (AssertionException)
            {
               
                test.Pass("Update User Test Failed");
            }

        }
        [Test]
        [Order(4)]
        
        public void DeleteUser()
        {
            test =extent.CreateTest("Delete User Test");
            var req = new RestRequest("posts/1", Method.Delete);
            var res = client.Execute(req);
            try
            {
                Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"Api Erro:{res.Content}");
                
                test.Pass("Delete User Test Pass");
            }
            catch(AssertionException)
            {
                
                test.Pass("Delete User Test Fail");
            }
        }
        [Test]
        [Order(5)]
       
        public void GetNotExistingUser()
        {
            test =extent.CreateTest("GetNotExisting Test");
            var req = new RestRequest("posts/5678", Method.Get);
            var res = client.Execute(req);
            try
            {

                Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
                Log.Information($"Api Error:{res.Content}");
               
                test.Pass("GetNotExisting test pass");
            }
            catch( AssertionException)
            {
                
                test.Pass("GetNotExisting test Fail");
            }

        }
        [Test]
        [Order(6)]
        public void GetAll()
        {
            test =extent.CreateTest("Get All Test");
            var req = new RestRequest("posts", Method.Get);
            var res = client.Execute(req);
            try
            {
                Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"Api Error:{res.Content}");
                List<UserData> user = JsonConvert.DeserializeObject<List<UserData>>(res.Content);

                Assert.NotNull(user);
                Log.Information("User Data Displayed");
                
                test.Pass("Get all test pass");
            }
            catch ( AssertionException)
            {
               
                test.Pass("Get all test Failed");
            }

        }
    }
}
