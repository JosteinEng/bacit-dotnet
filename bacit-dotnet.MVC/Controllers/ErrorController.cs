using System.Diagnostics;
using bacit_dotnet.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace bacit_dotnet.MVC.Controllers
{
    public class ErrorController : Controller
    {
        //GET ERROR
        //Method redirects the user to the PageNotFound view when called for.
        public IActionResult PageNotfound()
        {
            return View();
        }
    }
}