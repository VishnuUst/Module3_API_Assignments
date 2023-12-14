using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestFul_BiookingTests.Utilities
{
    internal class Cookies
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
