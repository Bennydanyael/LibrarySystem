using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.App.Mobile.Models
{
    public class Sexs
    {
        [JsonProperty("SexID")]
        [Key, DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int SexID { get; set; }
        [JsonProperty("SexName")]
        public string SexName { get; set; }
    }
}
