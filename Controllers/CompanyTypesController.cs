using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hager_Ind_CRM.Data;
using Hager_Ind_CRM.Models;

namespace Hager_Ind_CRM.Controllers
{
    public class CompanyTypesController : Controller
    {
        private readonly HagerIndContext _context;

        public CompanyTypesController(HagerIndContext context)
        {
            _context = context;
        }

        // GET: CompanyTypes
        public async Task<IActionResult> Index()
        {
            var hagerIndContext = _context.CompanyTypes.Include(c => c.Company).Include(c => c.Type);
            return View(await hagerIndContext.ToListAsync());
        }

        // GET: CompanyTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyType = await _context.CompanyTypes
                .Include(c => c.Company)
                .Include(c => c.Type)
                .FirstOrDefaultAsync(m => m.TypeID == id);
            if (companyType == null)
            {
                return NotFound();
            }

            return View(companyType);
        }

        // GET: CompanyTypes/Create
        public IActionResult Create()
        {
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "BillingAddress1");
            ViewData["TypeID"] = new SelectList(_context.Types, "ID", "Name");
            return View();
        }

        // POST: CompanyTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyID,TypeID")] CompanyType companyType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companyType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "BillingAddress1", companyType.CompanyID);
            ViewData["TypeID"] = new SelectList(_context.Types, "ID", "Name", companyType.TypeID);
            return View(companyType);
        }

        // GET: CompanyTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyType = await _context.CompanyTypes.FindAsync(id);
            if (companyType == null)
            {
                return NotFound();
            }
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "BillingAddress1", companyType.CompanyID);
            ViewData["TypeID"] = new SelectList(_context.Types, "ID", "Name", companyType.TypeID);
            return View(companyType);
        }

        // POST: CompanyTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompanyID,TypeID")] CompanyType companyType)
        {
            if (id != companyType.TypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyTypeExists(companyType.TypeID))
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
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "BillingAddress1", companyType.CompanyID);
            ViewData["TypeID"] = new SelectList(_context.Types, "ID", "Name", companyType.TypeID);
            return View(companyType);
        }

        // GET: CompanyTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyType = await _context.CompanyTypes
                .Include(c => c.Company)
                .Include(c => c.Type)
                .FirstOrDefaultAsync(m => m.TypeID == id);
            if (companyType == null)
            {
                return NotFound();
            }

            return View(companyType);
        }

        // POST: CompanyTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyType = await _context.CompanyTypes.FindAsync(id);
            _context.CompanyTypes.Remove(companyType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyTypeExists(int id)
        {
            return _context.CompanyTypes.Any(e => e.TypeID == id);
        }
    }
}
