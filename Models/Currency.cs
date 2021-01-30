using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hager_Ind_CRM.Models
{
    public class Currency
    {
        public Currency()
        {
            this.Companies = new HashSet<Company>();
        }

        public int ID { get; set; }

        [Display(Name = "Currency")]
        [Required(ErrorMessage = "Type is required.")]
        [RegularExpression("[A-Z]{3}", ErrorMessage = "Type cannot be more than 3 letters long.")]
        public string Name { get; set; }

        [Display(Name = "Entry")]
        public int OrderID { get; set; }


        public ICollection<Company> Companies { get; set; }
    }
}
