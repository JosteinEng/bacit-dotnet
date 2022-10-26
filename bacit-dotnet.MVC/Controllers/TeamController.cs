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
                Teams = _teamRepository.GetAllTeams()
            };

            return View(indexViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var users = _userRepository.GetAllUsers();

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
                return View(objTeams);
            }

            var newCategoryId = _teamRepository.Add(new Teams
            {
                UserId = objTeams.UserId,
                TeamName = objTeams.TeamName
            });

            if (newCategoryId > 0)
            {
                TempData["success"] = "Team har blitt opprettet!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Team ikke opprettet!";
            }

            return RedirectToAction("Index");
        }
    }
}
