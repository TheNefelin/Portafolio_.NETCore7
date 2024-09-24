using BibliotecaPortafolio.DTOs;
using Dapper;
using System.Data;
using WebApi.Utils;

namespace WebApi.Services.Imp
{
    public class ProTecService : ISingleService<ProTecDTO>
    {
        private readonly IDbConnection _connection;

        public ProTecService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<ProTecDTO>> GetAll(CancellationToken cancellationToken)
        {
            return await _connection.QueryAsync<ProTecDTO>(
                new CommandDefinition(
                    $"PF_ProTec_GetAll",
                    commandType: CommandType.StoredProcedure,
                    transaction: default,
                    cancellationToken: cancellationToken
            ));
        }

        public async Task<ResponseDB> Insert(ProTecDTO dto, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
                new CommandDefinition(
                    $"PF_ProTec_Insert",
                    new { dto.Id_Proyecto, dto.Id_Tecnologia },
                    commandType: CommandType.StoredProcedure,
                    transaction: default,
                    cancellationToken: cancellationToken
            ));
        }

        public async Task<ResponseDB> Delete(ProTecDTO dto, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
               new CommandDefinition(
                   $"PF_ProTec_Delete",
                   new { dto.Id_Proyecto, dto.Id_Tecnologia },
                   commandType: CommandType.StoredProcedure,
                   transaction: default,
                   cancellationToken: cancellationToken
            ));
        }
    }
}
