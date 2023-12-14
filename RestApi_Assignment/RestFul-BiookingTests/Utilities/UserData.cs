﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi_Assignment3
{
    internal class UserData
    {
       
        [JsonProperty("firstname")]
        public string? Firstname{ get; set; }
        [JsonProperty("lastname")]
        public string? LastName { get; set; }
        [JsonProperty("totalprice")]
        public int TotalPrice { get; set; }
      
    }
}
