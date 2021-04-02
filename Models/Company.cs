using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hager_Ind_CRM.Models
{
    public class Company
    {
        public Company()
        {
            this.Contacts = new HashSet<Contact>();
            this.CompanyTypes = new HashSet<CompanyType>();
            this.CompanySubTypes = new HashSet<CompanySubType>();
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(30, ErrorMessage = "Name cannot be more than 30 characters long.")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "Location cannot be more than 50 characters long.")]
        public string? Location { get; set; }

        [Display(Name = "Credit Check")]
        public bool CredCheck { get; set; }

        [Display(Name = "Billing Terms")]
        public int? BillingTermsID { get; set; }

        [Display(Name = "Billing Terms")]
        public BillingTerms BillingTerms { get; set; }

        [Display(Name = "Currency")]
        public int? CurrencyID { get; set; }

        [Display(Name = "Currency")]
        public Currency Currency { get; set; }

        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public Int64? Phone { get; set; }

        [StringLength(2000, ErrorMessage = "Website cannot be more than 2000 characters long.")]
        public string Website { get; set; }

        [Display(Name = "Billing Address #1")]
        [StringLength(50, ErrorMessage = "Billing Address #1 cannot be more than 50 characters long.")]
        public string BillingAddress1 { get; set; }

        [Display(Name = "Billing Address #2")]
        [StringLength(50, ErrorMessage = "Billing Address #2 cannot be more than 50 characters long.")]
        public string BillingAddress2 { get; set; }

        [Display(Name = "Billing Province")]
        public int? BillingProvinceID { get; set; }
        [Display(Name = "Billing Province")]
        public Province BillingProvince { get; set; }

        [Display(Name = "Billing Postal Code")]
        //[RegularExpression("[ABCEGHJKLMNPRSTVXY]\\d[ABCEGHJ-NPRSTV-Z]\\d[ABCEGHJ-NPRSTV-Z]\\d", ErrorMessage = "Please enter a valid 6 character postal code (no spaces).")]
        [DataType(DataType.PostalCode)]
        public string BillingPostalCode { get; set; }

        [Display(Name = "Billing Country")]
        public int? BillingCountryID { get; set; }
        [Display(Name = "Billing Country")]
        public Country BillingCountry { get; set; }

        [Display(Name = "Shipping Address #1")]
        [StringLength(50, ErrorMessage = "Shipping Address #1 cannot be more than 50 characters long.")]
        public string ShippingAddress1 { get; set; }

        [Display(Name = "Shipping Address #2")]
        [StringLength(50, ErrorMessage = "Shipping Address #2 cannot be more than 50 characters long.")]
        public string ShippingAddress2 { get; set; }

        [Display(Name = "Shipping Province")]
        public int? ShippingProvinceID { get; set; }
        [Display(Name = "Billing Province")]
        public Province ShippingProvince { get; set; }

        [Display(Name = "Shipping Postal Code")]
        //[RegularExpression("[ABCEGHJKLMNPRSTVXY]\\d[ABCEGHJ-NPRSTV-Z]\\d[ABCEGHJ-NPRSTV-Z]\\d", ErrorMessage = "Please enter a valid 6 character postal code (no spaces).")]
        [DataType(DataType.PostalCode)]
        public string ShippingPostalCode { get; set; }

        [Display(Name = "Shipping Country")]
        public int? ShippingCountryID { get; set; }
        [Display(Name = "Billing Country")]
        public Country ShippingCountry { get; set; }

        public bool Active { get; set; }

        [StringLength(511, ErrorMessage = "Notes cannot be more than 511 characters long.")]
        public string Notes { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        [Display(Name = "Company Types")]
        public ICollection<CompanyType> CompanyTypes { get; set; }
        public ICollection<CompanySubType> CompanySubTypes { get; set; }
    }
}
