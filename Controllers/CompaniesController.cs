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

namespace Hager_Ind_CRM.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly HagerIndContext _context;

        public CompaniesController(HagerIndContext context)
        {
            _context = context;
        }

        // GET: Companies
        [Authorize(Policy = PolicyTypes.Companies.Read)]
        public async Task<IActionResult> Index(string? CompanyType)
        {
            var hagerIndContext = from a in _context.Companies
                .Include(c => c.Contacts)
                .Include(c => c.BillingCountry)
                .Include(c => c.BillingProvince)
                .Include(c => c.BillingTerms)
                .Include(c => c.Currency)
                .Include(c => c.ShippingCountry)
                .Include(c => c.ShippingProvince)
                .Include(c => c.CompanyTypes).ThenInclude(p => p.Type).ThenInclude(p => p.SubTypes)
                                  select a;

            if (CompanyType != null)
            {
                if (CompanyType == "Customer")
                {
                    hagerIndContext = hagerIndContext.Where(p => p.CompanyTypes.Any(c => c.Type.Name == "Customer"));
                }
                else if (CompanyType == "Vendor")
                {
                    hagerIndContext = hagerIndContext.Where(p => p.CompanyTypes.Any(c => c.Type.Name == "Vendor"));
                }
                else if (CompanyType == "Contractor")
                {
                    hagerIndContext = hagerIndContext.Where(p => p.CompanyTypes.Any(c => c.Type.Name == "Contractor"));
                }
            }

            return View(await hagerIndContext.ToListAsync());
        }

        // GET: Companies/Details/5
        [Authorize(Policy = PolicyTypes.Companies.Detail)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .Include(c => c.Contacts)
                .Include(c => c.BillingCountry)
                .Include(c => c.BillingProvince)
                .Include(c => c.BillingTerms)
                .Include(c => c.Currency)
                .Include(c => c.ShippingCountry)
                .Include(c => c.ShippingProvince)
                .Include(c => c.CompanyTypes).ThenInclude(p => p.Type).ThenInclude(p => p.SubTypes)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (company == null)
            {
                return NotFound();
            }

            GetContacts(id);
            return View(company);
        }

        // GET: Companies/Create
        [Authorize(Policy = PolicyTypes.Companies.Create)]
        public IActionResult Create()
        {
            Company company = new Company();
            ViewData["BillingCountryID"] = new SelectList(_context.Countries, "ID", "Name");
            ViewData["BillingProvinceID"] = new SelectList(_context.Provinces, "ID", "Name");
            ViewData["BillingTermsID"] = new SelectList(_context.BillingTerms, "ID", "Terms");
            ViewData["CurrencyID"] = new SelectList(_context.Currencies, "ID", "Name");
            ViewData["ShippingCountryID"] = new SelectList(_context.Countries, "ID", "Name");
            ViewData["ShippingProvinceID"] = new SelectList(_context.Provinces, "ID", "Name");
            PopulateAssignedCatagoriesData(company);
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = PolicyTypes.Companies.Create)]
        public async Task<IActionResult> Create([Bind("ID,Name,Location,CredCheck,BillingTermsID,CurrencyID,Phone,Website,BillingAddress1,BillingAddress2,BillingProvinceID,BillingPostalCode,BillingCountryID,ShippingAddress1,ShippingAddress2,ShippingProvinceID,ShippingPostalCode,ShippingCountryID,Active,Notes")] Company company)
        {
            if (ModelState.IsValid)
            {
                if (_context.Companies.ToList().Any(c => c.Name == company.Name))
                {
                    var id = (from d in _context.Companies
                              where d.Name == company.Name
                              select d.ID).SingleOrDefault();
                    ViewBag.Msg = id;
                    ViewBag.Message = "The Company already exists.";
                }
                else
                {
                    _context.Add(company);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
            }
            ViewData["BillingCountryID"] = new SelectList(_context.Countries, "ID", "Name", company.BillingCountryID);
            ViewData["BillingProvinceID"] = new SelectList(_context.Provinces, "ID", "Name", company.BillingProvinceID);
            ViewData["BillingTermsID"] = new SelectList(_context.BillingTerms, "ID", "Terms", company.BillingTermsID);
            ViewData["CurrencyID"] = new SelectList(_context.Currencies, "ID", "Name", company.CurrencyID);
            ViewData["ShippingCountryID"] = new SelectList(_context.Countries, "ID", "Name", company.ShippingCountryID);
            ViewData["ShippingProvinceID"] = new SelectList(_context.Provinces, "ID", "Name", company.ShippingProvinceID);
            PopulateAssignedCatagoriesData(company);
            return View(company);
        }

        // GET: Companies/Edit/5
        [Authorize(Policy = PolicyTypes.Companies.Update)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var company = await _context.Companies.FindAsync(id);

            var company = await _context.Companies
               .Include(d => d.CompanySubTypes).ThenInclude(d => d.SubType)
               .AsNoTracking()
               .SingleOrDefaultAsync(d => d.ID == id);

            if (company == null)
            {
                return NotFound();
            }
            ViewData["BillingCountryID"] = new SelectList(_context.Countries, "ID", "Name", company.BillingCountryID);
            ViewData["BillingProvinceID"] = new SelectList(_context.Provinces, "ID", "Name", company.BillingProvinceID);
            ViewData["BillingTermsID"] = new SelectList(_context.BillingTerms, "ID", "Terms", company.BillingTermsID);
            ViewData["CurrencyID"] = new SelectList(_context.Currencies, "ID", "Name", company.CurrencyID);
            ViewData["ShippingCountryID"] = new SelectList(_context.Countries, "ID", "Name", company.ShippingCountryID);
            ViewData["ShippingProvinceID"] = new SelectList(_context.Provinces, "ID", "Name", company.ShippingProvinceID);
            PopulateAssignedCatagoriesData(company);
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = PolicyTypes.Companies.Update)]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Location,CredCheck,BillingTermsID,CurrencyID,Phone,Website,BillingAddress1,BillingAddress2,BillingProvinceID,BillingPostalCode,BillingCountryID,ShippingAddress1,ShippingAddress2,ShippingProvinceID,ShippingPostalCode,ShippingCountryID,Active,Notes")] Company company)
        {
            if (id != company.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BillingCountryID"] = new SelectList(_context.Countries, "ID", "Name", company.BillingCountryID);
            ViewData["BillingProvinceID"] = new SelectList(_context.Provinces, "ID", "Name", company.BillingProvinceID);
            ViewData["BillingTermsID"] = new SelectList(_context.BillingTerms, "ID", "Terms", company.BillingTermsID);
            ViewData["CurrencyID"] = new SelectList(_context.Currencies, "ID", "Name", company.CurrencyID);
            ViewData["ShippingCountryID"] = new SelectList(_context.Countries, "ID", "Name", company.ShippingCountryID);
            ViewData["ShippingProvinceID"] = new SelectList(_context.Provinces, "ID", "Name", company.ShippingProvinceID);
            PopulateAssignedCatagoriesData(company);
            return View(company);
        }

        // GET: Companies/Delete/5
        [Authorize(Policy = PolicyTypes.Companies.Delete)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .Include(c => c.BillingCountry)
                .Include(c => c.BillingProvince)
                .Include(c => c.BillingTerms)
                .Include(c => c.Currency)
                .Include(c => c.ShippingCountry)
                .Include(c => c.ShippingProvince)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = PolicyTypes.Companies.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.ID == id);
        }

        private void PopulateDropDownLists(Company company = null)
        {
            ViewData["BillingTermsID"] = BillingTermsSelectList(company?.BillingTermsID);
        }
        private SelectList BillingTermsSelectList(int? id)
        {
            var dQuery = (from d in _context.BillingTerms
                          orderby d.OrderID
                          select d).ToList();
            return new SelectList(dQuery, "ID", "Terms", id);
        }

        private void GetContacts(int? id)
        {
            var contacts = (from d in _context.Contacts
                            where d.CompanyID == id
                            orderby d.FirstName
                            select d);
            ViewData["Contacts"] = contacts;
        }

        private void PopulateAssignedCatagoriesData(Company company)
        {
            var allOptions = _context.SubTypes.Include(s => s.Type);
            var currentOptionsHS = new HashSet<int>(company.CompanySubTypes.Select(b => b.SubTypeID));
            
            var selectedCustomer = new List<ListOptionVM>();
            var availableCustomer = new List<ListOptionVM>();

            var selectedVendor = new List<ListOptionVM>();
            var availableVendor = new List<ListOptionVM>();

            var selectedContractor = new List<ListOptionVM>();
            var availableContractor = new List<ListOptionVM>();

            foreach (var s in allOptions)
            {
                if (currentOptionsHS.Contains(s.ID))
                {
                    if(s.Type.Name == "Customer")
                    {
                        selectedCustomer.Add(new ListOptionVM
                        {
                            ID = s.ID,
                            DisplayText = s.Name
                        });
                    }
                    else if (s.Type.Name == "Vendor")
                    {
                        selectedVendor.Add(new ListOptionVM
                        {
                            ID = s.ID,
                            DisplayText = s.Name
                        });
                    }
                    else if (s.Type.Name == "Contractor")
                    {
                        selectedContractor.Add(new ListOptionVM
                        {
                            ID = s.ID,
                            DisplayText = s.Name
                        });
                    }
                }
                else
                {
                    if (s.Type.Name == "Customer")
                    {
                        availableCustomer.Add(new ListOptionVM
                        {
                            ID = s.ID,
                            DisplayText = s.Name
                        });
                    }
                    else if (s.Type.Name == "Vendor")
                    {
                        availableVendor.Add(new ListOptionVM
                        {
                            ID = s.ID,
                            DisplayText = s.Name
                        });
                    }
                    else if (s.Type.Name == "Contractor")
                    {
                        availableContractor.Add(new ListOptionVM
                        {
                            ID = s.ID,
                            DisplayText = s.Name
                        });
                    }
                }
            }

            ViewData["selOptsCustomer"] = new MultiSelectList(selectedCustomer.OrderBy(s => s.DisplayText), "ID", "DisplayText");
            ViewData["availOptsCustomer"] = new MultiSelectList(availableCustomer.OrderBy(s => s.DisplayText), "ID", "DisplayText");

            ViewData["selOptsVendor"] = new MultiSelectList(selectedVendor.OrderBy(s => s.DisplayText), "ID", "DisplayText");
            ViewData["availOptsVendor"] = new MultiSelectList(availableVendor.OrderBy(s => s.DisplayText), "ID", "DisplayText");

            ViewData["selOptsContractor"] = new MultiSelectList(selectedContractor.OrderBy(s => s.DisplayText), "ID", "DisplayText");
            ViewData["availOptsContractor"] = new MultiSelectList(availableContractor.OrderBy(s => s.DisplayText), "ID", "DisplayText");
        }
    }
}
