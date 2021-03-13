using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hager_Ind_CRM.ViewModels
{
    public class RolePermsVM
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Permissions")]
        public IList<System.Security.Claims.Claim> rolePerms { get; set; }
    }
}
