using ClassLibraryApplication.DTOs;
using ClassLibraryApplication.Interfaces;
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
                        new { SqlToken = sqlToken, Id_Usuario = idUsuario, Id = id  },
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
                        new { SqlToken = sqlToken, secretDTO.Id_User,  secretDTO.Data01, secretDTO.Data02, secretDTO.Data03 },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken
                ));

                secretDTO.Id_User = result.Id;

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
                        new { SqlToken = sqlToken, secretDTO.Id_User, secretDTO.Id, secretDTO.Data01, secretDTO.Data02, secretDTO.Data03 },
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
