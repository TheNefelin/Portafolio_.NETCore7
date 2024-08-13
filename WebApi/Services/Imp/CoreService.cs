using BibliotecaPasswordManager.DTOs;
using Dapper;
using System.Data;
using WebApi.Utils;

namespace WebApi.Services.Imp
{
    public class CoreService : ICoreService
    {
        private readonly IDbConnection _connection;

        public CoreService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<CoreDTO_Get>> GetAll(string Deco, string Id_Usuario, CancellationToken cancellationToken)
        {
            return await _connection.QueryAsync<CoreDTO_Get>(
                new CommandDefinition(
                    $"PM_GetAll",
                    new { Deco, Id_Usuario },
                    commandType: CommandType.StoredProcedure,
                    transaction: default,
                    cancellationToken: cancellationToken
            ));
        }

        public async Task<ResponseDB> Insert(string Deco, CoreDTO_PostPut dto, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
                new CommandDefinition(
                    $"PM_Insert",
                    new { Deco, dto.Data01, dto.Data02, dto.Data03, dto.Id_Usuario },
                    commandType: CommandType.StoredProcedure,
                    transaction: default,
                    cancellationToken: cancellationToken
            ));
        }

        public async Task<ResponseDB> Update(int Id, string Deco, CoreDTO_PostPut dto, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
                new CommandDefinition(
                    $"PM_Update",
                    new { Id, Deco, dto.Data01, dto.Data02, dto.Data03, dto.Id_Usuario },
                    commandType: CommandType.StoredProcedure,
                    transaction: default,
                    cancellationToken: cancellationToken
            ));
        }

        public async Task<ResponseDB> Delete(int Id, string Id_Usuario, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
                new CommandDefinition(
                    $"PM_Delete",
                    new { Id, Id_Usuario },
                    commandType: CommandType.StoredProcedure,
                    transaction: default,
                    cancellationToken: cancellationToken
            ));
        }
    }
}
