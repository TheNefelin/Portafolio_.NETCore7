using ClassLibraryApplication.DTOs;
using ClassLibraryApplication.Interfaces;
using Dapper;
using System.Data;

namespace ClassLibraryApplication.Services
{
    public class SecretService : ISecretService
    {
        private readonly IDbConnection _connection;
        private readonly IPasswordService _authPassword;

        public SecretService(IDbConnection connection, IPasswordService authPassword)
        {
            _connection = connection;
            _authPassword = authPassword;   
        }

        public async Task<ResponseApiDTO<IEnumerable<SecretDTO>>> GetAllAsync(string sqlToken, string idUsuario, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _connection.QueryAsync<SecretDTO>(
                    new CommandDefinition(
                        $"PM_Core_Get",
                        new { SqlToken = sqlToken, Id_Usuario = idUsuario, Id = 0 },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken
                ));

                return new ResponseApiDTO<IEnumerable<SecretDTO>>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseApiDTO<IEnumerable<SecretDTO>>
                {
                    StatusCode = 500,
                    Message = "Error en la operación de base de datos: " + ex.Message
                };
            }
        }

        public async Task<ResponseApiDTO<SecretDTO>> GetByIdAsync(string sqlToken, string idUsuario, int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
                return new ResponseApiDTO<SecretDTO>
                {
                    StatusCode = 400,
                    Message = "Ingrese un Id mayor a 0."
                };

            try
            {
                var result = await _connection.QueryFirstOrDefaultAsync<SecretDTO>(
                    new CommandDefinition(
                        $"PM_Core_Get",
                        new { SqlToken = sqlToken, Id_Usuario = idUsuario, Id = id  },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken
                ));

                if (result == null)
                    return new ResponseApiDTO<SecretDTO>
                    {
                        StatusCode = 404,
                        Message = "Registro No Encontrado."
                    };

                return new ResponseApiDTO<SecretDTO>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseApiDTO<SecretDTO>
                {
                    StatusCode = 500,
                    Message = "Error en la operación de base de datos: " + ex.Message
                };
            }
        }

        public async Task<ResponseApiDTO<SecretDTO>> InsertAsync(string sqlToken, SecretDTO secretDTO, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _connection.QueryFirstOrDefaultAsync<ResponseSqlDTO>(
                    new CommandDefinition(
                        $"PM_Core_Insert",
                        new { SqlToken = sqlToken, secretDTO.Id_Usuario,  secretDTO.Data01, secretDTO.Data02, secretDTO.Data03 },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken
                ));

                secretDTO.Id_Usuario = result.Id;

                return new ResponseApiDTO<SecretDTO>
                {
                    StatusCode = result.StatusCode,
                    Message = result.Msge,
                    Data = secretDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseApiDTO<SecretDTO>
                {
                    StatusCode = 500,
                    Message = "Error en la operación de base de datos: " + ex.Message
                };
            }
        }

        public async Task<ResponseApiDTO<SecretDTO>> UpdateAsync(string sqlToken, SecretDTO secretDTO, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _connection.QueryFirstOrDefaultAsync<ResponseSqlDTO>(
                    new CommandDefinition(
                        $"PM_Core_Update",
                        new { SqlToken = sqlToken, secretDTO.Id_Usuario, secretDTO.Id, secretDTO.Data01, secretDTO.Data02, secretDTO.Data03 },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken
                ));

                return new ResponseApiDTO<SecretDTO>
                {
                    StatusCode = result.StatusCode,
                    Message = result.Msge,
                    Data = secretDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseApiDTO<SecretDTO>
                {
                    StatusCode = 500,
                    Message = "Error en la operación de base de datos: " + ex.Message
                };
            }
        }

        public async Task<ResponseApiDTO<SecretDTO>> DeleteAsync(string sqlToken, string idUsuario, int id, CancellationToken cancellationToken)
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

                return new ResponseApiDTO<SecretDTO>
                {
                    StatusCode = result.StatusCode,
                    Message = result.Msge,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseApiDTO<SecretDTO>
                {
                    StatusCode = 500,
                    Message = "Error en la operación de base de datos: " + ex.Message
                };
            }
        }
    }
}
