using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hager_Ind_CRM.Data
{
    public class PolicyTypes
    {
        public static class Companies
        {
            public const string Create = "companies.create.policy";
            public const string Read = "companies.read.policy";
            public const string Detail = "companies.detail.policy";
            public const string Update = "companies.update.policy";
            public const string Delete = "companies.delete.policy";
        }
    }
}
