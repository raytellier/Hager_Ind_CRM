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
using System.Collections.ObjectModel;

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
        public async Task<IActionResult> Index(string? CompanyType, string[] YourCheckboxes)
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

            if (YourCheckboxes.Count() < 2)
            {
                return View(await hagerIndContext.ToListAsync());
            }
            else
            {
                //int[] IDs = new int[] {};
                //foreach (var item in YourCheckboxes)
                //{
                //    IDs.Append(int.Parse(item));
                //}

                //var mergeB = (from d in hagerIndContext
                //              where IDs.Contains(d.ID)
                //              orderby d.Name
                //              select d);



                //var mergeB = hagerIndContext.Where(d => YourCheckboxes(d.ID));
                
                List<Company> companyMerge = new List<Company>();

                foreach (var item in YourCheckboxes)
                {
                    //var mergeB = (from d in hagerIndContext
                    //              where d.ID == int.Parse(item)
                    //              select d);





                    Company record = _context.Companies.SingleOrDefault(p => p.ID == int.Parse(item));
                    //var record = hagerIndContext.Where(p => p.ID == int.Parse(item));
                    companyMerge.Add(record);

                    //companiesMerge.Append(record);
                }
                //ViewData["MergeRecords"] = companyMerge;
                //ViewData["Companies"] = mergeB;

                //IEnumerable<Company> companies = ViewData["Companies"] as IEnumerable<Company>;
                //IEnumerable<Company> companies = companyMerge;
                return RedirectToAction("Merge", new { id1 = companyMerge[0].ID, id2 = companyMerge[1].ID });
            }
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
        public async Task<IActionResult> Create([Bind("ID,Name,Location,CredCheck,BillingTermsID,CurrencyID,Phone,Website,BillingAddress1,BillingAddress2,BillingProvinceID,BillingPostalCode,BillingCountryID,ShippingAddress1,ShippingAddress2,ShippingProvinceID,ShippingPostalCode,ShippingCountryID,Active,Notes")] Company company,
            string[] selectedOptionsCustomer, string[] selectedOptionsVendor, string[] selectedOptionsContractor,
            string isCustomer, string isVendor, string isContractor)
        {
            if (ModelState.IsValid)
            {
                if (_context.Companies.ToList().Any(c => (c.Name == company.Name)&&(c.Phone==company.Phone)))
                {
                    var id = (from d in _context.Companies
                              where d.Name == company.Name && d.Phone==company.Phone
                              select d.ID).SingleOrDefault();
                    ViewBag.Msg = id;
                    ViewBag.Message = "The Company already exists.";
                }
                else
                {
                    UpdateTypesAndSubs(company, isCustomer, selectedOptionsCustomer, isVendor, selectedOptionsVendor, isContractor, selectedOptionsContractor);
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
               .Include(d => d.CompanyTypes).ThenInclude(d => d.Type)
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
            ViewData["BillingCountryID"] = new SelectList(_context.Countries, "ID", "Name", companyToUpdate.BillingCountryID);
            ViewData["BillingProvinceID"] = new SelectList(_context.Provinces, "ID", "Name", companyToUpdate.BillingProvinceID);
            ViewData["BillingTermsID"] = new SelectList(_context.BillingTerms, "ID", "Terms", companyToUpdate.BillingTermsID);
            ViewData["CurrencyID"] = new SelectList(_context.Currencies, "ID", "Name", companyToUpdate.CurrencyID);
            ViewData["ShippingCountryID"] = new SelectList(_context.Countries, "ID", "Name", companyToUpdate.ShippingCountryID);
            ViewData["ShippingProvinceID"] = new SelectList(_context.Provinces, "ID", "Name", companyToUpdate.ShippingProvinceID);

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
            ViewData["BillingTermsID"] = BillingTermsSelectList(company?.BillingTermsID);
        }
        private SelectList BillingTermsSelectList(int? id)
        {
            var dQuery = (from d in _context.BillingTerms
                          orderby d.OrderID
                          select d).ToList();
            return new SelectList(dQuery, "ID", "Terms", id);
        }

        //===========================================================================
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
        }

        //======================================================================= Merge Action

        // GET: Companies/Merge
        [Authorize(Policy = PolicyTypes.Companies.Create)]
        public ActionResult Merge(int id1, int id2)
        {
            if (id1 < 0 || id2 < 0)
            {
                return NotFound();
            }

            //var companyToUpdate = from a in _context.Companies
            //    .Include(d => d.CompanyTypes).ThenInclude(d => d.Type)
            //    .Include(d => d.CompanySubTypes).ThenInclude(d => d.SubType)
            //    .Include(c => c.Contacts).ThenInclude(c => c.ContactCatagories).ThenInclude(c => c.Catagory)
            //    .Include(c => c.BillingCountry)
            //    .Include(c => c.BillingProvince)
            //    .Include(c => c.BillingTerms)
            //    .Include(c => c.Currency)
            //    .Include(c => c.ShippingCountry)
            //    .Include(c => c.ShippingProvince)
            //                      select a;

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

            Company record1 = hagerIndContext.SingleOrDefault(p => p.ID == id1);
            Company record2 = hagerIndContext.SingleOrDefault(p => p.ID == id2);

            //ViewData["Company1"] = company1;
            //ViewData["Company2"] = company2;

            
            var merge = new MergeVM();
            merge.Company1 = record1;
            merge.Company2 = record2;
            return View(merge);
            

            //IEnumerable<Company> companies = ViewData["MergeRecords"] as IEnumerable<Company>;

            //ViewData["Name"] = new SelectList(companies, "Name", "Name");
            //ViewData["Location"] = new SelectList(companies, "Location", "Location");
            //ViewData["Name"] = new SelectList(companies, "Name", "Name");
            //ViewData["BillingTermsID"] = new SelectList(companies, "BillingTermsID", "BillingTerms.Term");
            //ViewData["Name"] = new SelectList(companies, "Name", "Name");
            //ViewData["Location"] = new SelectList(companies, "Location", "Location");
            //ViewData["Name"] = new SelectList(companies, "Name", "Name");
            //ViewData["Location"] = new SelectList(companies, "Location", "Location");
            //ViewData["Name"] = new SelectList(companies, "Name", "Name");
            //ViewData["Location"] = new SelectList(companies, "Location", "Location");
            //PopulateAssignedCatagoriesData(company);
        }

        // POST: Companies/Merge
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = PolicyTypes.Companies.Create)]
        public async Task<IActionResult> Merge(int input_ID_1, int input_ID_2,
            //string[] selectedOptionsCustomer, string[] selectedOptionsVendor, string[] selectedOptionsContractor,
            //string isCustomer, string isVendor, string isContractor,

            string input_Location, string input_Name, int input_BillingTermsID, int input_CurrencyID,
            Int64 input_Phone, string input_Website, string input_Notes, string input_BillingAddress1,
            string input_BillingAddress2, int input_BillingCountryID, int input_BillingProvinceID,
            string input_BillingPostal, string input_ShippingAddress1, string input_ShippingAddress2,
            int input_ShippingCountryID, int input_ShippingProvinceID, string input_ShippingPostal,
            bool input_CreditCheck, bool input_Active
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

            //UpdateTypesAndSubs(companyToUpdate, isCustomer, selectedOptionsCustomer, isVendor, selectedOptionsVendor, isContractor, selectedOptionsContractor);

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
                    mergeCMP.BillingTermsID = input_BillingTermsID;
                    mergeCMP.CurrencyID = input_CurrencyID;
                    mergeCMP.Phone = input_Phone;
                    mergeCMP.Website = input_Website;
                    mergeCMP.Notes = input_Notes;
                    mergeCMP.BillingAddress1 = input_BillingAddress1;
                    mergeCMP.BillingAddress2 = input_BillingAddress2;
                    mergeCMP.BillingCountryID = input_BillingCountryID;
                    mergeCMP.BillingProvinceID = input_BillingProvinceID;
                    mergeCMP.BillingPostalCode = input_BillingPostal;
                    mergeCMP.ShippingAddress1 = input_ShippingAddress1;
                    mergeCMP.ShippingAddress2 = input_ShippingAddress2;
                    mergeCMP.ShippingCountryID = input_ShippingCountryID;
                    mergeCMP.ShippingProvinceID = input_ShippingProvinceID;
                    mergeCMP.ShippingPostalCode = input_ShippingPostal;
                    mergeCMP.CredCheck = input_CreditCheck;
                    mergeCMP.Active = input_Active;

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
            return RedirectToAction(nameof(Index));
        }

    }
}