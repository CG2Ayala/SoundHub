using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoundHub.Web.Data;
using SoundHub.Web.Models;
using SoundHub.Web.Services;
using System.Threading.Tasks;
using System.Linq;

namespace SoundHub.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly PostService _postService;
        private readonly ApplicationDbContext _db;

        public PostController(PostService postService, ApplicationDbContext db)
        {
            _postService = postService;
            _db = db;
        }

        [AllowAnonymous] // Permite ver el feed a cualquiera
        public IActionResult Index()
        {
            var posts = _postService.GetFeed().ToList();
            var communities = _db.Communities
                                 .OrderByDescending(c => c.CreatedAt)
                                 .ToList();
            ViewData["Communities"] = communities;
            return View("Index", posts);

        }

        // Permitimos acceso anónimo para que el formulario sea usable sin login
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Search(string q)
        {
            var query = _postService.GetFeed();

            if (!string.IsNullOrWhiteSpace(q))
            {
                q = q.ToLower();
                query = query.Where(p =>
                    p.Text.ToLower().Contains(q) ||
                    (p.ArtistName != null && p.ArtistName.ToLower().Contains(q))
                );
            }

            var posts = query.ToList();
            var communities = _db.Communities
                                 .OrderByDescending(c => c.CreatedAt)
                                 .ToList();
            ViewData["Communities"] = communities;
            ViewData["SearchTerm"] = q;

            return View("Index", posts);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous] // permite crear a usuarios no autenticados
        public async Task<IActionResult> Create(string text, string artistName)
        {
            // Si el usuario está autenticado usamos su id; si no, usamos "anonymous"
            var userId = User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                userId = "anonymous";
            }

            var post = await _postService.CreateAsync(userId, text, artistName, null);
            return RedirectToAction(nameof(Index));
        }
    }
}



