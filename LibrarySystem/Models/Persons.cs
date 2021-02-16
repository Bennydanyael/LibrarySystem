using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class Persons
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int PersonID { get; set; }
        [Required]
        public string PersonName { get; set; }

        [Required]
        [Display(Name = "Sexs")]
        public int SexID { get; set; }

        [ForeignKey("SexID")]
        public virtual Sexs Sexs { get; set; }

        [Required]
        [Display(Name = "Marital Status")]
        public int MaritalID { get; set; }

        [ForeignKey("MaritalID")]
        public virtual Maritals Maritals { get; set; }

        [Required]
        [Display(Name = "Religion")]
        public int ReligionID { get; set; }

        [ForeignKey("ReligionID")]
        public virtual Religions Religions { get; set; }

        public int PhoneNumber { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
