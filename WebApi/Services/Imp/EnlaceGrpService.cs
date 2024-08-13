using BibliotecaPortafolio.DTOs;
using Dapper;
using System.Data;
using WebApi.Utils;

namespace WebApi.Services.Imp
{
    public class EnlaceGrpService : IBaseService<EnlaceGrpDTO_Get, EnlaceGrpDTO_PostPut>
    {
        private readonly IDbConnection _connection;

        public EnlaceGrpService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<EnlaceGrpDTO_Get>> GetAll(CancellationToken cancellationToken)
        {
            return await _connection.QueryAsync<EnlaceGrpDTO_Get>(
                new CommandDefinition(
                    $"PF_EnlaceGrp_GetAll",
                    commandType: CommandType.StoredProcedure,
                    transaction: default,
                    cancellationToken: cancellationToken
            ));
        }

        public async Task<EnlaceGrpDTO_Get?> GetById(int Id, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstOrDefaultAsync<EnlaceGrpDTO_Get>(
                 new CommandDefinition(
                     $"PF_EnlaceGrp_GetById",
                     new { Id },
                     commandType: CommandType.StoredProcedure,
                     transaction: default,
                     cancellationToken: cancellationToken
             ));
        }

        public async Task<ResponseDB> Insert(EnlaceGrpDTO_PostPut dto, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
                new CommandDefinition(
                    $"PF_EnlaceGrp_Insert",
                    new { dto.Nombre, dto.Estado },
                    commandType: CommandType.StoredProcedure,
                    transaction: default,
                    cancellationToken: cancellationToken
            ));
        }

        public async Task<ResponseDB> Update(int Id, EnlaceGrpDTO_PostPut dto, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
               new CommandDefinition(
                   $"PF_EnlaceGrp_Update",
                   new { Id, dto.Nombre, dto.Estado },
                   commandType: CommandType.StoredProcedure,
                   transaction: default,
                   cancellationToken: cancellationToken
            ));
        }

        public async Task<ResponseDB> Delete(int Id, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
               new CommandDefinition(
                   $"PF_EnlaceGrp_Delete",
                   new { Id },
                   commandType: CommandType.StoredProcedure,
                   transaction: default,
                   cancellationToken: cancellationToken
            ));
        }
    }
}
