using BowlingAlleyApp.Models;
using BowlingAlleyDAL;
using BowlingAlleyDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingAlleyApp.Controllers
{
    public class HomeController : Controller
    {
        BowlingAlleyRepository repObj;

        private readonly BowlingAlleyDBContext _context;
        public HomeController(BowlingAlleyDBContext context)
        {
            _context = context;
            repObj = new BowlingAlleyRepository(_context);
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult CheckRole(IFormCollection frm)
        {
            string empname = frm["empname"];
            string empid = frm["empid"];

            byte? status = repObj.ValidateCredentials(empname, empid);

            if (status == 0)
            {
                //HttpContext.Session.SetString("empname", empname);
                return RedirectToAction("GetReservationDetails", "Admin");
            }
            //else if (roleId == 2)
            //{
            //    return Redirect("/Customer/CustomerHome?empname=" + empname);
            //}
            return View("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
