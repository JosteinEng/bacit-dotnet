using bacit_dotnet.MVC.Interfaces;
using bacit_dotnet.MVC.Models;
using bacit_dotnet.MVC.Repositories;
using bacit_dotnet.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bacit_dotnet.MVC.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IUserRepository _userRepository;
        
        public TeamController(ITeamRepository teamRepository, IUserRepository userRepository)
        {
            _teamRepository = teamRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var indexViewModel = new TeamViewModel()
            {
                Teams = _teamRepository.GetAllTeamsAndUsers()
            };

            return View(indexViewModel);
        }

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateTeamViewModel objTeams)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Ugyldige verdier i skjema";
                return RedirectToAction("Create");
            }

            var newTeamId = _teamRepository.Add(new Teams
            {
                UserId = objTeams.UserId,
                TeamName = objTeams.TeamName,
            });

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
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var teamFromDb = _teamRepository.GetTeamAndUserByTeamId(id.Value);

            if (teamFromDb == null)
            {
                return NotFound();
            }

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditTeamViewModel objTeams)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Ugyldige verdier i skjema";
                return RedirectToAction("Edit", objTeams.TeamId);
            }

            var updatedTeam = new Teams
            {
                TeamId = objTeams.TeamId,
                TeamName = objTeams.TeamName,
                UserId = objTeams.UserId,
            };

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
        [HttpGet]
            public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var teamFromDb = _teamRepository.GetTeamAndUserByTeamId(id.Value);

            if (teamFromDb == null)
            {
                return NotFound();
            }

            return View(teamFromDb);
        }

        //Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTeam(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var hasRowBeenDeleted = _teamRepository.Delete(id.Value);

            if (!hasRowBeenDeleted)
            {
                TempData["error"] = "Team ble ikke slettet";
                return NotFound(); // TODO: Make 404 page
            }

            TempData["success"] = "Team har blitt slettet";

            return RedirectToAction("Index");
        }
    }
}