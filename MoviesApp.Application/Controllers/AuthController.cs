using Microsoft.AspNetCore.Mvc;
using MoviesApp.Core.DTO;
using MoviesApp.Core.Services;

namespace MoviesApp.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<AuthResponseDTO>> Register(RegisterDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            if (result.IsAuthenticated == false)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponseDTO>> Login(LoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(model);

            if (result.IsAuthenticated == false)
                return BadRequest(result);

            return Ok(result);
        }
    }
} 