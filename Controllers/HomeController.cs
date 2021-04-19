using Hager_Ind_CRM.Data;
using Hager_Ind_CRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Hager_Ind_CRM.ViewModels;

namespace Hager_Ind_CRM.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly HagerIndContext _context;

        public HomeController(ILogger<HomeController> logger, HagerIndContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            GetAnnouncements();
            GetCreditChecks();
            GetMissingContactInfo();
            GetUpcomingBirthday();
            GetDataForJS();
            //GetJobPosition();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Management()
        {
            return View();
        }

        public IActionResult EditLists()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void GetAnnouncements()
        {
            var announcements = (from d in _context.Announcements
                                 orderby d.Notice
                                 select d);
            ViewData["Announcements"] = announcements;
        }

        private void GetDataForJS()
        {
            var vendors = (from d in _context.Companies
                           where d.CompanyTypes.Any(a => a.Type.Name == "Vendor")
                           select d);

            ViewData["Vendors"] = vendors;

            var customers = (from d in _context.Companies
                           where d.CompanyTypes.Any(a => a.Type.Name == "Customer")
                           select d);

            ViewData["Customers"] = customers;

            var contractors = (from d in _context.Companies
                           where d.CompanyTypes.Any(a => a.Type.Name == "Contractor")
                           select d);

            ViewData["Contractors"] = contractors;


            //Empl types
            var parttime = (from d in _context.Employees
                           where d.EmploymentType.Type == "Part-Time"
                           select d);

            ViewData["Parttime"] = parttime;

            var fulltime = (from d in _context.Employees
                            where d.EmploymentType.Type == "Full-Time"
                            select d);

            ViewData["Fulltime"] = fulltime;

            var seasonal = (from d in _context.Employees
                            where d.EmploymentType.Type == "Seasonal"
                            select d);

            ViewData["Seasonal"] = seasonal;


            var coop = (from d in _context.Employees
                            where d.EmploymentType.Type == "Co-op Student"
                            select d);

            ViewData["Coop"] = coop;

            var contract = (from d in _context.Employees
                        where d.EmploymentType.Type == "Contract"
                            select d);

            ViewData["Contract"] = contract;


        }

        private void GetCreditChecks()
        {
            var creditChecks = (from c in _context.Companies
                                orderby c.Name
                                where c.CredCheck == false
                                select c);
            ViewData["CreditChecks"] = creditChecks;
        }

        private void GetMissingContactInfo()
        {
            var contacts = (from c in _context.Contacts
                            orderby c.FirstName
                            where c.Email == null || c.CellPhone == null
                            select c);
            ViewData["MissingContactInfo"] = contacts;
        }

        private void GetUpcomingBirthday()
        {
            var upcomingBirthday = from e in _context.Employees.AsEnumerable()
                                   where e.DateOfBirth != null
                                   let today = DateTime.Today
                                   let age = today.Year - e.DateOfBirth.Value.Year
                                   let birthdayOccured = e.DateOfBirth.Value.Month < today.Month || (e.DateOfBirth.Value.Day <= today.Day && e.DateOfBirth.Value.Month == today.Month)
                                   let nextBirthday = e.DateOfBirth.Value.AddYears(age + (birthdayOccured ? 1 : 0))
                                   let birthdayDifference = nextBirthday - today
                                   orderby birthdayDifference
                                   select e;

            ViewData["UpcomingBirthday"] = upcomingBirthday.Take(5);

        }

        //private void GetJobPosition()
        //{
        //    //var jobPositions = from e in _context.Employees join j in _context.JobPositions on e.JobPositionID equals j.ID
        //    //                   group e by e.JobPosition into e
        //    //                   select new JobPositionCountVM { Position = e.Key.Name, Count = e.Count() };

        //    var jobPositions = from j in _context.JobPositions
        //                       select j;

        //    List<JobPositionCountVM> positionStats = new List<JobPositionCountVM>();

        //    foreach (var item in jobPositions)
        //    {
        //        JobPositionCountVM stats = new JobPositionCountVM
        //        {
        //            Position = item.Name,
        //            Count = item.Employees.Count()
        //        };
        //        positionStats.Add(stats);
        //    }


        //    ViewData["JobPosition"] = jobPositions;
        //}
    }
}
