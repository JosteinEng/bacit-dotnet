    using Microsoft.AspNetCore.Mvc;

namespace bacit_dotnet.MVC.Controllers
{
    public class SuggestionController : Controller
    {
        public IActionResult Suggestion()
        {
            return View();
        }
    }
}
