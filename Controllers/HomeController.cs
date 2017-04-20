using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Models;
namespace Dashboard.Controllers
{
    public class HomeController : Controller
    {
    private DashboardContext _context;
 
    public HomeController(DashboardContext context)
    {
        _context = context;
    }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register(UserViewValidation newUser)
        {
         if(ModelState.IsValid)
            {
                List<User> userList = _context.User.ToList();
                foreach (User theUser in userList)
                {
                    if(theUser.Email == newUser.Email)
                    {
                        ViewData["UserExists"] = "User already in db";
                        return View("Index");
                    }
                }
                int App_Level = 1;
                string App_Level_Direction;
                if(userList.Count < 1)
                    {
                        App_Level = 10;
                        App_Level_Direction = "admin";
                    }
                else{
                    App_Level_Direction = "normal";
                }
                User user = new User {
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Email = newUser.Email,
                    Password = newUser.Password,
                    App_Level = App_Level,
                    Created_At = DateTime.Now
                };

                _context.Add(user);
                _context.SaveChanges();
                User aUser = _context.User
                .SingleOrDefault(u => u.Email == user.Email);
                HttpContext.Session.SetInt32("user_access_level", aUser.App_Level);
                HttpContext.Session.SetString("Name", user.FirstName);
                HttpContext.Session.SetInt32("user_id", aUser.UserId);                
                
                return RedirectToAction(App_Level_Direction,"Dashboard");
            }
            return View("Index");
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(ExistingUserViewValidation existingUser)
        {
         if(ModelState.IsValid)
            {
                User myExistingUser = _context.User.SingleOrDefault(x => x.Email == existingUser.Existing_Email);
                
                if (myExistingUser != null && myExistingUser.Password == existingUser.Existing_Password)
                {
                    string App_Level_Direction;
                    if(myExistingUser.App_Level == 10)
                    {
                        App_Level_Direction = "admin";
                    }
                    else
                    {
                        App_Level_Direction = "normal";
                    }
                HttpContext.Session.SetInt32("user_access_level", myExistingUser.App_Level);
                HttpContext.Session.SetString("Name", myExistingUser.FirstName);
                HttpContext.Session.SetInt32("user_id", myExistingUser.UserId);    
                return RedirectToAction(App_Level_Direction,"Dashboard");
                }

            }
            ViewData["EmailPasswordComboFail"] = "No Email/Password Combination Could Be Found";
            return View("Index");
        }
        [HttpGet]
        [Route("logoff")]
        public IActionResult logoff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");   
        }

        [HttpGet]
        [Route("/profile")]
        public IActionResult userprofile() 
        {
            if((HttpContext.Session.GetInt32("user_id") == null))
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("edit","Admin", new{editId = (int)HttpContext.Session.GetInt32("user_id")});
        }
    }
}
