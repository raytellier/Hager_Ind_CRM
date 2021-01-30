using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hager_Ind_CRM.Models
{
    public class Type
    {
        public Type()
        {
            this.CompanyTypes = new HashSet<CompanyType>();

            this.SubTypes = new HashSet<SubType>();
        }

        public int ID { get; set; }

        [Display(Name = "Type")]
        [Required(ErrorMessage = "You cannot leave the name of the Type blank.")]
        [StringLength(50, ErrorMessage = "Type name cannot be more than 50 characters long.")]
        public string Name { get; set; }


        [Display(Name = "Entry")]
        public int OrderID { get; set; }

        public ICollection<CompanyType> CompanyTypes { get; set; }

        public ICollection<SubType> SubTypes { get; set; }
    }
}
