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
        public IActionResult Create([FromForm]CreateSuggestionViewModel objSuggestions)
        {
            if (ModelState.IsValid is false)
            {
                TempData["error"] = "Verdier er ikke gyldige";
                return View(objSuggestions);
            }

            if(objSuggestions.Attachments != null && objSuggestions.Attachments.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    objSuggestions.Attachments.CopyTo(memoryStream);
                    objSuggestions.Suggestion.Attachments = memoryStream.ToArray();
                }
            }


            var newSuggestionId = _suggestionRepository.Add(objSuggestions.Suggestion);

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

        //Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var suggestionFromDb = _suggestionRepository.GetSuggestionBySuggestionId(id.Value);

            if (suggestionFromDb == null)
            {
                return NotFound();
            }

            return View(suggestionFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Suggestions objSuggestions)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Ugyldige verdier i skjem";
                return View(objSuggestions);
            }

            var rowsAffectedByUpdate = _suggestionRepository.Update(objSuggestions);
            if (rowsAffectedByUpdate > 0)
            {
                TempData["success"] = "Forslag har blitt oppdatert";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Forslag ble ikke oppdatert";
            }

            return View(objSuggestions);
        }

        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var suggestionFromDb = _suggestionRepository.GetSuggestionBySuggestionId(id.Value);

            if (suggestionFromDb == null)
            {
                return NotFound();
            }

            return View(suggestionFromDb);
        }

        //Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSuggestion(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var hasRowBeenDeleted = _suggestionRepository.Delete(id.Value);

            if (!hasRowBeenDeleted)
            {
                TempData["error"] = "Forslag ble ikke slettet";
                return NotFound(); // TODO: Make 404 page
            }

            TempData["success"] = "Forslag har blitt slettet";

            return RedirectToAction("Index");
        }
    }
}
