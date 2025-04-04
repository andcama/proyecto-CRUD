using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolizasCRUD.API.DTOs;
using PolizasCRUD.API.Services;

namespace PolizasCRUD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterUserDto registerDto)
        {
            try
            {
                var usuario = await _authService.RegisterAsync(registerDto);
                return Ok(usuario);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al registrar el usuario");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            try
            {
                var usuario = await _authService.LoginAsync(loginDto);
                return Ok(usuario);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al iniciar sesión");
            }
        }

        [HttpPost("refresh-token")]
        public ActionResult<UserDto> RefreshToken(RefreshTokenDto refreshTokenDto)
        {
            // Token refresh disabled - authentication removed
            return NotFound("Feature disabled");
        }

        [HttpPost("logout")]
        public ActionResult Logout()
        {
            // Logout disabled - authentication removed
            return Ok(new { message = "Sesión cerrada correctamente" });
        }
    }
}