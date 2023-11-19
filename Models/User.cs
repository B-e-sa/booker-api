using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Booker.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(30)]
        public string? Name { get; set; }

        [Required]
        [StringLength(255)]
        public string? Email { get; set; }

        [Required]
        // Minimum eight characters, at least one uppercase letter, 
        // one lowercase letter, one number and one special character
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"
        )]
        public string? Password { get; set; }
    }
}