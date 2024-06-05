using BibliotecaPortafolio.DTOs;
using Dapper;
using System.Data;
using WebApi.Utils;

namespace WebApi.Services
{
    public class YoutubeService : IBaseService<YoutubeDTO_Get, YoutubeDTO_PostPut>
    {
        private readonly IDbConnection _connection;

        public YoutubeService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<YoutubeDTO_Get>> GetAll(CancellationToken cancellationToken)
        {
            return await _connection.QueryAsync<YoutubeDTO_Get>(
               new CommandDefinition(
                   $"PF_Youtube_GetAll",
                   commandType: CommandType.StoredProcedure,
                   transaction: default,
                   cancellationToken: cancellationToken
            ));
        }

        public async Task<YoutubeDTO_Get?> GetById(int Id, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstOrDefaultAsync<YoutubeDTO_Get>(
                 new CommandDefinition(
                     $"PF_Youtube_GetById",
                     new { Id },
                     commandType: CommandType.StoredProcedure,
                     transaction: default,
                     cancellationToken: cancellationToken
            ));
        }

        public async Task<ResponseDB> Insert(YoutubeDTO_PostPut dto, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
               new CommandDefinition(
                   $"PF_Youtube_Insert",
                   new { dto.Nombre, dto.Embed },
                   commandType: CommandType.StoredProcedure,
                   transaction: default,
                   cancellationToken: cancellationToken
            ));
        }

        public async Task<ResponseDB> Update(int Id, YoutubeDTO_PostPut dto, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
               new CommandDefinition(
                   $"PF_Youtube_Update",
                   new { Id, dto.Nombre, dto.Embed },
                   commandType: CommandType.StoredProcedure,
                   transaction: default,
                   cancellationToken: cancellationToken
            ));
        }

        public async Task<ResponseDB> Delete(int Id, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
               new CommandDefinition(
                   $"PF_Youtube_Delete",
                   new { Id },
                   commandType: CommandType.StoredProcedure,
                   transaction: default,
                   cancellationToken: cancellationToken
            ));
        }
    }
}
