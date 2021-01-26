using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class LibrarySystemRun
    {
        [Key,DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int LibraryID { get; set; }

        [Required,Display(Name = "Name")]
        public int PersonID { get; set; }

        [ForeignKey("PersonID")]
        public virtual Persons Persons { get; set; }

        [Display(Name = "Books")]
        public int BooksID { get; set; }

        [ForeignKey("BooksID")]
        public virtual Books Bookis { get; set; }
        [DataType(DataType.Date),Display(Name ="Start Date"),DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BorrowedDate { get; set; }
        [DataType(DataType.Date),Display(Name ="End Date"),DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateBack { get; set; }
        [Display(Name ="Total Day")]
        public int LengthBorrowed { get; set; }
        [Display(Name ="Descriptions")]
        public string Descriptions { get; set; }
    }
}
