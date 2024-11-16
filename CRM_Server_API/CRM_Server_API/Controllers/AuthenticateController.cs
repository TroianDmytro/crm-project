using CRM_Business_Layer.DTO.AuthDTO;
using CRM_Business_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Login([FromBody] LoginModelDTO model)
        {
            var result = await _authenticate.Login(model);

            if (result == null)
                return NotFound(result);

            return Ok(result);
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("register_manager")]
        public async Task<IActionResult> Register([FromBody] RegisterModelDTO model)
        {
            var result = await _authenticate.Register(model);

            if (result.Equals("Error"))
                return BadRequest(result);

            return Ok(result);
        }

        //[Authorize(Roles = UserRoles.Boss)]
        [HttpPost]
        [Route("register_admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModelDTO model)
        {
            var result = await _authenticate.RegisterAdmin(model);

            if (result.Equals("Error"))
                return BadRequest(result);

            return Ok(result);
        }

    }
}