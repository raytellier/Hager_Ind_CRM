using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hager_Ind_CRM.Data;
using Hager_Ind_CRM.Models;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;

namespace Hager_Ind_CRM.Controllers
{
    [Authorize(Policy = PolicyTypes.Lists.Manage)]
    public class EmploymentTypesController : Controller
    {
        private readonly HagerIndContext _context;

        public EmploymentTypesController(HagerIndContext context)
        {
            _context = context;
        }

        // GET: EmploymentTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmploymentTypes.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(int[] reorderedId)
        {
            int preference = 1;
            foreach (int id in reorderedId)
            {
                var record = _context.EmploymentTypes.Find(id);
                record.OrderID = preference;
                _context.SaveChanges();
                preference += 1;
            }
            return View(await _context.EmploymentTypes.OrderBy(p => p.OrderID).ToListAsync());
        }

        // GET: EmploymentTypes/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var employmentType = await _context.EmploymentTypes
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (employmentType == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(employmentType);
        //}

        // GET: EmploymentTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmploymentTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Type,OrderID")] EmploymentType employmentType)
        {
            if (ModelState.IsValid)
            {
                employmentType.OrderID = _context.EmploymentTypes.Count() + 1;
                _context.Add(employmentType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employmentType);
        }

        // GET: EmploymentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employmentType = await _context.EmploymentTypes.FindAsync(id);
            if (employmentType == null)
            {
                return NotFound();
            }
            return View(employmentType);
        }

        // POST: EmploymentTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Type,OrderID")] EmploymentType employmentType)
        {
            if (id != employmentType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employmentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmploymentTypeExists(employmentType.ID))
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
            return View(employmentType);
        }

        // GET: EmploymentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employmentType = await _context.EmploymentTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employmentType == null)
            {
                return NotFound();
            }

            return View(employmentType);
        }

        // POST: EmploymentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employmentType = await _context.EmploymentTypes.FindAsync(id);
            _context.EmploymentTypes.Remove(employmentType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmploymentTypeExists(int id)
        {
            return _context.EmploymentTypes.Any(e => e.ID == id);
        }

        

    }
}