using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.App.Mobile.Models
{
    public class Customers
    {
        [JsonProperty("CustomerId")]
        public int Id { get; set; }
        [JsonProperty("Username")]
        public string Username { get; set; }
        [JsonProperty("Password")]
        public string Password { get; set; }
        [JsonProperty("PasswordSalt")]
        public string PasswordSalt { get; set; }
        [JsonProperty("Firstname")]
        public string Firstname { get; set; }
        [JsonProperty("Lastname")]
        public string Lastname { get; set; }
        [JsonProperty("Email")]
        public string Email { get; set; }
        [JsonProperty("TS")]
        public DateTime TS { get; set; }
        [JsonProperty("Active")]
        public bool Active { get; set; }
        [JsonProperty("Blocked")]
        public bool Blocked { get; set; }
        [JsonProperty("Order")]
        public ICollection<LibraryTrancs> Order { get; set; }
    }
}
