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
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int LibraryID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public int PersonID { get; set; }

        [ForeignKey("PersonID")]
        public virtual Persons Persons { get; set; }

        [Display(Name = "BooksID")]
        public int BooksID { get; set; }

        [ForeignKey("BooksID")]
        public virtual Books Bookis { get; set; }

        public DateTime BorrowedDate { get; set; }
        public DateTime DateBack { get; set; }
        public int LengthBorrowed { get; set; }
        public string Descriptions { get; set; }
    }
}
