using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Models;
namespace Dashboard.Controllers
{
    public class AdminController : Controller
    {
    private DashboardContext _context;
 
    public AdminController(DashboardContext context)
        {
            _context = context;
        }
    [HttpGet]
    [Route("dashboard/users/edit/{editId}")]
    public IActionResult edit(int editId)
        {
            User singleUser = _context.User
            .SingleOrDefault(user => user.UserId == editId);
            ViewBag.singleUser = singleUser;
            if(HttpContext.Session.GetInt32("user_access_level") == 10)
            {
                ViewData["Allow"] = true;
            }
            else{
                 ViewData["Allow"] = false;
            }
            ViewData["success"] = TempData["Success"];
            return View();
        }
    [HttpPost]
    [Route("editdescription")]
    public IActionResult editdescription(string myTextArea, int incId)
        {
            User myEditedUser = _context.User
            .SingleOrDefault(user => user.UserId == incId);
            myEditedUser.Description = myTextArea;
            _context.SaveChanges();
            TempData["Success"] = "Successfully Updated Description";
            return RedirectToAction("edit", new{editId = incId});
        }
    [HttpPost]
    [Route("editpassword")]
    public IActionResult editpassword(string password, string confirmpassword, int incId)
        {
            if(password != null && confirmpassword != null)
            {
                if(password == confirmpassword && password.Length >=8)
                {
                        User myEditedUser = _context.User
                        .SingleOrDefault(user => user.UserId == incId);
                        myEditedUser.Password = password;
                        _context.SaveChanges();
                        TempData["Success"] = "Successfully Updated Password";
                            return RedirectToAction("edit", new{editId = incId});

                }
            }

            TempData["Success"] = "Could not Update Password, Check Requirements";
                return RedirectToAction("edit", new{editId = incId});
        
        }
    [HttpPost]
    [Route("editinformation/{id}")]
    public IActionResult editinformation(string incEmail, string incFirstName, string incLastName, int incId, int id, string Allowance)
        {
            
            if(incEmail.Length > 1 && incFirstName.Length > 2 && incLastName.Length > 2)
            {
                User myEditedUser = _context.User
                .SingleOrDefault(user => user.UserId == incId);
                myEditedUser.Email = incEmail;
                myEditedUser.FirstName = incFirstName;
                myEditedUser.LastName = incLastName;
                TempData["Success"] = "UpdatedUserInfo was successful";
                if(Allowance != null && Allowance == "Normal")
                {
                    myEditedUser.App_Level = 1;
                }
                else if(Allowance != null && Allowance == "Admin")
                {
                    myEditedUser.App_Level = 10;
                }

                _context.SaveChanges();
            return RedirectToAction("edit", new{editId = incId});
            }
            else
            {
                TempData["Success"] = "Updating User Info was not a Success";
                return RedirectToAction("edit", new{editId = incId});
            }
        }
    [HttpGet]
    [Route("users/new")]
    public IActionResult adduser()
        {
            ViewData["GotStatus"] =  TempData["UserStatus"];
            return View();
        }
    [HttpPost]
    [Route("users/new")]
        public IActionResult updatedadduser(UserViewValidation checkUser)
        {
            if(ModelState.IsValid)
            {
                                List<User> userList = _context.User.ToList();
                foreach (User theUser in userList)
                {
                    if(theUser.Email == checkUser.Email)
                    {
                        TempData["UserStatus"] = "User already in db";
                        return RedirectToAction("adduser");
                    }
                }
                User newUser = new User{
                    FirstName = checkUser.FirstName,
                    LastName = checkUser.LastName,
                    Email = checkUser.Email,
                    Password = checkUser.Password,
                    Created_At = DateTime.Now,
                    App_Level = 1
                };

                _context.Add(newUser);
                _context.SaveChanges();
                
                TempData["UserStatus"] = "User successfully added to db";
                            return RedirectToAction("adduser");
            }
            else
            {
                 ViewData["GotStatus"] = "Fix Errors and resubmit";
                return View("adduser");
            }
            
        }
        [HttpGet] 
        [Route("remove/{user_id}")]
        public IActionResult remove(int user_id)
        {
            User removeUser = _context.User.SingleOrDefault(auser => auser.UserId == user_id);
            _context.Remove(removeUser);
            _context.SaveChanges();
            return RedirectToAction("admin","Dashboard");
        }
    }
}
