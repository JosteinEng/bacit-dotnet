using System.Diagnostics;
using bacit_dotnet.MVC.Interfaces;
using bacit_dotnet.MVC.Models;
using bacit_dotnet.MVC.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bacit_dotnet.MVC.Controllers
{
    // This is the controller class for the Home page. The controller is the C in MVC
    // The method in the controller class are used for displaying the index view with suggestions and jusdoits fetched from the Db.
    // The class uses dependency injections of the suggestion -and justdoitRepository to fetch Db data.
    
    // Keyword Authorize uses Microsoft's authorization system for checking if a user should have access to
    // the page and it's functions. 
    [Authorize]
    public class HomeController : Controller
    {
        // Field variables - dependency injections
        private readonly ISuggestionRepository _suggestionRepository;
        private readonly IJustdoitRepository _justdoitRepository;
        public HomeController(ISuggestionRepository suggestionRepository, IJustdoitRepository justdoitRepository)
        {
            _suggestionRepository = suggestionRepository;
            _justdoitRepository = justdoitRepository;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Method returns the index view.
        // The view gets populated with suggestions through the view model.
        public IActionResult Index()
        {
            var indexViewModel = new HomeViewModel()
            {
                Suggestions = _suggestionRepository.GetAllSuggestions(),
                Justdoit = _justdoitRepository.GetAllJustdoit()
            };
            return View(indexViewModel);
        }
    }
}