using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class Authors
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int AuthorsID { get; set; }
        [Display(Name ="Author")]
        public string AuthorsName { get; set; }
        [Display(Name = "Biography")]
        public string Biography { get; set; }

        //[Required]
        //[Display(Name = "Sexs")]
        //public int SexID { get; set; }

        //[ForeignKey("SexID")]
        //public virtual Sexs Sexs { get; set; }
    }
}
