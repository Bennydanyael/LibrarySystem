using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class Books
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int BooksID { get; set; }

        [Display(Name ="ISBN")]
        public int ISBN { get; set; }

        [Display(Name ="Title")]
        public string Title { get; set; }

        [Required,Display(Name = "Authors")]
        public int AuthorsID { get; set; }

        [ForeignKey("AuthorsID")]
        public virtual Authors Authories { get; set; }
        [DataType(DataType.Date),Display(Name ="Publish Date"),DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }
        [Display(Name ="Production")]
        public string PublishName { get; set; }
    }
}
