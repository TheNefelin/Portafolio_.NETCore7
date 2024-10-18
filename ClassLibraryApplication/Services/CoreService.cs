using ClassLibraryApplication.Interfaces;
using ClassLibraryDTOs;
using Dapper;
using System.Data;

namespace ClassLibraryApplication.Services
{
    public class CoreService : ICoreService
    {
        private readonly IDbConnection _connection;
        private readonly IPasswordService _authPassword;

        public CoreService(IDbConnection connection, IPasswordService authPassword)
        {
            _connection = connection;
            _authPassword = authPassword;   
        }

        public async Task<ResponseApiDTO<object>> RegisterAsync(CoreRequestDTO<object> request, CancellationToken cancellationToken)
        {
            try
            {
                var (hash, salt) = _authPassword.HashPassword(request.Password);

                var editUser = new UserDTO
                {
                    Id = request.Id_Usuario,
                    SqlToken = request.Sql_Token,
                    Hash2 = hash,
                    Salt2 = salt,
                };

                var result = await _connection.QueryFirstOrDefaultAsync<ResponseSqlDTO>(
                    new CommandDefinition(
                        $"PM_Core_Register",
                        new { editUser.SqlToken, editUser.Id, editUser.Hash2, editUser.Salt2 },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken
                ));

                return new ResponseApiDTO<object>
                {
                    StatusCode = result.StatusCode,
                    Message = result.Msge,
                    Data = new CoreIVDTO() { IV = result.Id }
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

        public async Task<ResponseApiDTO<object>> LoginAsync(CoreRequestDTO<object> request, CancellationToken cancellationToken)
        {
            try
            {
                var editUser = new UserDTO
                {
                    Id = request.Id_Usuario,
                    SqlToken = request.Sql_Token,
                };

                var result = await _connection.QueryFirstOrDefaultAsync<UserDTO>(
                    new CommandDefinition(
                        $"PM_Core_Login",
                        new { editUser.SqlToken, editUser.Id },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken
                ));

                if (result == null)
                    return new ResponseApiDTO<object>
                    {
                        StatusCode = 404,
                        Message = "Registro No Encontrado."
                    };

                return new ResponseApiDTO<object>
                {
                    StatusCode = 200,
                    Message = "Autenticación Exitosa.",
                    Data = new CoreIVDTO() { IV = result.Salt2 }
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

        public async Task<ResponseApiDTO<IEnumerable<CoreDTO>>> GetAllAsync(string sqlToken, string idUsuario, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _connection.QueryAsync<CoreDTO>(
                    new CommandDefinition(
                        $"PM_Core_Get",
                        new { SqlToken = sqlToken, Id_Usuario = idUsuario, Id = 0 },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken
                ));

                return new ResponseApiDTO<IEnumerable<CoreDTO>>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseApiDTO<IEnumerable<CoreDTO>>
                {
                    StatusCode = 500,
                    Message = "Error en la operación de base de datos: " + ex.Message
                };
            }
        }

        public async Task<ResponseApiDTO<CoreDTO>> GetByIdAsync(string sqlToken, string idUsuario, int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
                return new ResponseApiDTO<CoreDTO>
                {
                    StatusCode = 400,
                    Message = "Ingrese un Id mayor a 0."
                };

            try
            {
                var result = await _connection.QueryFirstOrDefaultAsync<CoreDTO>(
                    new CommandDefinition(
                        $"PM_Core_Get",
                        new { SqlToken = sqlToken, Id_Usuario = idUsuario, Id = id },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken
                ));

                if (result == null)
                    return new ResponseApiDTO<CoreDTO>
                    {
                        StatusCode = 404,
                        Message = "Registro No Encontrado."
                    };

                return new ResponseApiDTO<CoreDTO>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseApiDTO<CoreDTO>
                {
                    StatusCode = 500,
                    Message = "Error en la operación de base de datos: " + ex.Message
                };
            }
        }

        public async Task<ResponseApiDTO<CoreDTO>> InsertAsync(string sqlToken, CoreDTO secretDTO, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _connection.QueryFirstOrDefaultAsync<ResponseSqlDTO>(
                    new CommandDefinition(
                        $"PM_Core_Insert",
                        new { SqlToken = sqlToken, secretDTO.Data01, secretDTO.Data02, secretDTO.Data03, Id_Usuario = secretDTO.Id_User },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken
                ));

                secretDTO.Id = Int32.Parse(result.Id);

                return new ResponseApiDTO<CoreDTO>
                {
                    StatusCode = result.StatusCode,
                    Message = result.Msge,
                    Data = secretDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseApiDTO<CoreDTO>
                {
                    StatusCode = 500,
                    Message = "Error en la operación de base de datos: " + ex.Message
                };
            }
        }

        public async Task<ResponseApiDTO<CoreDTO>> UpdateAsync(string sqlToken, CoreDTO secretDTO, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _connection.QueryFirstOrDefaultAsync<ResponseSqlDTO>(
                    new CommandDefinition(
                        $"PM_Core_Update",
                        new { SqlToken = sqlToken, secretDTO.Id, secretDTO.Data01, secretDTO.Data02, secretDTO.Data03, Id_Usuario = secretDTO.Id_User },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken
                ));

                return new ResponseApiDTO<CoreDTO>
                {
                    StatusCode = result.StatusCode,
                    Message = result.Msge,
                    Data = secretDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseApiDTO<CoreDTO>
                {
                    StatusCode = 500,
                    Message = "Error en la operación de base de datos: " + ex.Message
                };
            }
        }

        public async Task<ResponseApiDTO<CoreDTO>> DeleteAsync(string sqlToken, string idUsuario, int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _connection.QueryFirstOrDefaultAsync<ResponseSqlDTO>(
                    new CommandDefinition(
                        $"PM_Core_Delete",
                        new { SqlToken = sqlToken, Id_Usuario = idUsuario, Id = id },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken
                ));

                return new ResponseApiDTO<CoreDTO>
                {
                    StatusCode = result.StatusCode,
                    Message = result.Msge,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseApiDTO<CoreDTO>
                {
                    StatusCode = 500,
                    Message = "Error en la operación de base de datos: " + ex.Message
                };
            }
        }
    }
}
