using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.App.Mobile.Models
{
    public class Religions
    {
        [JsonProperty("ReligionID")]
        [Key, DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ReligionID { get; set; }
        [JsonProperty("ReligionName")]
        public string ReligionName { get; set; }
    }
}
