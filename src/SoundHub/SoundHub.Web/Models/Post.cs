using System;

namespace SoundHub.Web.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string AuthorId { get; set; } // FK to AspNetUsers (ApplicationUser.Id)
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string ArtistSpotifyId { get; set; }
        public string ArtistName { get; set; }
        public string? ImageUrl { get; set; }
        public string Status { get; set; } = "active"; // active | deleted | reported
        public int? CommunityId { get; set; }

    }
}
