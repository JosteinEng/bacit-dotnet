    using bacit_dotnet.MVC.Interfaces;
    using bacit_dotnet.MVC.Repositories;
    using bacit_dotnet.MVC.ViewModels.Suggestions;
    using bacit_dotnet.MVC.ViewModels.Users;
    using Microsoft.AspNetCore.Mvc;
using IndexViewModel = bacit_dotnet.MVC.ViewModels.Suggestions.IndexViewModel;

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
            var indexViewModel = new IndexViewModel()
            {
                Suggestions = _suggestionRepository.GetAllSuggestions()
            };

            return View(indexViewModel);
        }
    }
}
