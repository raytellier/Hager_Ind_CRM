using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hager_Ind_CRM.Models
{
    public class Catagory
    {

        public Catagory()
        {
            ContactCatagories = new HashSet<ContactCatagory>();
        }


        
        public int ID { get; set; }

        [Display(Name = "Contact Catagory")]
        public string Name { get; set; }

        [Display(Name = "Entry")]
        public int OrderID { get; set; }

        public ICollection<ContactCatagory> ContactCatagories { get; set; }
    }
}
