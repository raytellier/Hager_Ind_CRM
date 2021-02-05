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
    public class EmployeesController : Controller
    {
        private readonly HagerIndContext _context;

        public EmployeesController(HagerIndContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var hagerIndContext = _context.Employees
                .Include(e => e.EmploymentType)
                .Include(e => e.JobPosition)
                .Include(e => e.Country)
                .Include(e => e.Province);
            return View(await hagerIndContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Country)
                .Include(e => e.Province)
                .Include(e => e.EmploymentType)
                .Include(e => e.JobPosition)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,JobPositionID,EmploymentTypeID,Address1,Address2,City,BillingProvinceID,BillingPostal,BillingCountryID,CellPhone,HomePhone,Email,DateOfBirth,Wage,Expense,DateJoined,InactiveDate,KeyFobNumber,Active,IsUser,PermissionLevel,EmergencyContactName,EmergencyContactPhone")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDropDownLists(employee);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(employee);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,JobPositionID,EmploymentTypeID,Address1,Address2,City,BillingProvinceID,BillingPostal,BillingCountryID,CellPhone,HomePhone,Email,DateOfBirth,Wage,Expense,DateJoined,InactiveDate,KeyFobNumber,Active,IsUser,PermissionLevel,EmergencyContactName,EmergencyContactPhone")] Employee employee)
        {
            if (id != employee.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.ID))
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
            PopulateDropDownLists(employee);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.EmploymentType)
                .Include(e => e.JobPosition)
                .Include(e => e.Country)
                .Include(e => e.Province)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.ID == id);
        }
        private void PopulateDropDownLists(Employee employee = null)
        {
            ViewData["EmploymentTypeID"] = EmployementTypesSelectList(employee?.EmploymentTypeID);
            ViewData["JobPositionID"] = JobPositionsSelectList(employee?.JobPositionID);
            ViewData["BillingCountryID"] = CountriesSelectList(employee?.BillingCountryID);
            ViewData["BillingProvinceID"] = ProvincesSelectList(employee?.BillingProvinceID);
        }
        private SelectList EmployementTypesSelectList(int? id)
        {
            var dQuery = (from d in _context.EmploymentTypes
                          orderby d.OrderID
                          select d).ToList();
            return new SelectList(dQuery, "ID", "Type", id);
        }

        private SelectList JobPositionsSelectList(int? id)
        {
            var dQuery = (from d in _context.JobPositions
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

        private SelectList ProvincesSelectList(int? id)
        {
            var dQuery = (from d in _context.Provinces
                          orderby d.OrderID
                          select d).ToList();
            return new SelectList(dQuery, "ID", "Name", id);
        }
    }
}
