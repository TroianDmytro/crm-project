using CRM_Business_Layer.DTO.AuthDTO;
using CRM_Business_Layer.Interfaces;
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

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="model">Пользователь для входа в формате <see cref="LoginModelDTO"/></param>
        /// <returns>Возвращает код 200 и токен при успешной авторизации</returns>
        /// <returns>Возвращает код 404 если нет такого пользователя или если некорректный логин и пароль</returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModelDTO model)
        {
            var result = await _authenticate.Login(model);

            if (result == null)
                return NotFound(result);

            return Ok(result);
        }

        /// <summary>
        /// Регистрация менеджера
        /// </summary>
        /// <param name="model">Пользователь для регистрации в формате <see cref="RegisterModelDTO"/></param>
        /// <returns>Возвращает код 200 при успешной регистрации менеджера</returns>
        /// <returns>Возвращает код 400 если не удалось зарегестрировать менеджера</returns>
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

        /// <summary>
        /// Регистрация администратора
        /// </summary>
        /// <param name="model">Пользователь для регистрации в формате <see cref="RegisterModelDTO"/>.</param>
        /// <returns>Возвращает код 200 при успешной регистрации администратора</returns>
        /// <returns>Возвращает код 400 если не удалось зарегистрировать администратора</returns>
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