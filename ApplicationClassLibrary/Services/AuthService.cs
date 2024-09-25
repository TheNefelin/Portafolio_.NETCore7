using ApplicationClassLibrary.DTOs;
using ApplicationClassLibrary.Entities;
using ApplicationClassLibrary.Interfaces;
using Dapper;
using System.Data;

namespace ApplicationClassLibrary.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDbConnection _connection;
        private readonly IPasswordService _authPassword;

        public AuthService(IDbConnection connection, IPasswordService authPassword)
        {
            _connection = connection;
            _authPassword = authPassword;
        }

        public async Task<ResponseApiDTO<object>> RegisterAsync(AuthRegisterDTO registerDTO, CancellationToken cancellationToken)
        {
            if (registerDTO.Password1 != registerDTO.Password2)
                return new ResponseApiDTO<object>
                {
                    StatusCode = 400,
                    Message = "Las contraseñas no Coinciden.",
                };

            var (hash, salt) = _authPassword.HashPassword(registerDTO.Password1);

            var newUser = new AuthUserEntity
            {
                Id = Guid.NewGuid().ToString(),
                Email = registerDTO.Email,
                Hash1 = hash,
                Salt1 = salt,
            };

            try
            {
                var result = await _connection.QueryFirstAsync<ResponseSqlDTO>(
                    new CommandDefinition(
                         $"Auth_Register",
                        new { newUser.Id, newUser.Email, newUser.Hash1, newUser.Salt1 },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken
                ));

                return new ResponseApiDTO<object>
                {
                    StatusCode = result.StatusCode,
                    Message = result.Msge,
                    Data = result.StatusCode == 201 ? new { UserId = newUser.Id } : null
                };
            }
            catch (Exception ex)
            {
                return new ResponseApiDTO<object>
                {
                    StatusCode = 500,
                    Message = "Error en la operación de base de datos: " + ex.Message
                };
            }
        }

        public async Task<ResponseApiDTO<AuthUserDTO>> LoginAsync(AuthLoginDTO loginDTO, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _connection.QueryFirstOrDefaultAsync<AuthUserEntity>(
                    new CommandDefinition(
                        $"Auth_Login",
                        new { loginDTO.Email },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken
                ));

                if (result == null)
                    return new ResponseApiDTO<AuthUserDTO>
                    {
                        StatusCode = 401,
                        Message = "Usuario o Contraseña Icorrecta."
                    };

                bool passwordCorrect = _authPassword.VerifyPassword(loginDTO.Password, result.Hash1, result.Salt1);

                if (!passwordCorrect)
                    return new ResponseApiDTO<AuthUserDTO>
                    {
                        StatusCode = 401,
                        Message = "Usuario o Contraseña Icorrecta."
                    };

                AuthUserDTO userDTO = MapToDTO(result);

                return new ResponseApiDTO<AuthUserDTO>
                {
                    StatusCode = 200,
                    Message = "Autenticación Exitosa.",
                    Data = userDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseApiDTO<AuthUserDTO>
                {
                    StatusCode = 500,
                    Message = "Error en la operación de base de datos: " + ex.Message
                };
            }
        }

        private AuthUserDTO MapToDTO(AuthUserEntity userEntity)
        {
            return new AuthUserDTO
            {
                Id = userEntity.Id,
                Email = userEntity.Email,
                SqlToken = userEntity.SqlToken,
                Role = userEntity.Role,
            };
        } 
    }
}
