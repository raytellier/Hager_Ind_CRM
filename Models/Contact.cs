using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hager_Ind_CRM.Models
{
    public class Contact
    {
        public Contact()
        {
            ContactCatagories = new HashSet<ContactCatagory>();
        }

        [Display(Name = "Contact")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public int ID { get; set; }

        [Display(Name ="First Name")]
        [StringLength(30, ErrorMessage = "Name cannot be more than 30 characters long.")]
        [Required(ErrorMessage = "First name section is required.")]
        public string FirstName { get; set; }

        [Display(Name ="Last Name")]
        [StringLength(30, ErrorMessage = "Name cannot be more than 30 characters long.")]
        [Required(ErrorMessage = "Last name section is required.")]
        public string LastName { get; set; }

        [Display(Name ="Job Title")]
        [StringLength(30, ErrorMessage = "Name cannot be more than 30 characters long.")]
        public string JobTitle { get; set; }
        
        [Display(Name ="Cell Phone")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit cell phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public Int64? CellPhone { get; set; }

        [Display(Name ="Work Phone")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public Int64? WorkPhone { get; set; }

        [StringLength(50, ErrorMessage = "Email cannot be more than 50 characters long.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool Active { get; set; }

        public int? CompanyID { get; set; }
        public Company Company { get; set; }

        [Display(Name = "Catagories")]
        public ICollection<ContactCatagory> ContactCatagories { get; set; }
    }

    


}
