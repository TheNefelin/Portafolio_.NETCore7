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

        public async Task<ResponseApiDTO<CoreIVDTO>> RegisterAsync(CoreRequestDTO<object> request, CancellationToken cancellationToken)
        {
            try
            {
                var (hash, salt) = _authPassword.HashPassword(request.Password);

                var result = await _connection.QueryFirstOrDefaultAsync<ResponseSqlDTO>(
                    new CommandDefinition(
                        $"PM_Core_Register",
                        new { SqlToken = request.Sql_Token, request.Id_Usuario, hash2 = hash, salt2 = salt },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken
                ));

                if (result == null)
                    return new ResponseApiDTO<CoreIVDTO>
                    {
                        StatusCode = 404,
                        Message = "Registro No Encontrado."
                    };

                return new ResponseApiDTO<CoreIVDTO>
                {
                    StatusCode = result.StatusCode,
                    Message = result.Msge,
                    Data = new CoreIVDTO() { IV = result.Id }
                };
            }
            catch (Exception ex)
            {
                return new ResponseApiDTO<CoreIVDTO>
                {
                    StatusCode = 500,
                    Message = "Error en la operación de base de datos: " + ex.Message
                };
            }
        }

        public async Task<ResponseApiDTO<CoreIVDTO>> LoginAsync(CoreRequestDTO<object> request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _connection.QueryFirstOrDefaultAsync<UserDTO>(
                    new CommandDefinition(
                        $"PM_Core_Login",
                        new { SqlToken = request.Sql_Token, request.Id_Usuario },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken
                ));

                if (result == null)
                    return new ResponseApiDTO<CoreIVDTO>
                    {
                        StatusCode = 404,
                        Message = "Registro No Encontrado."
                    };

                return new ResponseApiDTO<CoreIVDTO>
                {
                    StatusCode = 200,
                    Message = "Autenticación Exitosa.",
                    Data = new CoreIVDTO() { IV = result.Salt2 }
                };
            }
            catch (Exception ex)
            {
                return new ResponseApiDTO<CoreIVDTO>
                {
                    StatusCode = 500,
                    Message = "Error en la operación de base de datos: " + ex.Message
                };
            }
        }

        public async Task<ResponseApiDTO<IEnumerable<CoreDTO>>> GetAllAsync(CoreRequestDTO<object> request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _connection.QueryAsync<CoreDTO>(
                    new CommandDefinition(
                        $"PM_Core_Get",
                        new { SqlToken = request.Sql_Token, request.Id_Usuario, Id = 0 },
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

        public async Task<ResponseApiDTO<CoreDTO>> GetByIdAsync(CoreRequestDTO<CoreDTO> request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _connection.QueryFirstOrDefaultAsync<CoreDTO>(
                    new CommandDefinition(
                        $"PM_Core_Get",
                        new { SqlToken = request.Sql_Token, request.Id_Usuario, request.CoreData.Id },
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

        public async Task<ResponseApiDTO<CoreDTO>> InsertAsync(CoreRequestDTO<CoreDTO> request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _connection.QueryFirstOrDefaultAsync<ResponseSqlDTO>(
                    new CommandDefinition(
                        $"PM_Core_Insert",
                        new { SqlToken = request.Sql_Token, request.Id_Usuario, request.CoreData.Data01, request.CoreData.Data02, request.CoreData.Data03 },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken
                ));

                request.CoreData.Id = Int32.Parse(result.Id);

                return new ResponseApiDTO<CoreDTO>
                {
                    StatusCode = result.StatusCode,
                    Message = result.Msge,
                    Data = request.CoreData,
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

        public async Task<ResponseApiDTO<CoreDTO>> UpdateAsync(CoreRequestDTO<CoreDTO> request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _connection.QueryFirstOrDefaultAsync<ResponseSqlDTO>(
                    new CommandDefinition(
                        $"PM_Core_Update",
                        new { SqlToken = request.Sql_Token, request.Id_Usuario, request.CoreData.Id, request.CoreData.Data01, request.CoreData.Data02, request.CoreData.Data03 },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken
                ));

                return new ResponseApiDTO<CoreDTO>
                {
                    StatusCode = result.StatusCode,
                    Message = result.Msge,
                    Data = request.CoreData,
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

        public async Task<ResponseApiDTO<object>> DeleteAsync(CoreRequestDTO<CoreDTO> request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _connection.QueryFirstOrDefaultAsync<ResponseSqlDTO>(
                    new CommandDefinition(
                        $"PM_Core_Delete",
                        new { SqlToken = request.Sql_Token, request.Id_Usuario, request.CoreData.Id },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken
                ));

                return new ResponseApiDTO<object>
                {
                    StatusCode = result.StatusCode,
                    Message = result.Msge,
                    Data = null
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
    }
}
