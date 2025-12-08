using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SoundHub.Web.Models
{
    public class Community
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        public string? OwnerId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relación con miembros
        public List<CommunityMember> Members { get; set; } = new();
    }
}


