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
                .Include(c => c.Contacts).ThenInclude(c => c.ContactCatagories).ThenInclude(c => c.Catagory)
                .Include(c => c.BillingCountry)
                .Include(c => c.BillingProvince)
                .Include(c => c.BillingTerms)
                .Include(c => c.Currency)
                .Include(c => c.ShippingCountry)
                .Include(c => c.ShippingProvince)
                .Include(c => c.CompanyTypes).ThenInclude(p => p.Type).ThenInclude(p => p.SubTypes)
                .Include(c => c.CompanySubTypes).ThenInclude(p => p.SubType)
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
            PopulateDropDownLists(company);
            PopulateAssignedCatagoriesData(company);
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = PolicyTypes.Companies.Create)]
        public async Task<IActionResult> Create([Bind("ID,Name,Location,CredCheck,BillingTermsID,CurrencyID,Phone,Website,BillingAddress1,BillingAddress2,BillingProvinceID,BillingPostalCode,BillingCountryID,ShippingAddress1,ShippingAddress2,ShippingProvinceID,ShippingPostalCode,ShippingCountryID,Active,Notes")] Company company,
            string[] selectedOptionsCustomer, string[] selectedOptionsVendor, string[] selectedOptionsContractor,
            string isCustomer, string isVendor, string isContractor)
        {
            if (ModelState.IsValid)
            {
                if (_context.Companies.ToList().Any(c => (c.Name == company.Name)&&(c.Phone==company.Phone)))
                {
                    var id = (from d in _context.Companies
                              where d.Name == company.Name && d.Phone == company.Phone
                              select d.ID).SingleOrDefault();
                    ViewBag.Msg = id;
                    ViewBag.Message = "The Company already exists.";
                    UpdateTypesAndSubs(company, isCustomer, selectedOptionsCustomer, isVendor, selectedOptionsVendor, isContractor, selectedOptionsContractor);
                    _context.Add(company);

                    await _context.SaveChangesAsync();
                    var id2 = (from d in _context.Companies
                               where d.ID == company.ID
                               select d.ID).SingleOrDefault();
                    ViewBag.ID = id2;
                }
                else
                {
                    UpdateTypesAndSubs(company, isCustomer, selectedOptionsCustomer, isVendor, selectedOptionsVendor, isContractor, selectedOptionsContractor);
                    _context.Add(company);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
            }
            company.BillingCountry = await _context.Countries.FindAsync(company.BillingCountryID);
            company.BillingProvince = await _context.Provinces.FindAsync(company.BillingProvinceID);
            company.ShippingCountry = await _context.Countries.FindAsync(company.ShippingCountryID);
            company.ShippingProvince = await _context.Provinces.FindAsync(company.ShippingProvinceID);
            PopulateDropDownLists(company);
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
               .Include(d => d.CompanyTypes).ThenInclude(d => d.Type)
               .AsNoTracking()
               .SingleOrDefaultAsync(d => d.ID == id);

            if (company == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(company);
            PopulateAssignedCatagoriesData(company);
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = PolicyTypes.Companies.Update)]
        public async Task<IActionResult> Edit(int id,
            string[] selectedOptionsCustomer, string[] selectedOptionsVendor, string[] selectedOptionsContractor,
            string isCustomer, string isVendor, string isContractor
            )
        {
            var companyToUpdate = await _context.Companies
                .Include(d => d.CompanyTypes).ThenInclude(d => d.Type)
                .Include(d => d.CompanySubTypes).ThenInclude(d => d.SubType)
                .Include(c => c.Contacts).ThenInclude(c => c.ContactCatagories).ThenInclude(c => c.Catagory)
                .Include(c => c.BillingCountry)
                .Include(c => c.BillingProvince)
                .Include(c => c.BillingTerms)
                .Include(c => c.Currency)
                .Include(c => c.ShippingCountry)
                .Include(c => c.ShippingProvince)
                .SingleOrDefaultAsync(p => p.ID == id);

            //Check that you got it or exit with a not found error
            if (companyToUpdate == null)
            {
                return NotFound();
            }

            //UpdateTypesAndSubs(companyToUpdate, isCustomer, selectedOptionsCustomer, isVendor, selectedOptionsVendor, isContractor, selectedOptionsContractor);

            if (await TryUpdateModelAsync<Company>(companyToUpdate, "",
                d => d.Name, d => d.Location, d => d.CredCheck, d => d.Active,
                d => d.BillingTermsID, d => d.CurrencyID, d => d.Phone,
                d => d.Website, d => d.BillingAddress1, d => d.BillingAddress2,
                d => d.BillingProvinceID, d => d.BillingPostalCode, d => d.BillingCountryID,
                d => d.ShippingAddress1, d => d.ShippingAddress2, d => d.ShippingProvinceID,
                d => d.ShippingPostalCode, d => d.ShippingCountryID, d => d.Notes
                ))
            {
                try
                {
                    UpdateTypesAndSubs(companyToUpdate, isCustomer, selectedOptionsCustomer, isVendor, selectedOptionsVendor, isContractor, selectedOptionsContractor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(companyToUpdate.ID))
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


            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(companyToUpdate);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!CompanyExists(companyToUpdate.ID))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}

            PopulateDropDownLists(companyToUpdate);
            PopulateAssignedCatagoriesData(companyToUpdate);
            return View(companyToUpdate);
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
            if ((company?.BillingProvinceID).HasValue)
            {
                ViewData["BillingCountryID"] = CountriesSelectList(company?.BillingCountryID);
                ViewData["BillingProvinceID"] = ProvincesSelectList(company?.BillingCountryID, company?.BillingProvinceID);
            }
            else
            {
                ViewData["BillingCountryID"] = CountriesSelectList(null);
                ViewData["BillingProvinceID"] = ProvincesSelectList(null,null);
            }
            if ((company?.ShippingProvinceID).HasValue)
            {
                ViewData["ShippingCountryID"] = CountriesSelectList(company?.ShippingCountryID);
                ViewData["ShippingProvinceID"] = ProvincesSelectList(company?.ShippingCountryID, company?.ShippingProvinceID);
            }
            else
            {
                ViewData["ShippingCountryID"] = CountriesSelectList(null);
                ViewData["ShippingProvinceID"] = ProvincesSelectList(null,null);
            }
            ViewData["BillingTermsID"] = BillingTermsSelectList(company?.BillingTermsID);
            ViewData["CurrencyID"] = CurrenciesSelectList(company?.CurrencyID);
        }
        private SelectList BillingTermsSelectList(int? id)
        {
            var dQuery = (from d in _context.BillingTerms
                          orderby d.OrderID
                          select d).ToList();
            return new SelectList(dQuery, "ID", "Terms", id);
        }
        private SelectList CurrenciesSelectList(int? id)
        {
            var dQuery = (from d in _context.Currencies
                          orderby d.OrderID
                          select d).ToList();
            return new SelectList(dQuery, "ID", "Name", id);
        }

        private SelectList CountriesSelectList(int? id)
        {
            var dQuery = (from d in _context.Countries
                          orderby d.OrderID
                          select d).ToList();
            return new SelectList(dQuery, "ID", "Name", id);
        }
        private SelectList ProvincesSelectList(int? CountryID, int? id)
        {
            var query = from c in _context.Provinces.Include(c => c.Country)
                        select c;
            if (CountryID.HasValue)
            {
                query = query.Where(p => p.CountryID == CountryID);
            }
            return new SelectList(query.OrderBy(p => p.Name), "ID", "Name", id);
        }

        [HttpGet]
        public JsonResult GetProvinces(int? ID)
        {
            return Json(ProvincesSelectList(ID, null));
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
                    if (s.Type.Name == "Customer")
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

        private void UpdateTypesAndSubs(Company company, 
            string isCustomer, string[] subTypesCustomer,
            string isVendor, string[] subTypesVendor,
            string isContractor, string[] subTypesContractor)
        {
            List<string> types = new List<string>();
            List<string> subtypes = new List<string>();

            if (isCustomer == "Customer")
            {
                types.Add(isCustomer);
                subtypes.AddRange(subTypesCustomer);
            }
            
            if (isVendor == "Vendor")
            {
                types.Add(isVendor);
                subtypes.AddRange(subTypesVendor);
            }

            if (isContractor == "Contractor")
            {
                types.Add(isContractor);
                subtypes.AddRange(subTypesContractor);
            }

            if (types.Count == 0)
            {
                foreach (var t in company.CompanyTypes)
                {
                    _context.Remove(t);
                }

                foreach (var s in company.CompanySubTypes)
                {
                    _context.Remove(s);
                }

                company.CompanyTypes = new List<CompanyType>();
                company.CompanySubTypes = new List<CompanySubType>();

                return;
            }
            else
            {
                var selectedOptionsHS = new HashSet<string>(types);
                var currentOptionsHS = new HashSet<int>(company.CompanyTypes.Select(b => b.TypeID));
                foreach (var s in _context.Types)
                {
                    if (selectedOptionsHS.Contains(s.Name.ToString()))
                    {
                        if (!currentOptionsHS.Contains(s.ID))
                        {
                            company.CompanyTypes.Add(new CompanyType
                            {
                                TypeID = s.ID,
                                CompanyID = company.ID
                            });
                        }
                    }
                    else
                    {
                        if (currentOptionsHS.Contains(s.ID))
                        {
                            CompanyType typeToRemove = company.CompanyTypes.SingleOrDefault(d => d.TypeID == s.ID);
                            _context.Remove(typeToRemove);
                        }
                    }
                }

                var selectedSubTypesHS = new HashSet<string>(subtypes);
                var currentSubTypesHS = new HashSet<int>(company.CompanySubTypes.Select(b => b.SubTypeID));
                foreach (var s in _context.SubTypes)
                {
                    if (selectedSubTypesHS.Contains(s.ID.ToString()))
                    {
                        if (!currentSubTypesHS.Contains(s.ID))
                        {
                            company.CompanySubTypes.Add(new CompanySubType
                            {
                                SubTypeID = s.ID,
                                CompanyID = company.ID
                            });
                        }
                    }
                    else
                    {
                        if (currentSubTypesHS.Contains(s.ID))
                        {
                            CompanySubType stypeToRemove = company.CompanySubTypes.SingleOrDefault(d => d.SubTypeID == s.ID);
                            _context.Remove(stypeToRemove);
                        }
                    }
                }
            }

            //if (type != "false")
            //{
            //    var currentOptionsHS = new HashSet<int>(company.CompanyTypes.Select(b => b.TypeID));

            //    foreach (var s in _context.Types)
            //    {
            //        if (type == s.Name)
            //        {
            //            if (!currentOptionsHS.Contains(s.ID))
            //            {
            //                company.CompanyTypes.Add(new CompanyType
            //                {
            //                    CompanyID = company.ID,
            //                    TypeID = s.ID
            //                });
            //            }
            //        }
            //    }


            //}


            //var selectedOptionsHS = new HashSet<string>(subTypes);
            //var currentOptionsHS1 = new HashSet<int>(company.CompanySubTypes.Select(b => b.SubTypeID));
            //if (selectedOptionsHS.Count > 0)
            //{
            //    foreach (var s in _context.SubType)
            //    {
            //        if (selectedOptionsHS.Contains(s.ID.ToString()))
            //        {
            //            if (!currentOptionsHS1.Contains(s.ID))
            //            {
            //                company.CompanySubTypes.Add(new CompanySubType
            //                {
            //                    SubTypeID = s.ID,
            //                    CompanyID = company.ID
            //                });
            //            }
            //        }
            //    }
            //}

        }
        private void PopulateAssignedCatagoriesData2(Company company)
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
                    if (s.Type.Name == "Customer")
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

            ViewData["selOptsCustomer2"] = new MultiSelectList(selectedCustomer.OrderBy(s => s.DisplayText), "ID", "DisplayText");
            ViewData["availOptsCustomer2"] = new MultiSelectList(availableCustomer.OrderBy(s => s.DisplayText), "ID", "DisplayText");

            ViewData["selOptsVendor2"] = new MultiSelectList(selectedVendor.OrderBy(s => s.DisplayText), "ID", "DisplayText");
            ViewData["availOptsVendor2"] = new MultiSelectList(availableVendor.OrderBy(s => s.DisplayText), "ID", "DisplayText");

            ViewData["selOptsContractor2"] = new MultiSelectList(selectedContractor.OrderBy(s => s.DisplayText), "ID", "DisplayText");
            ViewData["availOptsContractor2"] = new MultiSelectList(availableContractor.OrderBy(s => s.DisplayText), "ID", "DisplayText");
        }

        // GET: Companies/Merge
        [Authorize(Policy = PolicyTypes.Companies.Create)]
        public ActionResult Merge(int id1, int id2)
        {
            if (id1 < 0 || id2 < 0)
            {
                return NotFound();
            }

            var hagerIndContext = from a in _context.Companies
                .Include(d => d.CompanyTypes).ThenInclude(d => d.Type)
                .Include(d => d.CompanySubTypes).ThenInclude(d => d.SubType)
                .Include(c => c.Contacts).ThenInclude(c => c.ContactCatagories).ThenInclude(c => c.Catagory)
                .Include(c => c.BillingCountry)
                .Include(c => c.BillingProvince)
                .Include(c => c.BillingTerms)
                .Include(c => c.Currency)
                .Include(c => c.ShippingCountry)
                .Include(c => c.ShippingProvince)
                                  select a;

            Company record1 = hagerIndContext.SingleOrDefault(p => p.ID == id1);
            Company record2 = hagerIndContext.SingleOrDefault(p => p.ID == id2);
            PopulateAssignedCatagoriesData(record1);
            PopulateAssignedCatagoriesData2(record2);

            var merge = new MergeVM();
            merge.Company1 = record1;
            merge.Company2 = record2;
            return View(merge);
        }

        // POST: Companies/Merge
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = PolicyTypes.Companies.Create)]
        public async Task<IActionResult> Merge(int input_ID_1, int input_ID_2,

            string? input_Location, string input_Name, int? input_BillingTermsID, int? input_CurrencyID,
            Int64? input_Phone, string? input_Website, string? input_Notes, string? input_BillingAddress1,
            string? input_BillingAddress2, int? input_BillingCountryID, int? input_BillingProvinceID,
            string? input_BillingPostal, string? input_ShippingAddress1, string? input_ShippingAddress2,
            int? input_ShippingCountryID, int? input_ShippingProvinceID, string? input_ShippingPostal,
            bool input_CreditCheck, bool input_Active, string[] YourCheckboxes,

            string[] selectedOptionsCustomer, string[] selectedOptionsVendor, string[] selectedOptionsContractor,
            string[] selectedOptionsCustomer2, string[] selectedOptionsVendor2, string[] selectedOptionsContractor2,
            string isCustomer, string isVendor, string isContractor

            )
        {
            var mergeCMP = await _context.Companies
                .Include(d => d.CompanyTypes).ThenInclude(d => d.Type)
                .Include(d => d.CompanySubTypes).ThenInclude(d => d.SubType)
                .Include(c => c.Contacts).ThenInclude(c => c.ContactCatagories).ThenInclude(c => c.Catagory)
                .Include(c => c.BillingCountry)
                .Include(c => c.BillingProvince)
                .Include(c => c.BillingTerms)
                .Include(c => c.Currency)
                .Include(c => c.ShippingCountry)
                .Include(c => c.ShippingProvince)
                .SingleOrDefaultAsync(p => p.ID == input_ID_1);

            //Check that you got it or exit with a not found error
            if (mergeCMP == null)
            {
                return NotFound();
            }

            //if (await TryUpdateModelAsync<Company>(mergeCMP, "",
            //    d => d.Name, d => d.Location, d => d.CredCheck, d => d.Active,
            //    d => d.BillingTermsID, d => d.CurrencyID, d => d.Phone,
            //    d => d.Website, d => d.BillingAddress1, d => d.BillingAddress2,
            //    d => d.BillingProvinceID, d => d.BillingPostalCode, d => d.BillingCountryID,
            //    d => d.ShippingAddress1, d => d.ShippingAddress2, d => d.ShippingProvinceID,
            //    d => d.ShippingPostalCode, d => d.ShippingCountryID, d => d.Notes))
            //{
            if (1 == 1)
            {
                try
                {
                    //UpdateTypesAndSubs(mergeCMP, isCustomer, selectedOptionsCustomer, isVendor, selectedOptionsVendor, isContractor, selectedOptionsContractor);

                    //Update all data
                    mergeCMP.Location = input_Location;
                    mergeCMP.Name = input_Name;
                    mergeCMP.Phone = input_Phone;
                    mergeCMP.Website = input_Website;
                    mergeCMP.Notes = input_Notes;
                    mergeCMP.BillingAddress1 = input_BillingAddress1;
                    mergeCMP.BillingAddress2 = input_BillingAddress2;
                    mergeCMP.BillingPostalCode = input_BillingPostal;
                    mergeCMP.ShippingAddress1 = input_ShippingAddress1;
                    mergeCMP.ShippingAddress2 = input_ShippingAddress2;
                    mergeCMP.ShippingPostalCode = input_ShippingPostal;
                    mergeCMP.CredCheck = input_CreditCheck;
                    mergeCMP.Active = input_Active;

                    //foreign keys                    
                    mergeCMP.CurrencyID = input_CurrencyID;
                    mergeCMP.BillingTermsID = input_BillingTermsID;
                    mergeCMP.BillingCountryID = input_BillingCountryID;
                    mergeCMP.BillingProvinceID = input_BillingProvinceID;
                    mergeCMP.ShippingCountryID = input_ShippingCountryID;
                    mergeCMP.ShippingProvinceID = input_ShippingProvinceID;
                    //assign the types / sub types
                    if (selectedOptionsContractor.Length != 0 || selectedOptionsCustomer.Length != 0 || selectedOptionsVendor.Length != 0)
                    {
                        UpdateTypesAndSubs(mergeCMP, isCustomer, selectedOptionsCustomer, isVendor, selectedOptionsVendor, isContractor, selectedOptionsContractor);
                    }
                    else if (selectedOptionsContractor2.Length != 0 || selectedOptionsCustomer2.Length != 0 || selectedOptionsVendor2.Length != 0)
                    {
                        UpdateTypesAndSubs(mergeCMP, isCustomer, selectedOptionsCustomer2, isVendor, selectedOptionsVendor2, isContractor, selectedOptionsContractor2);
                    }
                    else
                    {
                        UpdateTypesAndSubs(mergeCMP, isCustomer, selectedOptionsCustomer, isVendor, selectedOptionsVendor, isContractor, selectedOptionsContractor);
                    }

                    //remove each company ID
                    var contactContext_1 = await _context.Contacts
                        .Include(c => c.Company).ThenInclude(p => p.CompanyTypes).ThenInclude(p => p.Type)
                        .Include(c => c.ContactCatagories).ThenInclude(p => p.Catagory)
                        .Where(c => c.CompanyID == input_ID_1)
                        .ToListAsync();
                    var contactContext_2 = await _context.Contacts
                       .Include(c => c.Company).ThenInclude(p => p.CompanyTypes).ThenInclude(p => p.Type)
                       .Include(c => c.ContactCatagories).ThenInclude(p => p.Catagory)
                       .Where(c => c.CompanyID == input_ID_2)
                       .ToListAsync();

                    foreach (var contact in contactContext_1)
                    {
                        contact.CompanyID = null;
                    }
                    foreach (var contact in contactContext_2)
                    {
                        contact.CompanyID = null;
                    }

                    //assign the contacts the new ID
                    foreach (var item in YourCheckboxes)
                    {
                        var contact = await _context.Contacts
                        .Include(c => c.Company).ThenInclude(p => p.CompanyTypes).ThenInclude(p => p.Type)
                        .Include(c => c.ContactCatagories).ThenInclude(p => p.Catagory)
                        .SingleOrDefaultAsync(p => p.ID == int.Parse(item));

                        contact.CompanyID = input_ID_1;
                    }

                    await _context.SaveChangesAsync();

                    var delCMP = await _context.Companies.FindAsync(input_ID_2);
                    _context.Companies.Remove(delCMP);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(mergeCMP.ID))
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
            //}
            PopulateAssignedCatagoriesData(mergeCMP);
            ////return View(mergeCMP);
            return RedirectToAction("Details", new { id = input_ID_1 });
        }

    }
}
