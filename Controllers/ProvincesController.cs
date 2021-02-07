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

namespace Hager_Ind_CRM.Controllers
{
    [Authorize(Policy = PolicyTypes.Lists.Manage)]
    public class ProvincesController : Controller
    {
        private readonly HagerIndContext _context;

        public ProvincesController(HagerIndContext context)
        {
            _context = context;
        }

        // GET: Provinces
        public async Task<IActionResult> Index()
        {
            return View(await _context.Provinces.Include(p => p.Country).OrderBy(p => p.OrderID).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(int[] reorderedId)
        {
            int preference = 1;
            foreach (int id in reorderedId)
            {
                var record = _context.Provinces.Find(id);
                record.OrderID = preference;
                preference += 1;
                _context.SaveChanges();
                
            }
            return View(await _context.Provinces.Include(p => p.Country).OrderBy(p => p.OrderID).ToListAsync());
        }


        // GET: Provinces/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var province = await _context.Provinces

        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (province == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(province);
        //}

        // GET: Provinces/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: Provinces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,OrderID,CountryID")] Province province)
        {
            if (ModelState.IsValid)
            {
                province.OrderID = _context.Provinces.Count() + 1;
                _context.Add(province);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDropDownLists(province);
            return View(province);
        }

        // GET: Provinces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var province = await _context.Provinces.FindAsync(id);
            if (province == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(province);
            return View(province);
        }

        // POST: Provinces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,OrderID,CountryID")] Province province)
        {
            if (id != province.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(province);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvinceExists(province.ID))
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
            PopulateDropDownLists(province);
            return View(province);
        }

        // GET: Provinces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var province = await _context.Provinces
                .Include(m => m.Country)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (province == null)
            {
                return NotFound();
            }

            return View(province);
        }

        // POST: Provinces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var province = await _context.Provinces.FindAsync(id);
            _context.Provinces.Remove(province);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private void PopulateDropDownLists(Province province = null)
        {
            var dQuery = from d in _context.Countries
                         orderby d.Name
                         select d;
            ViewData["CountryID"] = new SelectList(dQuery, "ID", "Name", province?.CountryID);
        }
        private bool ProvinceExists(int id)
        {
            return _context.Provinces.Any(e => e.ID == id);
        }
    }
}
