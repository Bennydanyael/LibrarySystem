using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.App.Mobile.Models
{
    public class Maritals
    {
        [JsonProperty("MaritalID")]
        public int MaritalID { get; set; }
        [JsonProperty("MaritalsName")]
        public string MaritalsName { get; set; }
    }
}
