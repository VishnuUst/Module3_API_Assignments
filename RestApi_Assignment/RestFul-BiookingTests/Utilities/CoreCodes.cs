using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace RestFul_BiookingTests.Utilities
{
    public class CoreCodes
    { 
        protected RestClient client;
        protected ExtentReports extent;
        protected ExtentTest test;
        ExtentSparkReporter sparkReporter;
        protected string? baseUrl = "https://restful-booker.herokuapp.com/";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            string currdir = Directory.GetParent(@"../../../").FullName;
            extent = new ExtentReports();
            sparkReporter = new ExtentSparkReporter(currdir + "/Reports/extent-report-" +
            DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".html");
            extent.AttachReporter(sparkReporter);
            string? logfilePath = currdir + "/Logs/log_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            Log.Logger = new LoggerConfiguration().WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day).CreateLogger();

         }

        [SetUp]
         public void SetUp()
         {
            client = new RestClient(baseUrl);
         }
        [OneTimeTearDown]
        public void TearDown()
        {
            extent.Flush();
        }
    

    }
}
