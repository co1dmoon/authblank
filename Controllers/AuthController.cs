using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Services;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("SignIn")]
        async public Task<string> SignIn([FromBody] UserCredentials userCredentials)
        {

            var result = await _authService.SignIn(userCredentials);

            return result;
        }

        [HttpPost("SignUp")]
        async public Task<string> SignUp([FromBody] UserCredentials userCredentials)
        {
            var result = await _authService.SignUp(userCredentials);

            return result;
        }

    }
}
