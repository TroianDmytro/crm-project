using CRM_Business_Layer.DTO.AuthDTO;
using CRM_Business_Layer.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CRM_Business_Layer.Interfaces
{
    public interface IAuthenticateService
    {
        Task<TokenDTO?> Login(LoginModelDTO loginModel);

        Task<ResponseAuthenticate> Register(RegisterModelDTO registerModel);

        Task<ResponseAuthenticate> RegisterAdmin(RegisterModelDTO registerModel);

        JwtSecurityToken GetToken(List<Claim> authClaims);

    }
}
