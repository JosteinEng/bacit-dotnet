using bacit_dotnet.MVC.Interfaces;
using bacit_dotnet.MVC.Models;
using bacit_dotnet.MVC.Repositories;
using bacit_dotnet.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bacit_dotnet.MVC.Controllers
{
    // This is the controller class for Teams. The controller is the C in MVC
    // The methods in the controller class are used for different CRUD actions related to Teams.
    // The class uses dependency injections of the team -and userRepository to use different CRUD related Db actions and methods.
    
    // Keyword Authorize uses Microsoft's authorization system for checking if a user should have access to
    // the page and it's functions.
	[Authorize(Roles = "Administrator")]
    public class TeamController : Controller
    {
        // Field variables - dependency injections
        private readonly ITeamRepository _teamRepository;
        private readonly IUserRepository _userRepository;
        
        public TeamController(ITeamRepository teamRepository, IUserRepository userRepository)
        {
            _teamRepository = teamRepository;
            _userRepository = userRepository;
        }

        // Method returns the index view.
        // The view gets populated with teams through the view model.
        [HttpGet]
        public IActionResult Index()
        {
            var indexViewModel = new TeamViewModel()
            {
                Teams = _teamRepository.GetAllTeamsAndUsers()
            };

            return View(indexViewModel);
        }

        // Method returns the create view.
        // The view gets populated with users through the view model.
        [HttpGet]
        public IActionResult Create()
        {
            var users = _userRepository.GetUsers();

            var createTeamViewModel = new CreateTeamViewModel
            {
                Users = users
            };

            return View(createTeamViewModel);
        }

        //Post
        // Method fires multiple actions for verifying and saving teams.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateTeamViewModel objTeams)
        {
            // ModelState checks if the values inputted in the forms are valid.
            // If ModelState is invalid, the method redirects the user back to the create view,
            // and informs the user of an error in the form.
            // This action is another layer of error prevention for a situation where an invalid input
            // would get past the Model [required] restrictions. Stopping an SQLException or Exception
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Ugyldige verdier i skjema";
                return RedirectToAction("Create");
            }

            // The inputs from the form/view model are added into a new team to be added to the database.
            var newTeamId = _teamRepository.Add(new Teams
            {
                UserId = objTeams.UserId,
                TeamName = objTeams.TeamName,
            });

            // If statements checks if the value of the new local variable is greater then 0.
            // The local variable newTeamId should contain the same int value as the int value in the db.
            // If the statement is true, the team was successfully added to the database.
            if (newTeamId > 0)
            {
                TempData["success"] = "Team har blitt opprettet!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Team ikke opprettet!";
            }

            return RedirectToAction("Create");
        }

        //Get
        // The method return the edit view with populated form field values fetched from the db.
        // The parameter id is used to fetch the correct team from the db.
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            // The if statement checks if the parameter id is empty(null) or 0
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            // _userRepository fetches the correct team from the db, based on the id value.
            var teamFromDb = _teamRepository.GetTeamAndUserByTeamId(id.Value);

            if (teamFromDb == null)
            {
                return NotFound();
            }
            
            // By using our edit view model, we populate the view with the content gathered from the db.
            // We also populate the view model with users to chose between in an input field in the form.
            var users = _userRepository.GetUsers();

            var teamAndUserGatheredFromDb = new EditTeamViewModel()
            {
                TeamId = teamFromDb.TeamId,
                TeamName = teamFromDb.TeamName,
                UserId = teamFromDb.UserId,
                Users = users
            };

            return View(teamAndUserGatheredFromDb);
        }

        //Post
        // Method fires multiple actions for verifying and updating teams.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditTeamViewModel objTeams)
        {
            // ModelState checks if the values inputted in the forms are valid.
            // If ModelState is invalid, the method redirects the user back to the create view,
            // and informs the user of an error in the form.
            // This action is another layer of error prevention for a situation where an invalid input
            // would get past the Model [required] restrictions. Stopping an SQLException or Exception 
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Ugyldige verdier i skjema";
                return RedirectToAction("Edit", objTeams.TeamId);
            }
            
            // The inputs from the form/view model are added into a new team to update the row in the db.
            var updatedTeam = new Teams
            {
                TeamId = objTeams.TeamId,
                TeamName = objTeams.TeamName,
                UserId = objTeams.UserId,
            };

            // If statements checks if the value of the new local variable is greater then 0.
            // The local variable newTeamId should contain the same int value as the int value in the db.
            // If the statement is true, the suggestions was successfully updated in the database.
            var rowsAffectedByUpdate = _teamRepository.Update(updatedTeam);
            if (rowsAffectedByUpdate > 0)
            {
                TempData["success"] = "Team har blitt oppdatert";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Team ble ikke oppdatert";
            }
            return RedirectToAction("Edit", objTeams.TeamId);
        }

        //Get
        // The method return the delete view with populated form field values fetched from the db,
        // to display what will be deleted.
        // The parameter id is used to fetch the correct suggestion from the db.
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            // The if statement checks if the parameter id is empty(null) or 0
            if (id == null || id <= 0)
            {
                return NotFound();
            }
            
            // The if statements checks if the team to be deleted is connected(foreign key) to a suggestions
            // or Justdoit in the db. If the user is trying to delete a connected team, the if statements
            // handles the actions and avoids the potential following SQLException.
            if (_teamRepository.IsTeamInUseSuggestion(id.Value))
            {
                TempData["error"] = "Kan ikke slette team. Team tilhører et forslag!";
                return RedirectToAction("Index");
            }
            
            if (_teamRepository.IsTeamInUseJustdoit(id.Value))
            {
                TempData["error"] = "Kan ikke slette team. Team tilhører et JustDoIt!";
                return RedirectToAction("Index");
            }

            // By using the team model, we populate the view with the content gathered from the db.
            var teamFromDb = _teamRepository.GetTeamAndUserByTeamId(id.Value);

            if (teamFromDb == null)
            {
                return NotFound();
            }

            return View(teamFromDb);
        }

        //Delete
        // Method fires multiple actions for verifying and deleting teams.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTeam(int? id)
        {
            // The if statement checks if the parameter id is empty(null) or 0
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            // If statements checks if the bool value of the new local variable is true or false.
            // If the statement is true, the team was successfully deleted in the database.
            var hasRowBeenDeleted = _teamRepository.Delete(id.Value);

            if (!hasRowBeenDeleted)
            {
                TempData["error"] = "Team ble ikke slettet";
                return NotFound();
            }

            TempData["success"] = "Team har blitt slettet";

            return RedirectToAction("Index");
        }
    }
}