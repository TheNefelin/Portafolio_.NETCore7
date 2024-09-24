using BibliotecaPortafolio.DTOs;
using Dapper;
using System.Data;
using WebApi.Utils;

namespace WebApi.Services.Imp
{
    public class EnlaceService : IBaseService<EnlaceDTO_Get, EnlaceDTO_PostPut>
    {
        private readonly IDbConnection _connection;

        public EnlaceService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<EnlaceDTO_Get>> GetAll(CancellationToken cancellationToken)
        {
            return await _connection.QueryAsync<EnlaceDTO_Get>(
                new CommandDefinition(
                    $"PF_Enlace_GetAll",
                    commandType: CommandType.StoredProcedure,
                    transaction: default,
                    cancellationToken: cancellationToken
            ));
        }

        public async Task<EnlaceDTO_Get?> GetById(int Id, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstOrDefaultAsync<EnlaceDTO_Get>(
                 new CommandDefinition(
                     $"PF_Enlace_GetById",
                     new { Id },
                     commandType: CommandType.StoredProcedure,
                     transaction: default,
                     cancellationToken: cancellationToken
             ));
        }

        public async Task<ResponseDB> Insert(EnlaceDTO_PostPut dto, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
               new CommandDefinition(
                   $"PF_Enlace_Insert",
                   new { dto.Nombre, dto.EnlaceUrl, dto.Estado, dto.Id_EnlaceGrp },
                   commandType: CommandType.StoredProcedure,
                   transaction: default,
                   cancellationToken: cancellationToken
            ));
        }

        public async Task<ResponseDB> Update(int Id, EnlaceDTO_PostPut dto, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
               new CommandDefinition(
                   $"PF_Enlace_Update",
                   new { Id, dto.Nombre, dto.EnlaceUrl, dto.Estado, dto.Id_EnlaceGrp },
                   commandType: CommandType.StoredProcedure,
                   transaction: default,
                   cancellationToken: cancellationToken
            ));
        }

        public async Task<ResponseDB> Delete(int Id, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
               new CommandDefinition(
                   $"PF_Enlace_Delete",
                   new { Id },
                   commandType: CommandType.StoredProcedure,
                   transaction: default,
                   cancellationToken: cancellationToken
            ));
        }
    }
}
