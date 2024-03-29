﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hager_Ind_CRM.Models
{
    public class Province
    {
        public Province()
        {
            this.BillingCompanies = new HashSet<Company>();
            this.ShippingCompanies = new HashSet<Company>();
            this.Employees = new HashSet<Employee>();
        }

        public int ID { get; set; }

        [Display(Name = "Province")]
        [Required(ErrorMessage = "Province Name is required.")]
        public string Name { get; set; }

        [Display(Name = "Entry")]
        public int OrderID { get; set; }

        [Display(Name = "Country")]
        public int CountryID { get; set; }
        public Country Country { get; set; }

        public ICollection<Company> BillingCompanies { get; set; }
        public ICollection<Company> ShippingCompanies { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
