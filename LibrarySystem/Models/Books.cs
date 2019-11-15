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
        public string Title { get; set; }

        [Required]
        [Display(Name = "AuthorsID")]
        public int AuthorsID { get; set; }

        [ForeignKey("AuthorsID")]
        public virtual Authors Authories { get; set; }

        public DateTime PublishDate { get; set; }
        public string PublishName { get; set; }
    }
}
