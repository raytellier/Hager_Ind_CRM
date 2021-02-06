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
    public class SubTypesController : Controller
    {
        private readonly HagerIndContext _context;

        public SubTypesController(HagerIndContext context)
        {
            _context = context;
        }

        // GET: SubTypes
        public async Task<IActionResult> Index()
        {
            var hagerIndContext = _context.SubType.Include(s => s.Type);
            return View(await hagerIndContext.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(int[] reorderedId)
        {
            int preference = 1;
            foreach (int id in reorderedId)
            {
                var record = _context.SubType.Find(id);
                record.OrderID = preference;
                _context.SaveChanges();
                preference += 1;
            }
            return View(await _context.SubType.OrderBy(p => p.OrderID).ToListAsync());
        }


        // GET: SubTypes/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var subType = await _context.SubType
        //        .Include(s => s.Type)
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (subType == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(subType);
        //}

        // GET: SubTypes/Create
        public IActionResult Create()
        {
            ViewData["TypeID"] = new SelectList(_context.Types, "ID", "Name");
            return View();
        }

        // POST: SubTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,OrderID,TypeID")] SubType subType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeID"] = new SelectList(_context.Types, "ID", "Name", subType.TypeID);
            return View(subType);
        }

        // GET: SubTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subType = await _context.SubType.FindAsync(id);
            if (subType == null)
            {
                return NotFound();
            }
            ViewData["TypeID"] = new SelectList(_context.Types, "ID", "Name", subType.TypeID);
            return View(subType);
        }

        // POST: SubTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,OrderID,TypeID")] SubType subType)
        {
            if (id != subType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubTypeExists(subType.ID))
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
            ViewData["TypeID"] = new SelectList(_context.Types, "ID", "Name", subType.TypeID);
            return View(subType);
        }

        // GET: SubTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subType = await _context.SubType
                .Include(s => s.Type)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (subType == null)
            {
                return NotFound();
            }

            return View(subType);
        }

        // POST: SubTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subType = await _context.SubType.FindAsync(id);
            _context.SubType.Remove(subType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubTypeExists(int id)
        {
            return _context.SubType.Any(e => e.ID == id);
        }
    }
}
