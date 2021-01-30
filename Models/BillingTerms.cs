using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hager_Ind_CRM.Models
{
    public class BillingTerms
    {
        public BillingTerms()
        {
            this.Companies = new HashSet<Company>();
        }

        public int ID { get; set; }

        [Display(Name = "Billing Terms")]
        [Required(ErrorMessage = "Terms description is required.")]
        public string Terms { get; set; }

        [Display(Name = "Entry")]
        public int OrderID { get; set; }



        public ICollection<Company> Companies { get; set; }
    }
}
