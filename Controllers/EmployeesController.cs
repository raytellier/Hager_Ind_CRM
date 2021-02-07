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
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.IO;
using System.Text.RegularExpressions;

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
        [Authorize(Policy = PolicyTypes.Employees.Read)]
        public async Task<IActionResult> Index()
        {
            var hagerIndContext = _context.Employees
                .Include(e => e.EmploymentType)
                .Include(e => e.JobPosition)
                .Include(e => e.Country)
                .Include(e => e.Province);
            if (User.HasClaim(CustomClaimTypes.Permission, Employees.Privacy))
            {
                return View("IndexPrivacy", await hagerIndContext.ToListAsync());
            }
            else
            {
                return View(await hagerIndContext.ToListAsync());
            }
        }

        // GET: Employees/Details/5
        [Authorize(Policy = PolicyTypes.Employees.Detail)]
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
        [Authorize(Policy = PolicyTypes.Employees.Create)]
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Policy = PolicyTypes.Employees.Create)]
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
        [Authorize(Policy = PolicyTypes.Employees.Update)]
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
        [Authorize(Policy = PolicyTypes.Employees.Update)]
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
        [Authorize(Policy = PolicyTypes.Employees.Delete)]
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
        [Authorize(Policy = PolicyTypes.Employees.Delete)]
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

        //------------------------------Upload Excel File Code-----------------------------------
        //String To Bool Method
        private bool StringToBool(string str)
        {
            if (str == "1") { return true; } else { return false; }
        }

        //Grab Numbers From a String Method
        private Int64? NumbGrab(string str)
        {
            if (!string.IsNullOrEmpty(str)) { return Convert.ToInt64(Regex.Replace(str, "[^0-9]+", string.Empty)); } else { return null; }
        }

        //Date Input Checker
        private DateTime? DateValidNullable(string str)
        {
            if (!string.IsNullOrEmpty(str)) { return DateTime.Parse(str); } else { return null; }
        }
        private DateTime DateValid(string str)
        {
            if (!string.IsNullOrEmpty(str)) { return DateTime.Parse(str); } else { return DateTime.Parse("01/01/2000"); }
        }

        //Decimal Input Checker
        private Decimal? DecimalValid(string str)
        {
            if (!string.IsNullOrEmpty(str)) { return Decimal.Parse(str); } else { return null; }
        }



        //Action for Employee file upload
        [HttpPost]
        public async Task<IActionResult> InsertFromExcel(IFormFile theExcel)
        {
            try
            {
                ExcelPackage excel;
                using (var memoryStream = new MemoryStream())
                {
                    await theExcel.CopyToAsync(memoryStream);
                    excel = new ExcelPackage(memoryStream);
                }
                var workSheet = excel.Workbook.Worksheets[0];
                var start = workSheet.Dimension.Start;
                var end = workSheet.Dimension.End;

                //Start a new list to hold imported objects
                List<Employee> newEmployees = new List<Employee>();

                for (int row = 2; row <= end.Row; row++)
                {

                    ////Get the foreign keys from the names of the positions
                    int provinceID = (await _context.Provinces
                        .FirstOrDefaultAsync(p => p.Name == workSheet.Cells[row, 13].Text)).ID;

                    int countryID = (await _context.Countries
                        .FirstOrDefaultAsync(p => p.Name == workSheet.Cells[row, 14].Text)).ID;

                    int jobPosID = (await _context.JobPositions
                        .FirstOrDefaultAsync(p => p.Name == workSheet.Cells[row, 15].Text)).ID;

                    int empTypeID = (await _context.EmploymentTypes
                        .FirstOrDefaultAsync(p => p.Type == workSheet.Cells[row, 16].Text)).ID;

                    //Bools
                    bool isUserBool = StringToBool(workSheet.Cells[row, 19].Text);
                    bool activeBool = StringToBool(workSheet.Cells[row, 20].Text);

                    //Phone Numbers
                    Int64? homePhoneInt = NumbGrab(workSheet.Cells[row, 4].Text);
                    Int64? cellPhoneInt = NumbGrab(workSheet.Cells[row, 5].Text);
                    Int64? emergPhoneInt = NumbGrab(workSheet.Cells[row, 23].Text);

                    //Key Fob Numbers
                    Int64? keyFobInt = NumbGrab(workSheet.Cells[row, 24].Text);

                    //Dates
                    DateTime? DOB = DateValidNullable(workSheet.Cells[row, 6].Text);
                    DateTime startDate = DateValid(workSheet.Cells[row, 17].Text);
                    DateTime? InactiveDate = DateValidNullable(workSheet.Cells[row, 18].Text);

                    //Decimals
                    Decimal? wageDec = DecimalValid(workSheet.Cells[row, 7].Text);
                    Decimal? expenseDec = DecimalValid(workSheet.Cells[row, 8].Text);

                    // Row by row...
                    Employee a = new Employee
                    {
                        FirstName = workSheet.Cells[row, 1].Text,
                        LastName = workSheet.Cells[row, 2].Text,
                        Email = workSheet.Cells[row, 3].Text,
                        HomePhone = homePhoneInt,
                        CellPhone = cellPhoneInt,
                        DateOfBirth = DOB,
                        Wage = wageDec,
                        Expense = expenseDec,
                        Address1 = workSheet.Cells[row, 9].Text,
                        Address2 = workSheet.Cells[row, 10].Text,
                        City = workSheet.Cells[row, 11].Text,
                        BillingPostal = workSheet.Cells[row, 12].Text,
                        BillingProvinceID = provinceID,
                        BillingCountryID = countryID,
                        JobPositionID = jobPosID,
                        EmploymentTypeID = empTypeID,
                        DateJoined = startDate,
                        InactiveDate = InactiveDate,
                        IsUser = isUserBool,
                        Active = activeBool,
                        PermissionLevel = workSheet.Cells[row, 21].Text,
                        EmergencyContactName = workSheet.Cells[row, 22].Text,
                        EmergencyContactPhone = emergPhoneInt,
                        KeyFobNumber = keyFobInt
                    };
                    newEmployees.Add(a);
                }
                _context.Database.ExecuteSqlRaw("DELETE FROM Employees");
                _context.Employees.AddRange(newEmployees);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                if (ex.GetBaseException().Message.Contains(""))
                {
                    ModelState.AddModelError("", "No File Selected or Unknown data in the Excel File");
                }

            }
            return RedirectToAction("Index", "Employees");
        }
    }
}
