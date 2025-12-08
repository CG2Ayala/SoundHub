using Microsoft.AspNetCore.Mvc;
using SoundHub.Web.Data;
using SoundHub.Web.Models;

namespace SoundHub.Web.Controllers
{
    public class CommunityController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CommunityController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var list = _db.Communities
                          .OrderByDescending(c => c.CreatedAt)
                          .ToList();
            return View(list);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Community model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.CreatedAt = DateTime.UtcNow;
            model.OwnerId = "anonymous"; // o el user logueado cuando uses Identity

            _db.Communities.Add(model);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }



    }
}




