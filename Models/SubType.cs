using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hager_Ind_CRM.Models
{
    public class SubType
    {

        public int ID { get; set; }

        [Display(Name = "Sub Type")]
        [Required(ErrorMessage = "You cannot leave the name of the Sub Type blank.")]
        [StringLength(50, ErrorMessage = "Sub Type name cannot be more than 50 characters long.")]
        public string Name { get; set; }

        [Display(Name = "Entry")]
        public int OrderID { get; set; }

        public int TypeID { get; set; }
        public Type Type { get; set; }
    }
}
