using bacit_dotnet.MVC.Interfaces;
using bacit_dotnet.MVC.Models;
using bacit_dotnet.MVC.Repositories;
using bacit_dotnet.MVC.ViewModels;
using bacit_dotnet.MVC.ViewModels.Justdoit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// Removes WARNINGS!
#pragma warning disable
///////////////////////

namespace bacit_dotnet.MVC.Controllers
{
    
    // This is the controller class for JustDoIt. The controller is the C in MVC
    // The methods in the controller class are used for different CRUD actions related to JustDoIt.
    // The class uses dependency injections of the justdoit, team -and userRepository to use different CRUD related Db actions and methods.
    
    // Keyword Authorize uses Microsoft's authorization system for checking if a user should have access to
    // the page and it's functions. 
    [Authorize]
    public class JustdoitController : Controller
    {
        // Field variables - dependency injections
        private readonly IJustdoitRepository _justdoitRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IUserRepository _userRepository;

        public JustdoitController(IJustdoitRepository useRepository, ITeamRepository teamRepository, IUserRepository userRepository)
        {
            _justdoitRepository = useRepository;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
        }

        // Method returns the index view.
        // The view gets populated with justdoit suggestions through the view model.
        public IActionResult Index()
        {
            var indexViewModel = new JustdoitViewModel()
            {
                Justdoit = _justdoitRepository.GetAllJustdoit()
            };

            return View(indexViewModel);
        }

        // Get
        // Method returns the create view.
        // The view gets populated with teams and users through the view model.
        public IActionResult Create()
        {
            var teams = _teamRepository.GetAllTeamsAndUsers();
            var users = _userRepository.GetUsers()
                ;
            var createSuggestionViewModel = new CreateJustdoitViewModel()
            {
                Teams = teams,
                Users = users
            };
            
            return View(createSuggestionViewModel);
        }

        // Post
        // Method fires multiple actions for verifying and saving JustDoIt suggestions.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm]CreateJustdoitViewModel objJustdoit)
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
            
            if(objJustdoit.Attachments != null && objJustdoit.Attachments.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    objJustdoit.Attachments.CopyTo(memoryStream);
                    uploadedAttachment = memoryStream.ToArray();
                }
            }
            
            var uploadedAttachmentsAfter = Array.Empty<byte>();
            
