using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hager_Ind_CRM.Models
{
    public class Employee
    {
        [Display(Name = "Employee")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public int ID { get; set; }

        [Display(Name = "First Name")]
        [StringLength(30, ErrorMessage = "First name cannot be more than 30 characters long.")]
        [Required(ErrorMessage = "First name section is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(30, ErrorMessage = "Last name cannot be more than 30 characters long.")]
        [Required(ErrorMessage = "Last name section is required.")]
        public string LastName { get; set; }


        [Display(Name = "Job Position")]
        [Required(ErrorMessage = "Job position section is required.")]
        public int? JobPositionID { get; set; }

        [Display(Name = "Job Position")]
        public JobPosition JobPosition { get; set; }

        [Display(Name = "Employment Type")]
        [Required(ErrorMessage = "Employment Type section is required.")]
        public int? EmploymentTypeID { get; set; }

        [Display(Name = "Employment Type")]
        public EmploymentType EmploymentType { get; set; }

        [Display(Name = "Address 1")]
        [StringLength(30, ErrorMessage = "Address cannot be more than 30 characters long.")]
        [Required(ErrorMessage = "Address 1 section is required.")]
        public string Address1 { get; set; }

        [Display(Name = "Address 2")]
        [StringLength(30, ErrorMessage = "Address cannot be more than 30 characters long.")]
        public string? Address2 { get; set; }

        [Required(ErrorMessage = "City section is required.")]
        public string City { get; set; }

        [Display(Name = "Province")]
        [Required(ErrorMessage = "Province section is required.")]
        public int BillingProvinceID { get; set; }
        public Province Province { get; set; }

        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "Postal Code section is required.")]
        //[RegularExpression("[ABCEGHJKLMNPRSTVXY]\\d[ABCEGHJ-NPRSTV-Z]\\d[ABCEGHJ-NPRSTV-Z]\\d", ErrorMessage = "Please enter a valid 6 character postal code (no spaces).")]
        [DataType(DataType.PostalCode)]
        public string BillingPostal { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country section is required.")]
        public int BillingCountryID { get; set; }
        public Country Country { get; set; }

        [Display(Name = "Cell Phone")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public Int64? CellPhone { get; set; }

        [Display(Name = "Home Phone")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public Int64? HomePhone { get; set; }

        [StringLength(50, ErrorMessage = "Email cannot be more than 50 characters long.")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Wage must follow the format '1000.00'.")]
        [Range(0, 9999999.99)]
        public decimal? Wage { get; set; }

        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Expense must follow the format '1000.00'.")]
        [Range(0, 99999999.99)]
        public decimal? Expense { get; set; }

        [Display(Name = "Date Joined")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DateJoined { get; set; }

        [Display(Name = "Inactive Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? InactiveDate { get; set; }

        [Display(Name = "Key Fob Number")]
        [RegularExpression("^\\d{9}$", ErrorMessage = "Please enter a valid 9-digit key fob number (no spaces).")]
        [DisplayFormat(DataFormatString = "{0:####:#####}", ApplyFormatInEditMode = false)]
        public Int64? KeyFobNumber { get; set; }

        public bool Active { get; set; }

        [Required(ErrorMessage = "Is User section is required.")]
        [Display(Name = "Is User")]
        public bool IsUser { get; set; }

        [Display(Name = "Permission Level")]
        [StringLength(30, ErrorMessage = "Permission level cannot be more than 30 characters long.")]
        [Required(ErrorMessage = "Permission level section is required.")]
        public string PermissionLevel { get; set; }

        [Display(Name = "Emergency Contact Name")]
        [StringLength(30, ErrorMessage = "Emergency contact name cannot be more than 30 characters long.")]
        public string? EmergencyContactName { get; set; }

        [Display(Name = "Emergency Contact Phone")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public Int64? EmergencyContactPhone { get; set; }
    }
}
