using bacit_dotnet.MVC.Interfaces;
using bacit_dotnet.MVC.Models;
using bacit_dotnet.MVC.Repositories;
using bacit_dotnet.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bacit_dotnet.MVC.Controllers
{
    
    // This is the controller class for Suggestions. The controller is the C in MVC
    // The methods in the controller class are used for different CRUD actions related to Suggestions.
    // The class uses dependency injections of the suggestion, team -and userRepository to use different CRUD related Db actions and methods.

    // Keyword Authorize uses Microsoft's authorization system for checking if a user should have access to
    // the page and it's functions. 
    [Authorize]
    public class SuggestionController : Controller
    {
        // Field variables - dependency injections
        private readonly ISuggestionRepository _suggestionRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IUserRepository _userRepository;

        public SuggestionController(ISuggestionRepository suggestionRepository, ITeamRepository teamRepository, IUserRepository userRepository)
        {
            _suggestionRepository = suggestionRepository;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
        }

        // Method returns the index view.
        // The view gets populated with suggestions through the view model.
        public IActionResult Index()
        {
            var indexViewModel = new SuggestionsViewModel()
            {
                Suggestions = _suggestionRepository.GetAllSuggestions()
            };

            return View(indexViewModel);
        }

        //Get
        // Method returns the create view.
        // The view gets populated with teams and users through the view model.
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
        // Method fires multiple actions for verifying and saving suggestions.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] CreateSuggestionViewModel objSuggestions)
        {
            
            // ModelState checks if the values inputted in the forms are valid.
            // If ModelState is invalid, the method redirects the user back to the create view,
            // and informs the user of an error in the form.
            // This action is another layer of error prevention for a situation where an invalid input
            // would get past the Model [required] restrictions. Stopping an SQLException or Exception 
            
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Verdier er ikke gyldige";
                return RedirectToAction("Create");
            }
            
            // Actions related to img uploading.
            // Using MemoryStream we convert the uploaded img into a byte-stream and copy the stream into a
            // byte-array saved in a local variable.
            
            // The if statement checks if the form inputs contain a file or not and acts accordingly.
            
            var uploadedAttachment = Array.Empty<byte>();
            
            if (objSuggestions.Attachments != null && objSuggestions.Attachments.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    objSuggestions.Attachments.CopyTo(memoryStream);
                    uploadedAttachment  = memoryStream.ToArray();
                }
            }
            
            var uploadedAttachmentsAfter = Array.Empty<byte>();
            
            if (objSuggestions.AttchmentsAfter != null && objSuggestions.AttchmentsAfter.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    objSuggestions.AttchmentsAfter.CopyTo(memoryStream);
                    uploadedAttachmentsAfter  = memoryStream.ToArray();
                }
            }
            
            // The inputs from the form/view model are added into a new suggestion to be added to the database.

            var newSuggestionId = _suggestionRepository.Add(new Suggestions
            {
                EmployeeId = objSuggestions.EmployeeId,
                Title = objSuggestions.Title,
                Description = objSuggestions.Description,
                Deadline = (DateTime)objSuggestions.Deadline,
                Status = objSuggestions.Status,
                Phase = objSuggestions.Phase,
                Category = objSuggestions.Category,
                TeamId = objSuggestions.TeamId,
                UserId = objSuggestions.UserId,
				
                Attachments = uploadedAttachment,
                AttachmentsAfter = uploadedAttachmentsAfter
            });

            // If statements checks if the value of the new local variable is greater then 0.
            // If the statement is true, the suggestions was successfully added to the database.
            // The local variable newSuggestionId should contain the same int value as the int value in the db.
            
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
        // The method return the edit view with populated form field values fetched from the db.
        // The parameter id is used to fetch the correct suggestion from the db.
        public IActionResult Edit(int? id)
        {
            // The if statement checks if the parameter id is empty(null) or 0
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            // _suggestionRepository fetches the correct suggestion from the db, based on the id value.
            var suggestionFromDb = _suggestionRepository.GetSuggestionBySuggestionId(id.Value);

            if (suggestionFromDb == null)
            {
                return NotFound();
            }

            // By using our edit view model, we populate the view with the content gathered from the db.
            // We also populate the view model with users and teams to chose between in different input fields in the form.
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
        // Method fires multiple actions for verifying and updating suggestions.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm]EditSuggestionViewModel objSuggestions)
        {
            
            // ModelState checks if the values inputted in the forms are valid.
            // If ModelState is invalid, the method redirects the user back to the create view,
            // and informs the user of an error in the form.
            // This action is another layer of error prevention for a situation where an invalid input
            // would get past the Model [required] restrictions. Stopping an SQLException or Exception 
            
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Ugyldige verdier i skjema";
                return View(objSuggestions);
            }

            // Actions related to img uploading.
            // Using MemoryStream we convert the uploaded img into a byte-stream and copy the stream into a
            // byte-array saved in a local variable.
            
            // The if statement checks if the form inputs contain a file or not and acts accordingly.
            var uploadedAttachment = Array.Empty<byte>();
            
            if (objSuggestions.Attachment != null && objSuggestions.Attachment.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    objSuggestions.Attachment.CopyTo(memoryStream);
                    uploadedAttachment  = memoryStream.ToArray();
                }
            }

            var uploadedAttachmentsAfter = Array.Empty<byte>();
            
            if (objSuggestions.AttchmentsAfter != null && objSuggestions.AttchmentsAfter.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    objSuggestions.AttchmentsAfter.CopyTo(memoryStream);
                    uploadedAttachmentsAfter  = memoryStream.ToArray();
                }
            }

            // The inputs from the form/view model are added into a new suggestion to update the row in the db.
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
                
                Attachments = uploadedAttachment,
                AttachmentsAfter = uploadedAttachmentsAfter
            };

            // If statements checks if the value of the new local variable is greater then 0.
            // If the statement is true, the suggestions was successfully updated in the database.
            // The local variable newSuggestionId should contain the same int value as the int value in the db.
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
        
        //Get
        // The method return the delete view with populated form field values fetched from the db, to
        // display what will be deleted.
        // The parameter id is used to fetch the correct suggestion from the db.
        public IActionResult Delete(int? id)
        {
            // The if statement checks if the parameter id is empty(null) or 0
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            // By using the suggestion model, we populate the view with the content gathered from the db.
            var suggestionFromDb = _suggestionRepository.GetSuggestionBySuggestionId(id.Value);

            if (suggestionFromDb == null)
            {
                return NotFound();
            }

            return View(suggestionFromDb);
        }

        // Delete
        // Method fires multiple actions for verifying and deleting suggestions.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSuggestion(int? id)
        {
            // The if statement checks if the parameter id is empty(null) or 0
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            // If statements checks if the bool value of the new local variable is true or false.
            // If the statement is true, the suggestions was successfully deleted in the database.
            var hasRowBeenDeleted = _suggestionRepository.Delete(id.Value);

            if (!hasRowBeenDeleted)
            {
                TempData["error"] = "Forslag ble ikke slettet";
                return NotFound(); 
            }

            TempData["success"] = "Forslag har blitt slettet";

            return RedirectToAction("Index", "Home");
        }
    }
}