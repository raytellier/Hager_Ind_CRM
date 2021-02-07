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
    public class BillingTermsController : Controller
    {
        private readonly HagerIndContext _context;

        public BillingTermsController(HagerIndContext context)
        {
            _context = context;
        }


        // GET: BillingTerms
        public async Task<IActionResult> Index()
        {
            return View(await _context.BillingTerms.OrderBy(p => p.OrderID).ToListAsync());
        }


        [HttpPost]
        public async Task<IActionResult> Index(int[] reorderedId)
        {
            int preference = 1;
            foreach (int id in reorderedId)
            {
                var record = _context.BillingTerms.Find(id);
                record.OrderID = preference;
                _context.SaveChanges();
                preference += 1;
            }
            return View(await _context.BillingTerms.OrderBy(p => p.OrderID).ToListAsync());
        }



        // GET: BillingTerms/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var billingTerms = await _context.BillingTerms
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (billingTerms == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(billingTerms);
        //}

        // GET: BillingTerms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BillingTerms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Terms")] BillingTerms billingTerms)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billingTerms);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billingTerms);
        }

        // GET: BillingTerms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billingTerms = await _context.BillingTerms.FindAsync(id);
            if (billingTerms == null)
            {
                return NotFound();
            }
            return View(billingTerms);
        }

        // POST: BillingTerms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Terms")] BillingTerms billingTerms)
        {
            if (id != billingTerms.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billingTerms);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillingTermsExists(billingTerms.ID))
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
            return View(billingTerms);
        }

        // GET: BillingTerms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billingTerms = await _context.BillingTerms
                .FirstOrDefaultAsync(m => m.ID == id);
            if (billingTerms == null)
            {
                return NotFound();
            }

            return View(billingTerms);
        }

        // POST: BillingTerms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billingTerms = await _context.BillingTerms.FindAsync(id);
            _context.BillingTerms.Remove(billingTerms);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillingTermsExists(int id)
        {
            return _context.BillingTerms.Any(e => e.ID == id);
        }
    }
}
