using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hager_Ind_CRM.Models
{
    public class CompanySubType
    {
        public int CompanyID { get; set; }
        public Company Company { get; set; }

        public int SubTypeID { get; set; }
        public SubType SubType { get; set; }
    }
}
