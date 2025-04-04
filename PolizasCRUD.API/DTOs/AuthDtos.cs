using System;
using System.ComponentModel.DataAnnotations;

namespace PolizasCRUD.API.DTOs
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [StringLength(50, ErrorMessage = "El nombre de usuario debe tener máximo 50 caracteres")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "Email en formato inválido")]
        public string Email { get; set; }
    }
    
    public class LoginDto
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; }
    }
    
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
    
    public class RefreshTokenDto
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}