using CRM_Business_Layer.Interfaces;
using CRM_DAL.Entitys.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Server_API.Controllers
{
    [Route("auth/")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticate;
        public AuthenticateController(IAuthenticateService authenticate)
        {
            _authenticate = authenticate;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _authenticate.Login(model);

            if (result == null)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await _authenticate.Register(model);

            if (result.Equals("Error"))
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var result = await _authenticate.RegisterAdmin(model);

            if (result.Equals("Error"))
                return BadRequest(result);

            return Ok(result);
        }

    }
}