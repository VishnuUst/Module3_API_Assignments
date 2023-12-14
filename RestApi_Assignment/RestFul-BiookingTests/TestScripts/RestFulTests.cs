using Newtonsoft.Json;
using RestFul_BiookingTests;
using RestFul_BiookingTests.Utilities;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFul_BiookingTests.TestScripts
{
    [TestFixture]
    internal class RestFulTests : CoreCodes
    {
        [Test]
        [Order(1)]
        [TestCase("2")]
        public void GetSingleUser(string id)
        {
            test = extent.CreateTest("Single User Test");
            var req = new RestRequest("booking/" + id + "", Method.Get);
            req.AddHeader("Accept", "application/json");
            var res = client.Execute(req);
            try
            {
                Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"Api Error:{res.Content}");
                var userdata = JsonConvert.DeserializeObject<UserData>(res.Content);

                Assert.NotNull(userdata);
                Log.Information("Get UserData Test Passed");
                Assert.IsNotEmpty(userdata.Firstname);
                Log.Information("Fetched User Id Is Correct");
                Assert.IsNotEmpty(userdata.LastName);
                Log.Information("User Title Is Correct");
                //Assert.IsNotEmpty(userdata.TotalPrice);
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
            test = extent.CreateTest("Create user test");
            var req = new RestRequest("booking", Method.Post);
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Accept", "application/json");
            req.AddJsonBody(new { 
                firstname = "Akash", 
                lastname = "L" ,
                totalprice=111,
                depositpaid=true,
                bookingdates = new
                {
                    checkin="2023-03-01",
                    checkout="2023-03-15"
                },
                additionalneeds="Extra pillows"

            });
            var res = client.Execute(req);
            try
            {
                Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"Api Error: {res.Content}");
                var user = JsonConvert.DeserializeObject<UserData>(res.Content);
                Assert.NotNull(user);
                Log.Information("Create User Test Passed");

                test.Pass("Create user test passed");
            }
            catch (AssertionException)
            {

                test.Fail("Create user test Failed");
            }

        }
        [Test]
        [Order(1)]
        
        public void GetAuth()
        {
            test = extent.CreateTest("Auth User Test");
            var req = new RestRequest("/auth", Method.Post);
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(new { username = "admin", password = "password123" });

            var res = client.Execute(req);
            try
            {
                Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"Api Error:{res.Content}");
                var userdata = JsonConvert.DeserializeObject<Cookies>(res.Content);

                Assert.NotNull(userdata);
                Log.Information("Get UserData Test Passed");
                Console.WriteLine(userdata.Token);
                Assert.IsNotEmpty(userdata.Token);
                Log.Information("User Title Is Correct");
               
                test.Pass("GetSingle User Test Passed");
            }
            catch (AssertionException)
            {

                test.Fail("GetSingle User Test Fail");
            }



        }
        [Test]
        [Order(4)]

        public void DeleteUser()
        {
            test = extent.CreateTest("Delete User Test");
            var req = new RestRequest("/auth", Method.Post);
            req.AddHeader("Content-Type", "Application/Json");

            req.AddJsonBody(new { username = "admin", password = "password123" });
            var res = client.Execute(req);

            try
            {
                var user11 = JsonConvert.DeserializeObject<Cookies>(res.Content);
                var req11 = new RestRequest("booking/7", Method.Delete);
                req11.AddHeader("Content-Type", "Application/Json");
                req11.AddHeader("Cookie", "token=" + user11.Token);
                var res11 = client.Execute(req11);

                Assert.That(res11.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information($"Api Erro:{res11.Content}");

                test.Pass("Delete User Test Pass");
            }
            catch (AssertionException)
            {

                test.Fail("Delete User Test Fail");
            }
        }
        [Test]
        [Order(3)]
        [TestCase("2")]
        public void UpdateUser(string id)
        {
            test = extent.CreateTest("Update user test");
            var req = new RestRequest("auth", Method.Post);
            req.AddHeader("Content-Type", "Application/Json");
           
            req.AddJsonBody(new { username = "admin", password = "password123" });
            var res = client.Execute(req);
            try
            {


                Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"Api Error:{res.Content}");
                var user = JsonConvert.DeserializeObject<Cookies>(res.Content);
                Assert.NotNull(user);
                Log.Information("Update User test passed");

                test.Pass("Update User Test Passed");
                var reqput = new RestRequest("booking/12", Method.Put);
                reqput.AddHeader("Content-Type", "Application/Json");
                reqput.AddHeader("Cookie", "token=" + user.Token);
                reqput.AddJsonBody(new {
                    firstname = "amal",
                    lastname = "k",
                    totalprice = 1231,
                    depositpaid = true,
                    bookingdates = new
                    {
                        checkin = "2023-03-11",
                        checkout = "2023-03-18"
                    },
                    additionalneeds = "Extra pillow"

               
                });

            }
            catch (AssertionException)
            {

                test.Fail("Update User Test Failed");
            }

        }


    }
}
