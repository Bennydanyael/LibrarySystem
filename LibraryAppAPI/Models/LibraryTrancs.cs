using LibraryAppAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Library.App.API.Models
{
    public class LibraryTrancs : IEntity
    {
        [Key, DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Customers")]
        public int CustomerId { get; set; }
        [JsonIgnore, ForeignKey("Id")]
        public Customers Customer { get; set; }

        [Display(Name = "Books")]
        public int BooksID { get; set; }

        [ForeignKey("BooksID")]
        public virtual Books Books { get; set; }
        [DataType(DataType.Date),Display(Name ="Start Date"),DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BorrowedDate { get; set; }
        [DataType(DataType.Date),Display(Name ="End Date"),DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateBack { get; set; }
        private TimeSpan _count
        {
            get => DateBack.Date - BorrowedDate.Date;
            set => _count = (DateBack - BorrowedDate);
        }
        [Display(Name = "Total Day")]
        public int LengthBorrowed
        {
            get => Convert.ToInt32(_count.TotalDays);
            set => Convert.ToInt32(_count.TotalDays);
        }
        [Display(Name ="Descriptions")]
        public string Descriptions { get; set; }
    }
}
