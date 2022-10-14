using Microsoft.AspNetCore.Mvc;
using bacit_dotnet.MVC.Interfaces;
using bacit_dotnet.MVC.Models;
using bacit_dotnet.MVC.ViewModels.Users;

namespace bacit_dotnet.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var indexViewModel = new IndexViewModel
            {
                Users = _userRepository.GetAllUsers()
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
        public IActionResult Create(Users objUsers)
        {

            if (!ModelState.IsValid)
            {
                TempData["error"] = "Ugyldige verdier i skjema";
                return View(objUsers);
            }

            var newCategoryId = _userRepository.Add(objUsers);

            if (newCategoryId > 0)
            {
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Category not created!";
            }

            return View(objUsers);
        }

        //Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // # Nullable methods
            // id.HasValue()
            // id.Value

            // These are three different ways to retrive a category from the database based on the id
            var userFromDb = _userRepository.GetUserByUserId(id.Value); // PS: Best
            // var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id); // PS: 2nd Best
            // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id); // PS: SLOW

            if (userFromDb == null)
            {
                return NotFound();
            }

            return View(userFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Users objUsers)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Ugyldige verdier i skjema"; // TODO: Not displaying error, why?
                return View(objUsers);
            }

            var rowsAffectedByUpdate = _userRepository.Update(objUsers);
            if (rowsAffectedByUpdate > 0)
            {
                TempData["success"] = "Category edited successfully";

                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Category was not updated!";

            }

            return View(objUsers);
        }

        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // These are three different ways to retrive a category from the database based on the id
            var userFromDb = _userRepository.GetUserByUserId(id.Value);
            // var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (userFromDb == null)
            {
                return NotFound();
            }

            return View(userFromDb);

        }

        //Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var hasRowBeenDeleted = _userRepository.Delete(id.Value);

            if (!hasRowBeenDeleted)
            {
                TempData["error"] = "Category not deleted";
                return NotFound(); // TODO: Make 404 page
            }

            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index");
        }
    }
}