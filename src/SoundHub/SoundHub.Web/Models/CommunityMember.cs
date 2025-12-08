using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoundHub.Web.Models
{
    public class CommunityMember
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CommunityId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        [ForeignKey("CommunityId")]
        public Community Community { get; set; }
    }
}

