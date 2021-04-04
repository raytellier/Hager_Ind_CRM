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
            GetJobPosition();
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

        private void GetJobPosition()
        {
            var jobPositions = from j in _context.Employees
                               .AsEnumerable()
                               group j by j.JobPosition into j
                               select new { Position = j.Key, Count = j.Count() };


            ViewData["JobPosition"] = jobPositions;
        }
    }
}
