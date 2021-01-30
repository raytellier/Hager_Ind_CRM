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
        public string FirstName { get; set; }

        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        [Display(Name ="Job Title")]
        public string JobTitle { get; set; }
        
        [Display(Name ="Cell Phone")]
        public Int64 CellPhone { get; set; }

        [Display(Name ="Work Phone")]
        public Int64 WorkPhone { get; set; }

        public string Email { get; set; }

        public bool Active { get; set; }

        public int CompanyID { get; set; }
        public Company Company { get; set; }

        [Display(Name = "Catagories")]
        public ICollection<ContactCatagory> ContactCatagories { get; set; }
    }

    


}
