using Microsoft.AspNetCore.Mvc;
using SoundHub.Web.Services;
using System.Linq;
using System.Threading.Tasks;

namespace SoundHub.Web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly PostService _postService;
        public PostsController(PostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public IActionResult GetFeed()
        {
            var posts = _postService.GetFeed()
                .Select(p => new { p.Id, p.AuthorId, p.Text, p.CreatedAt, p.ArtistName })
                .ToList();
            return Ok(posts);
        }

        public class CreatePostDto { public string Text { get; set; } public string ArtistName { get; set; } }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostDto dto)
        {
            // For demo: using a fixed authorId for mobile client; replace with token auth later
            var authorId = dto?.Text != null ? "mobile-user" : "mobile-user";
            var post = await _postService.CreateAsync(authorId, dto.Text, dto.ArtistName, null);
            return CreatedAtAction(nameof(GetFeed), new { id = post.Id }, post);
        }
    }
}

