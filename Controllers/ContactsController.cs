using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hager_Ind_CRM.Data;
using Hager_Ind_CRM.Models;
using Microsoft.AspNetCore.Authorization;
using Hager_Ind_CRM.ViewModels;
using Microsoft.EntityFrameworkCore.Storage;

namespace Hager_Ind_CRM.Controllers
{
    public class ContactsController : Controller
    {
        private readonly HagerIndContext _context;

        public ContactsController(HagerIndContext context)
        {
            _context = context;
        }

        // GET: Users
        [Authorize(Policy = PolicyTypes.Contacts.Read)]
        public async Task<IActionResult> Index(string? CompanyType)
        {
            var hagerIndContext = from a in _context.Contacts
                .Include(c => c.Company).ThenInclude(p => p.CompanyTypes).ThenInclude(p => p.Type)
                .Include(c => c.ContactCatagories).ThenInclude(p => p.Catagory)
                select a;


            if (CompanyType != null)
            {
                if (CompanyType == "Customer")
                {
                    hagerIndContext = hagerIndContext.Where(p => p.Company.CompanyTypes.Any(c => c.Type.Name == "Customer"));
                }
                else if (CompanyType == "Vendor")
                {
                    hagerIndContext = hagerIndContext.Where(p => p.Company.CompanyTypes.Any(c => c.Type.Name == "Vendor"));
                }
                else if (CompanyType == "Contractor")
                {
                    hagerIndContext = hagerIndContext.Where(p => p.Company.CompanyTypes.Any(c => c.Type.Name == "Contractor"));
                }
            }
            return View(await hagerIndContext.ToListAsync());
        }

        // GET: Users/Details/5
        [Authorize(Policy = PolicyTypes.Contacts.Detail)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .Include(d => d.Company).ThenInclude(d => d.CompanyTypes).ThenInclude(d => d.Type)
                .Include(d => d.Company).ThenInclude(d => d.BillingTerms)
                .Include(d => d.ContactCatagories).ThenInclude(d => d.Catagory)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            Contact contact = new Contact();
            PopulateAssignedCatagoriesData(contact);
            PopulateDropDownLists();
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,JobTitle,CellPhone,WorkPhone,Email,Active,CompanyID")] Contact contact,
            string[] selectedOptions)
        {
            UpdateContactCatagories(selectedOptions, contact);
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDropDownLists(contact);
            PopulateAssignedCatagoriesData(contact);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        [Authorize(Policy = PolicyTypes.Contacts.Update)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
               .Include(d => d.ContactCatagories).ThenInclude(d => d.Catagory)
               .Include(d => d.Company)
               .AsNoTracking()
               .SingleOrDefaultAsync(d => d.ID == id);
            if (contact == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(contact);
            PopulateAssignedCatagoriesData(contact);
            return View(contact);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = PolicyTypes.Contacts.Update)]
        public async Task<IActionResult> Edit(int id, string[] selectedOptions)
        {
            var contact = await _context.Contacts
                .Include(d => d.ContactCatagories).ThenInclude(d => d.Contact)
                .SingleOrDefaultAsync(p => p.ID == id);

            //Check that you got it or exit with a not found error
            if (contact == null)
            {
                return NotFound();
            }

            UpdateContactCatagories(selectedOptions, contact);

            if (await TryUpdateModelAsync<Contact>(contact, "",
                d => d.FirstName, d => d.LastName, d => d.JobTitle,
                d => d.CellPhone, d => d.WorkPhone, d => d.Email,
                d => d.Active, d => d.CompanyID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }

            }
            
            PopulateDropDownLists(contact);
            PopulateAssignedCatagoriesData(contact);
            return View(contact);
        }

        // GET: Users/Delete/5
        [Authorize(Policy = PolicyTypes.Contacts.Delete)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .Include(d => d.Company).ThenInclude(d => d.CompanyTypes).ThenInclude(d => d.Type)
                .Include(d => d.Company).ThenInclude(d => d.BillingTerms)
                .Include(d => d.ContactCatagories).ThenInclude(d => d.Catagory)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Users/Delete/5
        [Authorize(Policy = PolicyTypes.Contacts.Delete)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.ID == id);
        }

        private void PopulateDropDownLists(Contact contact = null)
        {
            ViewData["CompanyID"] = CompaniesSelectList(contact?.CompanyID);
        }
        private SelectList CompaniesSelectList(int? id)
        {
            var dQuery = (from d in _context.Companies
                          orderby d.ID
                          select d).ToList();
            return new SelectList(dQuery, "ID", "Name", id);
        }
        private void PopulateAssignedCatagoriesData(Contact contact)
        {
            var allOptions = _context.Catagories;
            var currentOptionsHS = new HashSet<int>(contact.ContactCatagories.Select(b => b.CatagoryID));
            var selected = new List<ListOptionVM>();
            var available = new List<ListOptionVM>();
            foreach (var s in allOptions)
            {
                if (currentOptionsHS.Contains(s.ID))
                {
                    selected.Add(new ListOptionVM
                    {
                        ID = s.ID,
                        DisplayText = s.Name
                    });
                }
                else
                {
                    available.Add(new ListOptionVM
                    {
                        ID = s.ID,
                        DisplayText = s.Name
                    });
                }
            }

            ViewData["selOpts"] = new MultiSelectList(selected.OrderBy(s => s.DisplayText), "ID", "DisplayText");
            ViewData["availOpts"] = new MultiSelectList(available.OrderBy(s => s.DisplayText), "ID", "DisplayText");
        }
        private void UpdateContactCatagories(string[] selectedOptions, Contact contactToUpdate)
        {
            if (selectedOptions == null)
            {
                contactToUpdate.ContactCatagories = new List<ContactCatagory>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedOptions);
            var currentOptionsHS = new HashSet<int>(contactToUpdate.ContactCatagories.Select(b => b.CatagoryID));
            foreach (var s in _context.Catagories)
            {
                if (selectedOptionsHS.Contains(s.ID.ToString()))
                {
                    if (!currentOptionsHS.Contains(s.ID))
                    {
                        contactToUpdate.ContactCatagories.Add(new ContactCatagory
                        {
                            CatagoryID = s.ID,
                            ContactID = contactToUpdate.ID
                        });
                    }
                }
                else
                {
                    if (currentOptionsHS.Contains(s.ID))
                    {
                        ContactCatagory specToRemove = contactToUpdate.ContactCatagories.SingleOrDefault(d => d.CatagoryID == s.ID);
                        _context.Remove(specToRemove);
                    }
                }
            }
        }
    }
}
