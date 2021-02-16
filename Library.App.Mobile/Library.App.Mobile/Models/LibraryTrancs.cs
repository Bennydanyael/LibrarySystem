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
    public class LibraryTrancs
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("CustomerId")]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        [JsonIgnore, ForeignKey("CustomerId")]
        public Customers Customers { get; set; }
        [JsonProperty("Books")]
        [Display(Name = "Books")]
        public int BooksID { get; set; }
        [ForeignKey("BooksID")]
        public virtual Books Books { get; set; }
        [JsonProperty("BorrowedDate")]
        [DataType(DataType.Date), Display(Name = "Start Date"), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BorrowedDate { get; set; }
        [JsonProperty("DateBack")]
        [DataType(DataType.Date), Display(Name = "End Date"), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateBack { get; set; }
        private TimeSpan _count 
        {
            get => DateBack - BorrowedDate;
            set => _count = (DateBack - BorrowedDate);
        }
        [JsonProperty("LengthBorrowed")]
        [Display(Name ="Total Day")]
        public int LengthBorrowed {
            get => Convert.ToInt32(_count);
            set => Convert.ToInt32(_count);
        }

        [JsonProperty("Descriptions")]
        [Display(Name ="Descriptions")]
        public string Descriptions { get; set; }
    }
}
