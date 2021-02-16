using Library.App.Mobile;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.App.Mobile.Models
{
    public class Books
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("ISBN")]
        public string ISBN { get; set; }
        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("AuthorsID")]
        public int AuthorsID { get; set; }

        [ForeignKey("AuthorsID")]
        public virtual Authors Authories { get; set; }
        [JsonProperty("PublishDate")]
        [DataType(DataType.Date),Display(Name ="Publish Date"),DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }
        [JsonProperty("PublishName")]
        public string PublishName { get; set; }
    }
}
