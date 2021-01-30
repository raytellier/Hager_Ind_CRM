using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hager_Ind_CRM.Data
{
    public class CustomClaimTypes
    {
        public const string Permission = "hager/permission";
    }
    public static class Companies
    {
        public const string Create = "companies.create";
        public const string Read = "companies.read";
        public const string Detail = "companies.detail";
        public const string Update = "companies.update";
        public const string Delete = "companies.delete";
    }
}
