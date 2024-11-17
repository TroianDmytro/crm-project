using AutoMapper;
using CRM_Business_Layer.DTO.AuthDTO;
using CRM_Business_Layer.Infrastructure;
using CRM_Business_Layer.Interfaces;
using CRM_DAL.Entitys.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRM_Business_Layer.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly UserManager<EmployeeRegisterModel> _userEmployee;
        private readonly RoleManager<IdentityRole> _roleEmployee;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AuthenticateService(
            UserManager<EmployeeRegisterModel> userEmployee,
            RoleManager<IdentityRole> roleEmployee,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userEmployee = userEmployee;
            _roleEmployee = roleEmployee;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<TokenDTO?> Login(LoginModelDTO loginModel)
        {
            var user = await _userEmployee.FindByNameAsync(loginModel.UserName);
            if (user != null && await _userEmployee.CheckPasswordAsync(user, loginModel.Password))
            {
                var userRoles = await _userEmployee.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return new TokenDTO()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                };
            }
            return null;
        }


        public async Task<MessageResponseAuthenticate> Register(RegisterModelDTO registerModel)
        {
            var userExists = await _userEmployee.FindByNameAsync(registerModel.UserName);
            if (userExists != null)
                return new MessageResponseAuthenticate { Status = "Error", Message = "Manager already exists!" };

            EmployeeRegisterModel user = _mapper.Map<EmployeeRegisterModel>(registerModel);
            //EmployeeRegisterModel user = new()
            //{
            //    Name = registerModel.Name,
            //    LastName = registerModel.LastName,

            //    Email = registerModel.Email,
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //    UserName = registerModel.UserName

            //};
            var result = await _userEmployee.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
                return new MessageResponseAuthenticate { Status = "Error", Message = "Manager creation failed! Please check user details and try again." };

            // Перевірка, чи існує роль "User"; якщо ні, то створюємо її
            if (!await _roleEmployee.RoleExistsAsync(UserRolesDTO.Manager))
            {
                await _roleEmployee.CreateAsync(new IdentityRole(UserRolesDTO.Manager));
            }

            // Призначення ролі "Manager" користувачеві, якщо роль існує
            if (await _roleEmployee.RoleExistsAsync(UserRolesDTO.Manager))
            {
                await _userEmployee.AddToRoleAsync(user, UserRolesDTO.Manager);
            }

            return new MessageResponseAuthenticate { Status = "Success", Message = "Manager created successfully!" };
        }


        public async Task<MessageResponseAuthenticate> RegisterAdmin(RegisterModelDTO registerModel)
        {
            var userExists = await _userEmployee.FindByNameAsync(registerModel.UserName);
            if (userExists != null)
                return new MessageResponseAuthenticate { Status = "Error", Message = "User already exists!" };

            EmployeeRegisterModel user = _mapper.Map<EmployeeRegisterModel>(registerModel);
            //EmployeeRegisterModel user = new()
            //{
            //    Email = registerModel.Email,
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //    UserName = registerModel.UserName
            //};
            var result = await _userEmployee.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
                return new MessageResponseAuthenticate { Status = "Error", Message = "User creation failed! Please check user details and try again." };

            if (!await _roleEmployee.RoleExistsAsync(UserRolesDTO.Admin))
                await _roleEmployee.CreateAsync(new IdentityRole(UserRolesDTO.Admin));
            if (!await _roleEmployee.RoleExistsAsync(UserRolesDTO.Manager))
                await _roleEmployee.CreateAsync(new IdentityRole(UserRolesDTO.Manager));

            if (await _roleEmployee.RoleExistsAsync(UserRolesDTO.Admin))
            {
                await _userEmployee.AddToRoleAsync(user, UserRolesDTO.Admin);
            }
            if (await _roleEmployee.RoleExistsAsync(UserRolesDTO.Admin))
            {
                await _userEmployee.AddToRoleAsync(user, UserRolesDTO.Manager);
            }

            return new MessageResponseAuthenticate { Status = "Success", Message = "User created successfully!" };
        }

        public JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

    }
}
