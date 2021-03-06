using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Hager_Ind_CRM.Data;
using Hager_Ind_CRM.ViewModels;

namespace Hager_Ind_CRM.Controllers
{
    public class RolePermsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        

        public RolePermsController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        // GET: Role
        //[Authorize(Policy = PolicyTypes.Users.Read)]
        public async Task<IActionResult> Index()
        {
            var roles = await (from r in _context.Roles
                               .OrderBy(r => r.Name)
                               select new RolePerms
                               {
                                   Id = r.Id,
                                   Name = r.Name
                               }).ToListAsync();
            foreach (var r in roles)
            {
                var role = await _roleManager.FindByIdAsync(r.Id);
                r.rolePerms = await _roleManager.GetClaimsAsync(role).ConfigureAwait(false);
            };
            return View(roles);
        }
    }
}
