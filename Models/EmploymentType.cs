using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hager_Ind_CRM.Models
{
    public class EmploymentType
    {
        public EmploymentType()
        {
            this.Employees= new HashSet<Employee>();
        }
        public int ID { get; set; }

        [Display(Name = "Employment Type")]
        public string Type { get; set; }

        [Display(Name = "Entry")]
        public int OrderID { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
