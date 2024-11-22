using CRM_Business_Layer.DTO.AuthDTO;
using CRM_Business_Layer.Infrastructure;
using CRM_Business_Layer.Interfaces;
using CRM_Server_API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CRM_Tests
{
    public class AuthController_Test
    {
        private readonly Mock<IAuthenticateService> _mockAuthService;
        private readonly AuthenticateController _controller;

        public AuthController_Test()
        {
            _mockAuthService = new Mock<IAuthenticateService>();
            _controller = new AuthenticateController(_mockAuthService.Object);
        }

        //successful login with correct data returns OkObjectResult with token
        [Fact]
        public async Task Login_ValidCredentials_ReturnsOkResult()
        {
            var loginModel = new LoginModelDTO { UserName = "Bodya", Password = "bodya_12#" };
            var expectedToken = new TokenDTO { Token = "validToken" }; 
            _mockAuthService.Setup(s => s.Login(loginModel)).ReturnsAsync(expectedToken);

            var result = await _controller.Login(loginModel);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedToken, okResult.Value); 
        }

        //login data is incorrect,returns NotFoundObjectResult 
        [Fact]
        public async Task Login_InvalidCredentials_ReturnsNotFoundResult()
        {
            var loginModel = new LoginModelDTO { UserName = "bodya", Password = "128_3kds" };

            _mockAuthService.Setup(s => s.Login(It.IsAny<LoginModelDTO>()))
                            .ReturnsAsync((TokenDTO)null);

            var result = await _controller.Login(loginModel);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result); 
            Assert.Null(notFoundResult.Value); 
        }

        //successful manager registration returns OkObjectResult with correct success
        [Fact]
        public async Task Register_ValidManager_ReturnsOkResult()
        {
            var registerModel = new RegisterModelDTO { UserName = "bodya12", Password = "crBodya_23" };

            var response = new MessageResponseAuthenticate
            {
                Status = "Success",
                Message = "Manager registered successfully"
            };

            _mockAuthService
                .Setup(s => s.Register(It.IsAny<RegisterModelDTO>()))
                .ReturnsAsync(response);

            var result = await _controller.Register(registerModel);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value); 
        }

        //successful admin registration returns OkObjectResult with correct success 
        [Fact]
        public async Task RegisterAdmin_ValidAdmin_ReturnsOkResult()
        {
            var registerModel = new RegisterModelDTO
            {
                UserName = "adminBodya",
                Password = "Crm$_21_$s",
                Email = "bodyaraz@gmail.com"
            };

            var response = new MessageResponseAuthenticate
            {
                Message = "Registered admin successfully",
                Status = "Success"
            };

            _mockAuthService
                .Setup(s => s.RegisterAdmin(It.IsAny<RegisterModelDTO>()))
                .ReturnsAsync(response); 

            var result = await _controller.RegisterAdmin(registerModel);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }

        //failed admin registration returns BadRequestObjectResult
        [Fact]
        public async Task RegisterAdmin_InvalidAdmin_ReturnsBadRequestResult()
        {
            var registerModel = new RegisterModelDTO
            {
                UserName = "adminBodya",
                Password = "Crm$_21_$s",
                Email = "bodyaraz@gmail.com"
            };

            var errorResponse = new MessageResponseAuthenticate
            {
                Status = "Error",
                Message = "Registration admin failed"
            };

            _mockAuthService
                .Setup(s => s.RegisterAdmin(It.IsAny<RegisterModelDTO>()))
                .ReturnsAsync(errorResponse);  

            var result = await _controller.RegisterAdmin(registerModel);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

            var actualResponse = Assert.IsType<MessageResponseAuthenticate>(badRequestResult.Value);
            Assert.Equal(errorResponse.Message, actualResponse.Message);
            Assert.Equal(errorResponse.Status, actualResponse.Status);

            _mockAuthService.Verify(s => s.RegisterAdmin(It.IsAny<RegisterModelDTO>()), Times.Once);
        }

        //register service with correct model and controller returns correct result
        [Fact]
        public async Task Register_ValidArguments_CallsServiceWithCorrectModel()
        {
            var registerModel = new RegisterModelDTO
            {
                UserName = "dima11337",
                Password = "dima_1337#",
                Email = "dimatroyan12@gmail.com"
            };

            var response = new MessageResponseAuthenticate
            {
                Status = "Success",
                Message = "Registration successfuly"
            };

            _mockAuthService.Setup(s => s.Register(It.IsAny<RegisterModelDTO>())).ReturnsAsync(response);

            var result = await _controller.Register(registerModel);

            _mockAuthService.Verify(s => s.Register(It.Is<RegisterModelDTO>(r => r.UserName == "dima11337" && r.Password == "dima_1337#" && r.Email == "dimatroyan12@gmail.com")), Times.Once);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResponse = Assert.IsType<MessageResponseAuthenticate>(okResult.Value);
            Assert.Equal(response.Message, actualResponse.Message);
            Assert.Equal(response.Status, actualResponse.Status);
        }





    }
}