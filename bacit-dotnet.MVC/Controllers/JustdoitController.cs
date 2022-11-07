using bacit_dotnet.MVC.Interfaces;
using bacit_dotnet.MVC.Models;
using bacit_dotnet.MVC.Repositories;
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

        public JustdoitController(IJustdoitRepository useRepository)
        {
            _justdoitRepository = useRepository;
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
            return View();
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

            return View(justdoitFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Justdoit objJustdoit)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Ugyldige verdier i skjem";
                return View(objJustdoit);
            }

            var rowsAffectedByUpdate = _justdoitRepository.Update(objJustdoit);
            if (rowsAffectedByUpdate > 0)
            {
                TempData["success"] = "Forslag har blitt oppdatert";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = "Forslag ble ikke oppdatert";
            }

            return View(objJustdoit);
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