            if (objJustdoit.AttachmentsAfter != null && objJustdoit.AttachmentsAfter.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    objJustdoit.AttachmentsAfter.CopyTo(memoryStream);
                    uploadedAttachmentsAfter  = memoryStream.ToArray();
                }
            }

            // The inputs from the form/view model are added into a new JustDoIt to be added to the database.
            
            var newJustdoitId = _justdoitRepository.Add(new Justdoit
            {
                EmployeeId = objJustdoit.EmployeeId.Value,
                Title = objJustdoit.Title,
                Description = objJustdoit.Description,
                Category = objJustdoit.Category,
                TeamId = objJustdoit.TeamId,
				
                Attachments = uploadedAttachment,
                AttachmentAfter = uploadedAttachmentsAfter
            });

            
            // If statements checks if the value of the new local variable is greater then 0.
            // If the statement is true, the justdoit suggestions was successfully added to the database.
            // The local variable newJustdoitId should contain the same int value as the int value in the db.
            
            if (newJustdoitId > 0)
            {
                TempData["success"] = "JustDoIt ble opprettet";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = "JustDoIt ble ikke opprettet";
            }

            return RedirectToAction("Create");
        }

        // Get
        // The method return the edit view with populated form field values fetched from the db.
        // The parameter id is used to fetch the correct Justdoit suggestion from the db.
        public IActionResult Edit(int? id)
        {
            // The if statement checks if the parameter id is empty(null) or 0
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            // _justdoitRepository fetches the correct justdoit suggestion from the db, based on the id value.
            var justdoitFromDb = _justdoitRepository.GetJustdoitByJustdoitId(id.Value);

            if (justdoitFromDb == null)
            {
                return NotFound();
            }

            // By using our edit view model, we populate the view with the content gathered from the db.
            // We also populate the view model with users and teams to chose between in different input fields in the form.
            var teams = _teamRepository.GetAllTeamsAndUsers();
            var users = _userRepository.GetUsers();

            var justdoitToEdit = new EditJustdoitViewModel()
            {
                JustdoitId = justdoitFromDb.JustdoitId,
                EmployeeId = justdoitFromDb.EmployeeId,
                Title = justdoitFromDb.Title,
                Description = justdoitFromDb.Description,
                Category = justdoitFromDb.Category,
                TeamId = justdoitFromDb.TeamId,
                Teams = teams,
                Users = users
            };
            
            return View(justdoitToEdit);
        }

        //Post
        // Method fires multiple actions for verifying and updating JustDoIt suggestions.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm]EditJustdoitViewModel objJustdoit)
        {
            // ModelState checks if the values inputted in the forms are valid.
            // If ModelState is invalid, the method redirects the user back to the create view,
            // and informs the user of an error in the form.
            // This action is another layer of error prevention for a situation where an invalid input
            // would get past the Model [required] restrictions. Stopping an SQLException or Exception 
            
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Ugyldige verdier i skjem";
                return View(objJustdoit);
            }
            
            // Actions related to img uploading.
            // Using MemoryStream we convert the uploaded img into a byte-stream and copy the stream into a
            // byte-array saved in a local variable.
            
            // The if statement checks if the form inputs contain a file or not and acts accordingly.
            var uploadedAttachment = Array.Empty<byte>();
            
            if (objJustdoit.Attachment != null && objJustdoit.Attachment.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    objJustdoit.Attachment.CopyTo(memoryStream);
                    uploadedAttachment  = memoryStream.ToArray();
                }
            }
            
            var uploadedAttachmentsAfter = Array.Empty<byte>();
            
            if (objJustdoit.AttachmentsAfter != null && objJustdoit.AttachmentsAfter.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    objJustdoit.AttachmentsAfter.CopyTo(memoryStream);
                    uploadedAttachmentsAfter  = memoryStream.ToArray();
                }
            }
            
            // The inputs from the form/view model are added into a new JustDoIt to update the row in the db.
            var updatedJustdoit = new Justdoit()
            {
                JustdoitId = objJustdoit.JustdoitId,
                EmployeeId = objJustdoit.EmployeeId,
                Title = objJustdoit.Title,
                Description = objJustdoit.Description,
                Category = objJustdoit.Category,
                TeamId = objJustdoit.TeamId,
                
                Attachments = uploadedAttachment,
                AttachmentAfter = uploadedAttachmentsAfter
            };

            // If statements checks if the value of the new local variable is greater then 0.
            // If the statement is true, the justdoit suggestions was successfully updated in the database.
            // The local variable newJustdoitId should contain the same int value as the int value in the db.
            var rowsAffectedByUpdate = _justdoitRepository.Update(updatedJustdoit);
            if (rowsAffectedByUpdate > 0)
            {
                TempData["success"] = "Forslag har blitt oppdatert";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = "Forslag ble ikke oppdatert";
            }

            return RedirectToAction("Edit", objJustdoit.JustdoitId);
        }

        //Get
        // The method return the delete view with populated form field values fetched from the db,
        // to display what will be deleted.
        // The parameter id is used to fetch the correct Justdoit suggestion from the db.
        public IActionResult Delete(int? id)
        {
            // The if statement checks if the parameter id is empty(null) or 0
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            // By using the justdoit model, we populate the view with the content gathered from the db.
            var justdoitFromDb = _justdoitRepository.GetJustdoitByJustdoitId(id.Value);

            if (justdoitFromDb == null)
            {
                return NotFound();
            }

            return View(justdoitFromDb);
        }

        // Delete
        // Method fires multiple actions for verifying and deleting JustDoIt suggestions.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteJustdoit(int? id)
        {
            // The if statement checks if the parameter id is empty(null) or 0
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            // If statements checks if the bool value of the new local variable is true or false.
            // If the statement is true, the justdoit suggestions was successfully deleted in the database.
            var hasRowBeenDeleted = _justdoitRepository.Delete(id.Value);

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
