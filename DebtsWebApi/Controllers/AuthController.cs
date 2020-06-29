using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DebtsWebApi.Interfaces;
using DebtsWebApi.Entities;

namespace DebtsWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthInfo authInfo)
        {
            var user = _authService.Authenticate(authInfo.Login, authInfo.Password);

            if (user == null)
                return BadRequest(new { message = "Неверный логин или пароль." });

            return Ok(user);
        }
    }
}