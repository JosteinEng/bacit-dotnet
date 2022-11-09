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

namespace bacit_dotnet.MVC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
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
