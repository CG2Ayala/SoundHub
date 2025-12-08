using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoundHub.Web.Data;
using SoundHub.Web.Models;

namespace SoundHub.Web.Services
{
    public class PostService
    {
        private readonly ApplicationDbContext _db;
        private readonly ISpotifyService _spotify;

        public PostService(ApplicationDbContext db, ISpotifyService spotify)
        {
            _db = db;
            _spotify = spotify;
        }

        public IQueryable<Post> GetFeed()
        {
            return _db.Posts
                .AsNoTracking()
                .OrderByDescending(p => p.CreatedAt);
        }

        public IQueryable<Post> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return GetFeed();

            query = query.ToLowerInvariant();
            return _db.Posts
                .AsNoTracking()
                .Where(p =>
                    (p.Text != null && p.Text.ToLower().Contains(query)) ||
                    (p.ArtistName != null && p.ArtistName.ToLower().Contains(query))
                )
                .OrderByDescending(p => p.CreatedAt);
        }

        public async Task<Post> CreateAsync(string authorId, string text, string artistName, string imageUrl)
        {
            string artistId = null;
            if (!string.IsNullOrWhiteSpace(artistName))
            {
                var artist = await _spotify.SearchArtistAsync(artistName);
                artistId = artist?.Id;
            }

            var post = new Post
            {
                AuthorId = authorId,
                Text = text,
                ArtistSpotifyId = artistId,
                ArtistName = artistName,
                ImageUrl = imageUrl ?? string.Empty // <- aquí el cambio
            };


            _db.Posts.Add(post);
            await _db.SaveChangesAsync();
            return post;
        }
    }
}

