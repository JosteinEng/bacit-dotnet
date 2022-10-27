using System.Diagnostics;
using bacit_dotnet.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace bacit_dotnet.MVC.Controllers
{
    public class ErrorController : Controller
    {
        //GET ERROR
        public IActionResult PageNotfound()
        {
            return View();
        }
    }
}