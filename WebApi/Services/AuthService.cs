using BibliotecaAuth.Classes;
using Dapper;
using System.Data;
using WebApi.Utils;

namespace WebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDbConnection _connection;

        public AuthService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<ResponseDB> Register(NewRegister register, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
               new CommandDefinition(
                   $"Auth_Register",
                   new { register.Id, register.Email, register.Usuario, register.AuthHash, register.AuthSalt },
                   commandType: CommandType.StoredProcedure,
                   transaction: default,
                   cancellationToken: cancellationToken
            ));
        }

        public async Task<LoginCredential?> Login(string email, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstOrDefaultAsync<LoginCredential>(
               new CommandDefinition(
                   $"Auth_Login",
                   new { email },
                   commandType: CommandType.StoredProcedure,
                   transaction: default,
                   cancellationToken: cancellationToken
            ));
        }
    }
}
