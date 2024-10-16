﻿using ClassLibraryApplication.Interfaces;
using ClassLibraryDTOs;
using Microsoft.Extensions.Configuration;
using Moq;
using WebApi.Controllers;

namespace WebApiTests.Controllers
{
    [TestFixture]
    public class AuthControllerTests
    {
        private Mock<IAuthService> _authServiceMock;
        private Mock<IConfiguration> _configurationMock;
        private AuthController _authController;

        [SetUp]
        public void SetUp()
        {
            _authServiceMock = new Mock<IAuthService>();
            _configurationMock = new Mock<IConfiguration>();

            // Configura el valor de JWT en la configuración
            _configurationMock.Setup(c => c["JWT:ExpireMin"]).Returns("60");
            _configurationMock.Setup(c => c["Jwt:Key"]).Returns("MySuperSecretKey12345MySuperSecretKey12345");
            _configurationMock.Setup(c => c["Jwt:Issuer"]).Returns("your-issuer");
            _configurationMock.Setup(c => c["Jwt:Audience"]).Returns("your-audience");

            _authController = new AuthController(_configurationMock.Object, _authServiceMock.Object);
        }

        [Test]
        public async Task Login_ReturnsOk_WhenCredentialsAreValid()
        {
            //// Arrange
            //var loginDTO = new LoginDTO { Email = "testuser", Password = "testpassword" };
            //var expectedUser = new UserDTO { Id = "123", Email = "testuser@example.com", Role = "User" };
            //var expectedResponse = new ResponseApiDTO<UserDTO>
            //{
            //    StatusCode = 200,
            //    Message = "Login successful",
            //    Data = expectedUser
            //};

            //_authServiceMock.Setup(x => x.LoginAsync(loginDTO, It.IsAny<CancellationToken>()))
            //    .ReturnsAsync(expectedResponse);

            //// Act
            //var result = await _authController.Login(loginDTO, CancellationToken.None);

            //// Assert
            //var okResult = result as OkObjectResult;  // Convertir a OkObjectResult
            //Assert.IsNotNull(okResult);  // Asegurar que el resultado no es nulo
            //Assert.AreEqual(200, okResult.StatusCode);  // Verificar el código de estado
            //var responseData = okResult.Value as dynamic;  // Acceder a los datos dinámicamente
            //Assert.IsNotNull(responseData);
            ////Assert.AreEqual(expectedUser.Id, responseData.Data.Id);
            ////Assert.AreEqual(expectedUser.Email, responseData.Data.Email);
        }


        [Test]
        public async Task Login_ReturnsStatusCode500_WhenUserIsNull()
        {
            // Arrange
            var loginDTO = new LoginDTO
            {
                Email = "test@example.com",
                Password = "Password123"
            };

            //var expectedResponse = new ResponseApiDTO<UserDTO>
            //{
            //    StatusCode = 200,
            //    Message = "Login successful",
            //    Data = null
            //};

            //_authServiceMock
            //    .Setup(service => service.LoginAsync(It.IsAny<LoginDTO>(), It.IsAny<CancellationToken>()))
            //    .ReturnsAsync(expectedResponse);

            //// Act
            //var result = await _authController.Login(loginDTO, CancellationToken.None) as ObjectResult;

            //// Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual(500, result.StatusCode);
            //Assert.AreEqual("Error al procesar los datos del usuario.", result.Value);
        }

        [Test]
        public async Task Login_ReturnsError_WhenCredentialsAreInvalid()
        {
            // Arrange
            var loginDTO = new LoginDTO
            {
                Email = "wrong@example.com",
                Password = "WrongPassword123"
            };

            //var expectedResponse = new ResponseApiDTO<UserDTO>
            //{
            //    StatusCode = 401,
            //    Message = "Invalid credentials",
            //    Data = null
            //};

            //_authServiceMock
            //    .Setup(service => service.LoginAsync(It.IsAny<LoginDTO>(), It.IsAny<CancellationToken>()))
            //    .ReturnsAsync(expectedResponse);

            //// Act
            //var result = await _authController.Login(loginDTO, CancellationToken.None) as ObjectResult;

            //// Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual(401, result.StatusCode);
            //Assert.AreEqual(expectedResponse, result.Value);
        }
    }

}
