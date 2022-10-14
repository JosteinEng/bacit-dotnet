    using bacit_dotnet.MVC.Interfaces;
    using bacit_dotnet.MVC.Models;
    using bacit_dotnet.MVC.Repositories;
    using bacit_dotnet.MVC.ViewModels.Suggestions;
    using bacit_dotnet.MVC.ViewModels.Users;
    using Microsoft.AspNetCore.Mvc;

    namespace bacit_dotnet.MVC.Controllers
{
    public class SuggestionController : Controller
    {
        private readonly ISuggestionRepository _suggestionRepository;

        public SuggestionController(ISuggestionRepository useRepository)
        {
            _suggestionRepository = useRepository;
        }

        public IActionResult Index()
        {
            var indexViewModel = new SuggestionsViewModel()
            {
                Suggestions = _suggestionRepository.GetAllSuggestions()
            };

            return View(indexViewModel);
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Suggestions objSuggestions)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Test test";
                return View(objSuggestions);
            }

            var newSuggestionId = _suggestionRepository.Add(objSuggestions);

            if (newSuggestionId > 0)
            {
                TempData["success"] = "Forslag ble opprettet";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Forslag ble ikke opprettet";
            }

            return View(objSuggestions);
        }
    }
}
