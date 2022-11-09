using System.Diagnostics;
using bacit_dotnet.MVC.Interfaces;
using bacit_dotnet.MVC.Models;
using bacit_dotnet.MVC.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace bacit_dotnet.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISuggestionRepository _suggestion;
        private readonly IJustdoitRepository _justdoit;
	    private readonly IUserRepository userRepository;
        public HomeController(ILogger<HomeController> logger, ISuggestionRepository suggestion, IJustdoitRepository justdoit)
        {
            _logger = logger;
            _suggestion = suggestion;
            _justdoit = justdoit;
	    this.userRepository = userRepository;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            var indexViewModel = new HomeViewModel()
            {
                Suggestions = _suggestion.GetAllSuggestions(),
                Justdoit = _justdoit.GetAllJustdoit()
            };
            return View(indexViewModel);
        }
    }
}