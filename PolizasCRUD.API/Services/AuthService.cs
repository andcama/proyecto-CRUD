using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PolizasCRUD.API.Auth;
using PolizasCRUD.API.Data;
using PolizasCRUD.API.DTOs;
using PolizasCRUD.API.Models;

namespace PolizasCRUD.API.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> RegisterAsync(RegisterUserDto registerDto)
        {
            // Verificar si el usuario ya existe
            if (await _context.Usuarios.AnyAsync(u => u.Username == registerDto.Username))
                throw new InvalidOperationException("El nombre de usuario ya está en uso");

            if (await _context.Usuarios.AnyAsync(u => u.Email == registerDto.Email))
                throw new InvalidOperationException("El email ya está en uso");

            // Crear usuario
            PasswordHasher.CreatePasswordHash(registerDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var usuario = new Usuario
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Id = usuario.Id,
                Username = usuario.Username,
                Email = usuario.Email,
                Token = "token-disabled",
                RefreshToken = "refresh-token-disabled"
            };
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Username == loginDto.Username);

            if (usuario == null)
                throw new UnauthorizedAccessException("Usuario o contraseña incorrectos");

            if (!PasswordHasher.VerifyPasswordHash(loginDto.Password, usuario.PasswordHash, usuario.PasswordSalt))
                throw new UnauthorizedAccessException("Usuario o contraseña incorrectos");

            return new UserDto
            {
                Id = usuario.Id,
                Username = usuario.Username,
                Email = usuario.Email,
                Token = "token-disabled",
                RefreshToken = "refresh-token-disabled"
            };
        }

        public async Task<UserDto> RefreshTokenAsync(string token, string refreshToken)
        {
            // Autenticación deshabilitada
            throw new UnauthorizedAccessException("Authentication disabled");
        }

        public async Task<bool> LogoutAsync(int userId)
        {
            // Autenticación deshabilitada
            return true;
        }
    }
}