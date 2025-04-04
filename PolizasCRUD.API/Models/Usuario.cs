using System.ComponentModel.DataAnnotations;

namespace PolizasCRUD.API.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        
        [Required]
        public byte[] PasswordHash { get; set; }
        
        [Required]
        public byte[] PasswordSalt { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        
        public string? RefreshToken { get; set; }
        
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}