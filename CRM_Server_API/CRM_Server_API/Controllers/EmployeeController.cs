using CRM_DAL.Entitys.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("employee/")]
[ApiController]
[Authorize]
public class EmployeeController : ControllerBase
{
    private readonly UserManager<EmployeeRegisterModel> _userManager;

    public EmployeeController(UserManager<EmployeeRegisterModel> userManager)
    {
        _userManager = userManager;
    }

    /// <summary>
    /// Получаем авторизированного пользователя
    /// </summary>
    /// <returns>Возвращает код 200 при успешном получении авторизированного пользователя</returns>
    /// <returns>Возвращает код 401 при недействительном Токене</returns>
    /// <returns>Возвраащет код 404 если пользователь не найден</returns>
    [HttpGet("profile")]
    public async Task<IActionResult> GetUserProfile()
    {
        // Отримуємо ID користувача з токена
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "Invalid token or user ID missing." });
        }

        // Знаходимо користувача за ID
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound(new { message = "User not found." });
        }

        // Формуємо відповідь
        var response = new
        {
            user.Id,
            user.UserName,
            user.Email,
            user.PhoneNumber,
            user.Name,
            user.LastName,
            user.Patronymic,
            user.Address,
            user.DateOfBirth,
            user.HireDate,
            user.Position,
            user.Department
        };

        return Ok(response);
    }
}

