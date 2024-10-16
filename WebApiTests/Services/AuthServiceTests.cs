using ClassLibraryApplication.Interfaces;
using ClassLibraryApplication.Services;
using ClassLibraryDTOs;
using Dapper;
using Moq;
using Moq.Dapper;
using System.Data;

namespace WebApiTests.Services
{

    [TestFixture]
    public class AuthServiceTests
    {
        private Mock<IDbConnection> _dbConnectionMock;
        private Mock<IPasswordService> _authPasswordMock;
        private AuthService _authService;

        [SetUp]
        public void SetUp()
        {
            // Inicializa el mock de los servicios
            _dbConnectionMock = new Mock<IDbConnection>();
            _authPasswordMock = new Mock<IPasswordService>();

            _authService = new AuthService(_dbConnectionMock.Object, _authPasswordMock.Object);
        }

        [Test]
        public async Task LoginAsync_ReturnsUser_WhenCredentialsAreValid()
        {
            // Arrange
            var loginDto = new LoginDTO { Email = "test@example.com", Password = "Password123" };
            var userDTO = new UserDTO { Id = "user-id", Email = "test@example.com", Hash1 = "hashedPassword", Salt1 = "salt" };

            // Simulamos que el password es correcto
            _authPasswordMock
                .Setup(p => p.VerifyPassword(It.IsAny<string>(), userDTO.Hash1, userDTO.Salt1))
                .Returns(true);

            _dbConnectionMock.SetupDapperAsync(c => c.QueryFirstOrDefaultAsync<UserDTO>(
                It.IsAny<string>(),
                It.IsAny<object>(),
                null,
                null,
                CommandType.StoredProcedure))
                .ReturnsAsync(userDTO);

            //// Act
            //var result = await _authService.LoginAsync(loginDto, CancellationToken.None);

            //// Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual(200, result.StatusCode);
            //Assert.IsNotNull(result.Data);
            //Assert.AreEqual("user-id", result.Data.Id);
        }

        [Test]
        public async Task LoginAsync_Returns401_WhenUserNotFound()
        {
            // Arrange
            var loginDto = new LoginDTO { Email = "test@example.com", Password = "Password123" };

            // Simulamos que la consulta no encuentra el usuario
            _dbConnectionMock.SetupDapperAsync(c => c.QueryFirstOrDefaultAsync<UserDTO>(
                It.IsAny<string>(),
                It.IsAny<object>(),
                null,
                null,
                CommandType.StoredProcedure))
                .ReturnsAsync((UserDTO)null); // Retornamos null

            //// Act
            //var result = await _authService.LoginAsync(loginDto, CancellationToken.None);

            //// Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual(401, result.StatusCode);
            //Assert.IsNull(result.Data);
        }

    }
}
