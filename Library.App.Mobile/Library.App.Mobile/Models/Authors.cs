using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.App.Mobile.Models
{
    public class Authors
    {
        [JsonProperty("AuthorsID")]
        public int AuthorsID { get; set; }
        [JsonProperty("AuthorsName")]
        public string AuthorsName { get; set; }
        [JsonProperty("Biography")]
        public string Biography { get; set; }
    }
}
