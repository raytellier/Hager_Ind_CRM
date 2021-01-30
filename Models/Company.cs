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
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(30, ErrorMessage = "Name cannot be more than 30 characters long.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(50, ErrorMessage = "Location cannot be more than 50 characters long.")]
        public string Location { get; set; }

        [Display(Name = "Credit Check")]
        [Required(ErrorMessage = "Credit Check is required.")]
        public bool CredCheck { get; set; }

        [Display(Name = "Billing Terms")]
        [Required(ErrorMessage = "Billing Terms section is required.")]
        public int BillingTermsID { get; set; }

        [Display(Name = "Billing Terms")]
        public BillingTerms BillingTerms { get; set; }

        [Display(Name = "Currency")]
        [Required(ErrorMessage = "Currency is required.")]
        public int CurrencyID { get; set; }

        [Display(Name = "Currency")]
        public Currency Currency { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public Int64 Phone { get; set; }

        [Required(ErrorMessage = "Website is required.")]
        [StringLength(2000, ErrorMessage = "Website cannot be more than 2000 characters long.")]
        public string Website { get; set; }

        [Display(Name = "Billing Address #1")]
        [Required(ErrorMessage = "Billing Address #1 is required.")]
        [StringLength(50, ErrorMessage = "Billing Address #1 cannot be more than 50 characters long.")]
        public string BillingAddress1 { get; set; }

        [Display(Name = "Billing Address #2")]
        [StringLength(50, ErrorMessage = "Billing Address #2 cannot be more than 50 characters long.")]
        public string BillingAddress2 { get; set; }

        [Display(Name = "Billing Province")]
        [Required(ErrorMessage = "Billing Province is required.")]
        public int BillingProvinceID { get; set; }
        [Display(Name = "Billing Province")]
        public Province BillingProvince { get; set; }

        [Display(Name = "Billing Postal Code")]
        [Required(ErrorMessage = "Billing Postal code is required.")]
        //[RegularExpression("[ABCEGHJKLMNPRSTVXY]\\d[ABCEGHJ-NPRSTV-Z]\\d[ABCEGHJ-NPRSTV-Z]\\d", ErrorMessage = "Please enter a valid 6 character postal code (no spaces).")]
        [DataType(DataType.PostalCode)]
        public string BillingPostalCode { get; set; }

        [Display(Name = "Billing Country")]
        [Required(ErrorMessage = "Billing Country is required.")]
        public int BillingCountryID { get; set; }
        [Display(Name = "Billing Country")]
        public Country BillingCountry { get; set; }

        [Display(Name = "Shipping Address #1")]
        [Required(ErrorMessage = "Shipping Address #1 is required.")]
        [StringLength(50, ErrorMessage = "Shipping Address #1 cannot be more than 50 characters long.")]
        public string ShippingAddress1 { get; set; }

        [Display(Name = "Shipping Address #2")]
        [StringLength(50, ErrorMessage = "Shipping Address #2 cannot be more than 50 characters long.")]
        public string ShippingAddress2 { get; set; }

        [Display(Name = "Shipping Province")]
        [Required(ErrorMessage = "Shipping Province is required.")]
        public int ShippingProvinceID { get; set; }
        [Display(Name = "Billing Province")]
        public Province ShippingProvince { get; set; }

        [Display(Name = "Shipping Postal Code")]
        [Required(ErrorMessage = "Shipping Postal code is required.")]
        //[RegularExpression("[ABCEGHJKLMNPRSTVXY]\\d[ABCEGHJ-NPRSTV-Z]\\d[ABCEGHJ-NPRSTV-Z]\\d", ErrorMessage = "Please enter a valid 6 character postal code (no spaces).")]
        [DataType(DataType.PostalCode)]
        public string ShippingPostalCode { get; set; }

        [Display(Name = "Shipping Country")]
        [Required(ErrorMessage = "Shipping Country is required.")]
        public int ShippingCountryID { get; set; }
        [Display(Name = "Billing Country")]
        public Country ShippingCountry { get; set; }

        [Required(ErrorMessage = "Active status is required.")]
        public bool Active { get; set; }

        [StringLength(511, ErrorMessage = "Notes cannot be more than 511 characters long.")]
        public string Notes { get; set; }

        public ICollection<Contact> Contacts { get; set; }
        public ICollection<CompanyType> CompanyTypes { get; set; }
    }
}
