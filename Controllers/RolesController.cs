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
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        

        public RolesController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        // GET: Role
        [Authorize(Policy = PolicyTypes.Roles.Read)]
        public async Task<IActionResult> Index()
        {
            var roles = await (from r in _context.Roles
                               .OrderBy(r => r.Name)
                               select new RolePermsVM
                               {
                                   Id = r.Id,
                                   Name = r.Name
                               }).ToListAsync();
            foreach (var r in roles)
            {
                var role = await _roleManager.FindByIdAsync(r.Id);
                r.rolePerms = await _roleManager.GetClaimsAsync(role);
            };
            return View(roles);
        }

        // GET: RolePerms/Create
        [Authorize(Policy = PolicyTypes.Roles.Create)]
        public async Task<IActionResult> Create()
        {
            await PopulatePermData();
            return View();
        }

        // POST: RolePerms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Policy = PolicyTypes.Roles.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RolePermsVM role, string[] selectedPerms)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = role.Name
                };

                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    await SetRolePerms(selectedPerms, identityRole.Name);
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            await PopulatePermData();
            return View(role);
        }

        // GET: RolePerms/Edit/5
        [Authorize(Policy = PolicyTypes.Roles.Update)]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var _role = await _roleManager.FindByIdAsync(id);//Role
            if (_role == null)
            {
                return NotFound();
            }
            RolePermsVM role = new RolePermsVM
            {
                Id = _role.Id,
                Name = _role.Name,
                rolePerms = await _roleManager.GetClaimsAsync(_role)
            };
            await PopulateAssignedPermData(_role);
            return View(role);
        }

        // POST: RolePerms/Edit/5
        [Authorize(Policy = PolicyTypes.Roles.Update)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Id, string Email, string[] selectedPerms)
        {
            var _role = await _roleManager.FindByIdAsync(Id);//IdentityRole
            RolePermsVM role = new RolePermsVM
            {
                Id = _role.Id,
                Name = _role.Name,
                rolePerms = await _roleManager.GetClaimsAsync(_role)
            };
            try
            {
                await UpdateRolePerms(selectedPerms, _role.Name);
                await _roleManager.UpdateAsync(_role);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty,
                                "Unable to save changes.");
            }
            await PopulateAssignedPermData(_role);
            return View(role);
        }

        // GET: RolePerms/Delete/5
        [Authorize(Policy = PolicyTypes.Roles.Delete)]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var _role = await _roleManager.FindByIdAsync(id);//IdentityRole
            if (_role == null)
            {
                return NotFound();
            }
            RolePermsVM role = new RolePermsVM
            {
                Id = _role.Id,
                Name = _role.Name,
                rolePerms = await _roleManager.GetClaimsAsync(_role)
            };
            return View(role);
        }

        // POST: RolePerms/Delete/5
        [Authorize(Policy = PolicyTypes.Roles.Delete)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var _role = await _roleManager.FindByIdAsync(id);//IdentityRole
            await _roleManager.DeleteAsync(_role);
            return RedirectToAction(nameof(Index));
        }

        // Populate perm data for creation
        private async Task PopulatePermData()
        {//Prepare checkboxes for all Roles
            var adminRole = await _roleManager.FindByNameAsync("Admin");
            var allPerms = await _roleManager.GetClaimsAsync(adminRole);
            var viewModel = new List<PermVM>();
            foreach (var p in allPerms)
            {
                viewModel.Add(new PermVM
                {
                    PermValue = p.Value,
                    Assigned = false
                });
            }
            ViewBag.Perms = viewModel;
        }

        // Update populate perm data
        private async Task PopulateAssignedPermData(IdentityRole role)
        {//Prepare checkboxes for all Perms
            var adminRole = await _roleManager.FindByNameAsync("Admin");
            var allPerms = await _roleManager.GetClaimsAsync(adminRole);
            var currentPerms = await _roleManager.GetClaimsAsync(role);
            List<String> currentPermValues = new List<string>();
            foreach (var p in currentPerms)
            {
                currentPermValues.Add(p.Value);
            }
            var viewModel = new List<PermVM>();
            foreach (var p in allPerms)
            {
                viewModel.Add(new PermVM
                {
                    PermValue = p.Value,
                    Assigned = currentPermValues.Contains(p.Value),
                });
            }
            ViewBag.Perms = viewModel;
        }

        // Set Permission Creation
        private async Task SetRolePerms(string[] selectedPerms, string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            foreach(var value in selectedPerms)
            {
                await _roleManager.AddClaimAsync(role, new System.Security.Claims.Claim(CustomClaimTypes.Permission, value));
            }
        }


        // Update Permission Creation
        private async Task UpdateRolePerms(string[] selectedPerms, string roleName)
        {
            var adminRole = await _roleManager.FindByNameAsync("Admin");
            var allPerms = await _roleManager.GetClaimsAsync(adminRole);

            var currentRole = await _roleManager.FindByNameAsync(roleName);
            var currentPerms = await _roleManager.GetClaimsAsync(currentRole);

            List<String> currentPermValues = new List<string>();
            foreach (var p in currentPerms)
            {
                currentPermValues.Add(p.Value);
            }

            foreach (var p in allPerms)
            {
                if (selectedPerms.Contains(p.Value))
                {
                    if (!currentPermValues.Contains(p.Value))
                    {
                        await _roleManager.AddClaimAsync(currentRole, new System.Security.Claims.Claim(CustomClaimTypes.Permission, p.Value));
                    }
                }
                else
                {
                    if (currentPermValues.Contains(p.Value))
                    {
                        await _roleManager.RemoveClaimAsync(currentRole, p);
                    }
                }
            }
        }
    }
}
