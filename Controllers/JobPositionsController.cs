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
    public class JobPositionsController : Controller
    {
        private readonly HagerIndContext _context;

        public JobPositionsController(HagerIndContext context)
        {
            _context = context;
        }

        // GET: JobPositions
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobPositions.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(int[] reorderedId)
        {
            int preference = 1;
            foreach (int id in reorderedId)
            {
                var record = _context.JobPositions.Find(id);
                record.OrderID = preference;
                _context.SaveChanges();
                preference += 1;
            }
            return View(await _context.JobPositions.OrderBy(p => p.OrderID).ToListAsync());
        }

        // GET: JobPositions/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var jobPosition = await _context.JobPositions
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (jobPosition == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(jobPosition);
        //}

        // GET: JobPositions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobPositions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,OrderID")] JobPosition jobPosition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobPosition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobPosition);
        }

        // GET: JobPositions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPosition = await _context.JobPositions.FindAsync(id);
            if (jobPosition == null)
            {
                return NotFound();
            }
            return View(jobPosition);
        }

        // POST: JobPositions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,OrderID")] JobPosition jobPosition)
        {
            if (id != jobPosition.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobPosition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobPositionExists(jobPosition.ID))
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
            return View(jobPosition);
        }

        // GET: JobPositions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPosition = await _context.JobPositions
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jobPosition == null)
            {
                return NotFound();
            }

            return View(jobPosition);
        }

        // POST: JobPositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobPosition = await _context.JobPositions.FindAsync(id);
            _context.JobPositions.Remove(jobPosition);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobPositionExists(int id)
        {
            return _context.JobPositions.Any(e => e.ID == id);
        }
    }
}
