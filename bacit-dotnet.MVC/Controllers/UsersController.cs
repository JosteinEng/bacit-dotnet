using bacit_dotnet.MVC.Interfaces;
using bacit_dotnet.MVC.Models;
using bacit_dotnet.MVC.Models.Account;
using bacit_dotnet.MVC.Repositories;
using bacit_dotnet.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;
using System.Net;

//FJERNER WARNINGS!
#pragma warning disable
///////////////////////

namespace bacit_dotnet.MVC.Controllers
{
    // This is the controller class for Users. The controller is the C in MVC
    // The methods in the controller class are used for different CRUD actions related to Users.
    // The class uses dependency injections of the userRepository to use different CRUD related Db actions and methods.
    
    // Keyword Authorize uses Microsoft's authorization system for checking if a user should have access to
    // the page and it's functions. 
    //[Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        // Field variables - dependency injections
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        
        // Method returns the index view.
        // The view gets populated with users through the view model.
        [HttpGet]
        public IActionResult Index()
        {
            var model = new UserViewModel();
            model.Users = userRepository.GetUsers();

            return View(model);
        }

        [HttpPost]
        public IActionResult Save(UserViewModel model)
        {

            UserEntity newUser = new UserEntity
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                EmployeeNumber = model.EmployeeNumber,
                Role = model.Role,
                Team = model.Team,
            };
            var roles = new List<string>();
            if (model.IsAdmin)
                roles.Add("Administrator");
            userRepository.Update(newUser, roles);

            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public IActionResult Delete(string email)
        //{
        //    userRepository.Delete(email);
        //    return RedirectToAction("Index");
        //}

     
        public IActionResult Cancel()
        {
            return RedirectToAction("Index");

        }

        public IActionResult Edit(string? email)
        {
            var model = new UserViewModel();
            model.Users = userRepository.GetUsers();
            if (email != null)
            {
                var currentUser = model.Users.FirstOrDefault(x => x.Email == email);
                if (currentUser != null)
                {
                    model.EmployeeNumber = currentUser.EmployeeNumber;
                    model.Email = currentUser.Email;
                    model.FirstName = currentUser.FirstName;
                    model.LastName = currentUser.LastName;
                    model.Role = currentUser.Role;
                    model.Team = currentUser.Team;
                    model.IsAdmin = userRepository.IsAdmin(currentUser.Email);
                }
            }
            return View(model);
        }
    }
}
