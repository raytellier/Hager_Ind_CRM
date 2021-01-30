using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hager_Ind_CRM.Models
{
    public class Country
    {
        public Country()
        {
            this.BillingCompanies = new HashSet<Company>();
            this.ShippingCompanies = new HashSet<Company>();
            this.Employees = new HashSet<Employee>();
        }

        public int ID { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country Name is required.")]
        public string Name { get; set; }


        [Display(Name = "Entry")]
        public int OrderID { get; set; }


        public ICollection<Company> BillingCompanies { get; set; }
        public ICollection<Company> ShippingCompanies { get; set; }
        public ICollection<Employee> Employees{ get; set; }
    }
}
