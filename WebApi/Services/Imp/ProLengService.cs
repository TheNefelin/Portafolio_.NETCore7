using BibliotecaPortafolio.DTOs;
using Dapper;
using System.Data;
using WebApi.Utils;

namespace WebApi.Services.Imp
{
    public class ProLengService : ISingleService<ProLengDTO>
    {
        private readonly IDbConnection _connection;

        public ProLengService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<ProLengDTO>> GetAll(CancellationToken cancellationToken)
        {
            return await _connection.QueryAsync<ProLengDTO>(
                new CommandDefinition(
                    $"PF_ProLeng_GetAll",
                    commandType: CommandType.StoredProcedure,
                    transaction: default,
                    cancellationToken: cancellationToken
            ));
        }

        public async Task<ResponseDB> Insert(ProLengDTO dto, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
                new CommandDefinition(
                    $"PF_ProLeng_Insert",
                    new { dto.Id_Proyecto, dto.Id_Lenguaje },
                    commandType: CommandType.StoredProcedure,
                    transaction: default,
                    cancellationToken: cancellationToken
            ));
        }

        public async Task<ResponseDB> Delete(ProLengDTO dto, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
                new CommandDefinition(
                    $"PF_ProLeng_Delete",
                    new { dto.Id_Proyecto, dto.Id_Lenguaje },
                    commandType: CommandType.StoredProcedure,
                    transaction: default,
                    cancellationToken: cancellationToken
            ));
        }
    }
}
