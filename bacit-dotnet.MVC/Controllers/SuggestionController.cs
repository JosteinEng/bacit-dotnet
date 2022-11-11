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
        private readonly IUserRepository _userRepository;

        public SuggestionController(ISuggestionRepository suggestionRepository, ITeamRepository teamRepository, IUserRepository userRepository)
        {
            _suggestionRepository = suggestionRepository;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
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
            var users = _userRepository.GetUsers();

            var createSuggestionViewModel = new CreateSuggestionViewModel
            {
                Teams = teams,
                Users = users
            };
            
            return View(createSuggestionViewModel);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] CreateSuggestionViewModel objSuggestions)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Verdier er ikke gyldige";
                return RedirectToAction("Create");
            }

            var uploadedAttachment = Array.Empty<byte>();
            
            if (objSuggestions.Attachments != null && objSuggestions.Attachments.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    objSuggestions.Attachments.CopyTo(memoryStream);
                    uploadedAttachment  = memoryStream.ToArray();
                }
            }


            var newSuggestionId = _suggestionRepository.Add(new Suggestions
            {
                EmployeeId = objSuggestions.EmployeeId.Value,
                Title = objSuggestions.Title,
                Description = objSuggestions.Description,
                Deadline = (DateTime)objSuggestions.Deadline,
                Status = objSuggestions.Status,
                Category = objSuggestions.Category,
                TeamId = objSuggestions.TeamId,
                UserId = objSuggestions.UserId,
                Attachments = uploadedAttachment
            });

            if (newSuggestionId > 0)
            {
                TempData["success"] = "Forslag ble opprettet";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = "Forslag ble ikke opprettet";
            }

            return RedirectToAction("Create");
        }

        //Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var suggestionFromDb = _suggestionRepository.GetSuggestionBySuggestionId(id.Value);

            if (suggestionFromDb == null)
            {
                return NotFound();
            }

            var teams = _teamRepository.GetAllTeamsAndUsers();
            var users = _userRepository.GetUsers();

            var suggestionToEdit = new EditSuggestionViewModel
            {
                SuggestionId = suggestionFromDb.SuggestionId,
                EmployeeId = suggestionFromDb.EmployeeId,
                Title = suggestionFromDb.Title,
                Description = suggestionFromDb.Description,
                Deadline = suggestionFromDb.Deadline,
                Status = suggestionFromDb.Status,
                Phase = suggestionFromDb.Phase,
                Category = suggestionFromDb.Category,
                TeamId = suggestionFromDb.TeamId,
                UserId = suggestionFromDb.UserId,
                Teams = teams,
                Users = users
            };

            return View(suggestionToEdit);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm]EditSuggestionViewModel objSuggestions)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Ugyldige verdier i skjema";
                return View(objSuggestions);
            }

            
            
            var uploadedAttachment = Array.Empty<byte>();
            
            if (objSuggestions.Attachment != null && objSuggestions.Attachment.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    objSuggestions.Attachment.CopyTo(memoryStream);
                    uploadedAttachment  = memoryStream.ToArray();
                }
            }
            
            var updatedSuggestion = new Suggestions
            {
                SuggestionId = objSuggestions.SuggestionId,
                EmployeeId = objSuggestions.EmployeeId,
                Title = objSuggestions.Title,
                Description = objSuggestions.Description,
                Deadline = objSuggestions.Deadline,
                Status = objSuggestions.Status,
                Phase = objSuggestions.Phase,
                Category = objSuggestions.Category,
                TeamId = objSuggestions.TeamId,
                UserId = objSuggestions.UserId,
                
                Attachments = uploadedAttachment
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
            if (id == null || id <= 0)
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
            if (id == null || id <= 0)
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