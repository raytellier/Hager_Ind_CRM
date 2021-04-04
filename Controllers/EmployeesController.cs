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
using Microsoft.AspNetCore.Http.Features;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Windows;

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
            if (User.HasClaim(CustomClaimTypes.Permission, Employees.Privacy))
            {
                return View("DetailsPrivacy", employee);
            }
            else {
                return View("Details", employee);
            }
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
            employee.Country = await _context.Countries.FindAsync(employee.BillingCountryID);
            employee.Province = await _context.Provinces.FindAsync(employee.BillingProvinceID);
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
            if((employee?.BillingProvinceID).HasValue)
            {
                
                ViewData["BillingCountryID"] = CountriesSelectList(employee?.BillingCountryID);
                ViewData["BillingProvinceID"] = ProvincesSelectList(employee?.BillingCountryID, employee?.BillingProvinceID);
            }
            else
            {
                ViewData["BillingCountryID"] = CountriesSelectList(null);
                ViewData["BillingProvinceID"] = ProvincesSelectList(null, null);
            }
            ViewData["EmploymentTypeID"] = EmployementTypesSelectList(employee?.EmploymentTypeID);
            ViewData["JobPositionID"] = JobPositionsSelectList(employee?.JobPositionID);
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




        //------------------------------Upload Excel File Code-----------------------------------
        //String To Bool Method
        private bool StringToBool(string str)
        {
            if (str == "1" || str.ToLower() == "true") { return true; } else { return false; }
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

        //Decimal Input Checker
        private Decimal? DecimalValid(string str)
        {
            if (!string.IsNullOrEmpty(str)) { return Decimal.Parse(str); } else { return null; }
        }

        //Decimal Input Checker
        private String? StringEmpty(string str)
        {
            if (!string.IsNullOrEmpty(str)) { return str; } else { return null; }
        }



        //Action for Employee file upload-------------------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> InsertFromExcel(IFormFile theExcel, bool replace)
        {
            if (theExcel is null)
            {
                throw new ArgumentNullException(nameof(theExcel));
            }

            try

                {

                if (replace == true)
                {
                    MessageBoxResult result;
                    result = MessageBox.Show("Are you sure you want to delete all current employee data and replace it with the new employee data?", "Replace All Data?",
                        MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.No)
                    {
                        return RedirectToAction("Index", "Employees");
                    }
                }

                ExcelPackage excel;
                using (var memoryStream = new MemoryStream())
                {
                    await theExcel.CopyToAsync(memoryStream);
                    excel = new ExcelPackage(memoryStream);
                }
                var workSheet = excel.Workbook.Worksheets[0];
                var start = workSheet.Dimension.Start;
                var end = workSheet.Dimension.End;

                //Get all current employe names
                var ENames = (from d in _context.Employees
                                  orderby d.FirstName
                                  select d.FirstName + " " + d.LastName).ToList();

                    //Start a new list to hold imported objects
                    List<Employee> newEmployees = new List<Employee>();

                    for (int row = 2; row <= end.Row; row++)
                    {
                        if (replace == false && ENames.Contains(workSheet.Cells[row, 1].Text + " " + workSheet.Cells[row, 2].Text))
                        {
                            continue;
                        }

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
                        DateTime? startDate = DateValidNullable(workSheet.Cells[row, 17].Text);
                        DateTime? InactiveDate = DateValidNullable(workSheet.Cells[row, 18].Text);

                        //Decimals
                        Decimal? wageDec = DecimalValid(workSheet.Cells[row, 7].Text);
                        Decimal? expenseDec = DecimalValid(workSheet.Cells[row, 8].Text);


                    //Strings
                    String? emailStr = StringEmpty(workSheet.Cells[row, 3].Text);




                        // Row by row...
                    Employee a = new Employee
                        {
                            FirstName = workSheet.Cells[row, 1].Text,
                            LastName = workSheet.Cells[row, 2].Text,
                            Email = emailStr,
                            HomePhone = homePhoneInt,
                            CellPhone = cellPhoneInt,
                            DateOfBirth = DOB,
                            Wage = wageDec,
                            Expense = expenseDec,
                            Address1 = StringEmpty(workSheet.Cells[row, 9].Text),
                            Address2 = StringEmpty(workSheet.Cells[row, 10].Text),
                            City = StringEmpty(workSheet.Cells[row, 11].Text),
                            BillingPostal = StringEmpty(workSheet.Cells[row, 12].Text),
                            BillingProvinceID = provinceID,
                            BillingCountryID = countryID,
                            JobPositionID = jobPosID,
                            EmploymentTypeID = empTypeID,
                            DateJoined = startDate,
                            InactiveDate = InactiveDate,
                            IsUser = isUserBool,
                            Active = activeBool,
                            PermissionLevel = StringEmpty(workSheet.Cells[row, 21].Text),
                            EmergencyContactName = StringEmpty(workSheet.Cells[row, 22].Text),
                            EmergencyContactPhone = emergPhoneInt,
                            KeyFobNumber = keyFobInt
                        };
                        newEmployees.Add(a);
                    }
                    if(replace == true)
                    {
                        _context.Database.ExecuteSqlRaw("DELETE FROM Employees");
                    }
                    _context.Employees.AddRange(newEmployees);
                    _context.SaveChanges();

                }
                catch (Exception ex)
                {
                    if (ex.GetBaseException().Message.Contains(""))
                    {
                        ModelState.AddModelError("Error", "No File Selected or Unknown data in the Excel File");
                    }

                }
            
            return RedirectToAction("Index", "Employees");
        }



        //------------------------------Download List to Excel File------------------------------
        
        public IActionResult DEmployees()
        {
            //Get the Employees
            var emp = from a in _context.Employees
                          .Include(a => a.JobPosition)
                          .Include(a => a.EmploymentType)
                          .Include(a => a.Province)
                          .Include(a => a.Country)
                      orderby a.FirstName descending
                      select new
                      {
                          a.FirstName,
                          a.LastName,
                          a.Email,
                          HomePhone = String.Format("{0:(###) ###-####}", a.HomePhone),
                          CellPhone = String.Format("{0:(###) ###-####}", a.CellPhone),
                          DateOfBirth = String.Format("{0:yyyy-MM-dd}", a.DateOfBirth),
                          Wage = a.Wage,
                          Expense = a.Expense,
                          a.Address1,
                          a.Address2,
                          a.City,
                          a.BillingPostal,
                          Province = a.Province.Name,
                          Country = a.Country.Name,
                          JobPosition = a.JobPosition.Name,
                          EmploymentType = a.EmploymentType.Type,
                          DateJoined = String.Format("{0:yyyy-MM-dd}", a.DateJoined),
                          InactiveDate = String.Format("{0:yyyy-MM-dd}", a.InactiveDate),
                          IsUser = a.IsUser,
                          Active = a.Active,
                          a.PermissionLevel,
                          a.EmergencyContactName,
                          EmergencyContactPhone = String.Format("{0:(###) ###-####}", a.EmergencyContactPhone),
                          KeyFobNumber = String.Format("{0:####:#####}", a.KeyFobNumber)
                      };
            //How many rows?
            int numRows = emp.Count();
            
            if (numRows > 0) //There are employees
            {

                //New Spreadsheet
                using ExcelPackage excel = new ExcelPackage();
                var workSheet = excel.Workbook.Worksheets.Add("Employees");

                //Note: Cells[row, column]
                workSheet.Cells[1, 1].LoadFromCollection(emp, true);

                //Style first column for dates
                workSheet.Column(1).Style.Font.Bold = true;

                //Set the names for the headings
                string[] colNames = {"First Name", "Last Name", "Email", "Home Phone", "Cell Phone", "Date Of Birth", "Wage",
                    "Expense", "Address 1", "Address 2", "City", "Postal Code", "Province/State", "Country", "Job Title", "Employment Type",
                    "Start Date", "Inactive Date", "Is User", "Is Active", "Permission Level", "Emergency Contact Name", "Emergency Contact Phone", "Key Cards"};
                for (int i = 1; i < 24; i++)
                {
                    workSheet.Cells[1, i].Value = colNames[i-1];
                } 


                //Set Style and backgound colour of headings
                using (ExcelRange headings = workSheet.Cells[1, 1, 1, 24])
                {
                    headings.Style.Font.Bold = true;
                    var fill = headings.Style.Fill;
                    fill.PatternType = ExcelFillStyle.Solid;
                    fill.BackgroundColor.SetColor(Color.LightGray);
                }

                //Autofit columns
                workSheet.Cells.AutoFitColumns();

                //Grab the date as a string variable
                DateTime utcDate = DateTime.UtcNow;
                TimeZoneInfo esTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                DateTime localDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, esTimeZone);
                string DlDate = localDate.ToShortDateString();

                //Time To Download!
                string EFileName = "Employees List - " + DlDate + ".xlsx";
                var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (syncIOFeature != null)
                {
                    syncIOFeature.AllowSynchronousIO = true;
                    using var memoryStream = new MemoryStream();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.Headers["content-disposition"] = "attachment;  filename=" + EFileName;
                    excel.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.Body);
                }
                else
                {
                    try
                    {
                        Byte[] theData = excel.GetAsByteArray();
                        string filename = EFileName;
                        string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        return File(theData, mimeType, filename);
                    }
                    catch (Exception)
                    {
                        return NotFound();
                    }
                }
            }
            return NotFound();
        }
    }
}
