using bacit_dotnet.MVC.Interfaces;
using bacit_dotnet.MVC.Models;
using bacit_dotnet.MVC.Repositories;
using bacit_dotnet.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bacit_dotnet.MVC.Controllers
{
    [Authorize]
    public class SuggestionController : Controller
    {
        private readonly ISuggestionRepository _suggestionRepository;
        private readonly ITeamRepository _teamRepository;

        public SuggestionController(ISuggestionRepository useRepository, ITeamRepository teamRepository)
        {
            _suggestionRepository = useRepository;
            _teamRepository = teamRepository;
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
            var teams = _teamRepository.GetAllTeamsAndUsers();

            var createSuggestionViewModel = new CreateSuggestionViewModel
            {
                Teams = teams
            };
            
            return View(createSuggestionViewModel);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] CreateSuggestionViewModel objSuggestions)
        {
            if (ModelState.IsValid is false)
            {
                TempData["error"] = "Verdier er ikke gyldige";
                return View(objSuggestions);
            }

            if (objSuggestions.Attachments != null && objSuggestions.Attachments.Length > 0)
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
                return RedirectToAction("Index", "Home");
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

            var teams = _teamRepository.GetAllTeamsAndUsers();

            var suggestionToEdit = new EditSuggestionViewModel
            {
                SuggestionId = suggestionFromDb.SuggestionId,
                EmployeeId = suggestionFromDb.EmployeeId,
                Title = suggestionFromDb.Title,
                Description = suggestionFromDb.Description,
                Deadline = suggestionFromDb.Deadline,
                Status = suggestionFromDb.Status,
                Category = suggestionFromDb.Category,
                TeamId = suggestionFromDb.TeamId,
                Teams = teams
            };

            return View(suggestionToEdit);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditSuggestionViewModel objSuggestions)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Ugyldige verdier i skjema";
                return View(objSuggestions);
            }

            var updatedSuggestion = new Suggestions
            {
                SuggestionId = objSuggestions.SuggestionId,
                EmployeeId = objSuggestions.EmployeeId,
                Title = objSuggestions.Title,
                Description = objSuggestions.Description,
                Deadline = objSuggestions.Deadline,
                Status = objSuggestions.Status,
                Category = objSuggestions.Category,
                TeamId = objSuggestions.TeamId
            };

            var rowsAffectedByUpdate = _suggestionRepository.Update(updatedSuggestion);
            if (rowsAffectedByUpdate > 0)
            {
                TempData["success"] = "Forslag har blitt oppdatert";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = "Forslag ble ikke oppdatert";
            }

            return RedirectToAction("Edit", objSuggestions.SuggestionId);
        }

        // TODO: Sett opp if check før sletting av brukere og teams.
        
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

            return RedirectToAction("Index", "Home");
        }
    }
}