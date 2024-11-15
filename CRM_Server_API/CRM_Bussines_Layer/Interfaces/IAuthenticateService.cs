using CRM_Business_Layer.Infrastructure;
using CRM_DAL.Entitys.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CRM_Business_Layer.Interfaces
{
    public interface IAuthenticateService
    {
        Task<TokenDTO?> Login(LoginModel loginModel);

        Task<ResponseAuthenticate> Register(RegisterModel registerModel);

        Task<ResponseAuthenticate> RegisterAdmin(RegisterModel registerModel);

        JwtSecurityToken GetToken(List<Claim> authClaims);

    }
}
