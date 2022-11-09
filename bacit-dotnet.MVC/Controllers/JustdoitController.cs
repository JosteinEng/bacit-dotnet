using bacit_dotnet.MVC.Interfaces;
using bacit_dotnet.MVC.Models;
using bacit_dotnet.MVC.Repositories;
using bacit_dotnet.MVC.ViewModels;
using bacit_dotnet.MVC.ViewModels;
using bacit_dotnet.MVC.ViewModels.Justdoit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bacit_dotnet.MVC.Controllers
{
    [Authorize]
    public class JustdoitController : Controller
    {
        private readonly IJustdoitRepository _justdoitRepository;
        private readonly ITeamRepository _teamRepository;

        public JustdoitController(IJustdoitRepository useRepository, ITeamRepository teamRepository)
        {
            _justdoitRepository = useRepository;
            _teamRepository = teamRepository;
        }

        public IActionResult Index()
        {
            var indexViewModel = new JustdoitViewModel()
            {
                Justdoit = _justdoitRepository.GetAllJustdoit()
            };

            return View(indexViewModel);
        }

        //Get
        public IActionResult Create()
        {
            var teams = _teamRepository.GetAllTeamsAndUsers();

            var createSuggestionViewModel = new CreateJustdoitViewModel()
            {
                Teams = teams
            };
            
            return View(createSuggestionViewModel);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm]CreateJustdoitViewModel objJustdoit)
        {
            if (ModelState.IsValid is false)
            {
                TempData["error"] = "Verdier er ikke gyldige";
                return View(objJustdoit);
            }

            if(objJustdoit.Attachments != null && objJustdoit.Attachments.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    objJustdoit.Attachments.CopyTo(memoryStream);
                    objJustdoit.Justdoit.Attachments = memoryStream.ToArray();
                }
            }


            var newJustdoitId = _justdoitRepository.Add(objJustdoit.Justdoit);

            if (newJustdoitId > 0)
            {
                TempData["success"] = "Forslag ble opprettet";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = "Forslag ble ikke opprettet";
            }

            return View(objJustdoit);
        }

        //Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var justdoitFromDb = _justdoitRepository.GetJustdoitByJustdoitId(id.Value);

            if (justdoitFromDb == null)
            {
                return NotFound();
            }

            var teams = _teamRepository.GetAllTeamsAndUsers();

            var justdoitToEdit = new EditJustdoitViewModel()
            {
                JustdoitId = justdoitFromDb.JustdoitId,
                EmployeeId = justdoitFromDb.EmployeeId,
                Title = justdoitFromDb.Title,
                Description = justdoitFromDb.Description,
                Category = justdoitFromDb.Category,
                TeamId = justdoitFromDb.TeamId,
                Teams = teams
            };
            
            return View(justdoitToEdit);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditJustdoitViewModel objJustdoit)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Ugyldige verdier i skjem";
                return View(objJustdoit);
            }

            var updatedJustdoit = new Justdoit()
            {
                JustdoitId = objJustdoit.JustdoitId,
                EmployeeId = objJustdoit.EmployeeId,
                Title = objJustdoit.Title,
                Description = objJustdoit.Description,
                Category = objJustdoit.Category,
                TeamId = objJustdoit.TeamId
            };

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
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var justdoitFromDb = _justdoitRepository.GetJustdoitByJustdoitId(id.Value);

            if (justdoitFromDb == null)
            {
                return NotFound();
            }

            return View(justdoitFromDb);
        }

        //Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteJustdoit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var hasRowBeenDeleted = _justdoitRepository.Delete(id.Value);

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
