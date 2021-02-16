using Library.App.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.App.API.Models
{
    public class Customers
    {
        [Key, DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public DateTime TS { get; set; }
        public bool Active { get; set; }
        public bool Blocked { get; set; }
        public ICollection<LibraryTrancs> Order { get; set; }
    }
}
