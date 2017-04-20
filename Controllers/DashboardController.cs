
using System.Collections.Generic;
using System.Linq;
using Dashboard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Dashboard.Controllers
{
    public class DashboardController: Controller
    {
        private DashboardContext _context;
 
        public DashboardController(DashboardContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("dashboard/admin")]
        
        public IActionResult admin()
        {
            ViewData["admin"] = "admin";
            ViewData["FirstName"] = HttpContext.Session.GetString("Name");
            List<User> myUserList = _context.User.ToList();
            ViewBag.myUserList = myUserList;
            return View("dashboard");
        }
        [HttpGet]
        [Route("dashboard")]
        
        public IActionResult normal()
        {
            if(HttpContext.Session.GetInt32("user_id") == null)
            {
                return RedirectToAction("Index","Home");
            }
            else if((int)HttpContext.Session.GetInt32("user_access_level") == 10)
            {
                return RedirectToAction("admin");
            }
            else
            {
            ViewData["admin"] = "normal";
            ViewData["FirstName"] = HttpContext.Session.GetString("Name");
            List<User> myUserList = _context.User.ToList();
            ViewBag.myUserList = myUserList;
            return View("dashboard");
            }
        }
    }
}