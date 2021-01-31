using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hager_Ind_CRM.Models
{
    public class CompanyType
    {
        public int CompanyID { get; set; }
        public Company Company { get; set; }

        public int TypeID { get; set; }
        public CType Type { get; set; }

        //collection ?
    }
}
