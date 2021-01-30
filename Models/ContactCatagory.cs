using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hager_Ind_CRM.Models
{
    public class ContactCatagory
    {


        public int CatagoryID { get; set; }

        public Catagory Catagory { get; set; }

        public int ContactID { get; set; }

        public Contact Contact { get; set; }
    }
}
