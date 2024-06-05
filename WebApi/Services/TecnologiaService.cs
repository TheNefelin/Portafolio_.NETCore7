using BibliotecaPortafolio.DTOs;
using Dapper;
using System.Data;
using WebApi.Utils;

namespace WebApi.Services
{
    public class TecnologiaService : IBaseService<TecnologiaDTO_Get, TecnologiaDTO_PostPut>
    {
        private readonly IDbConnection _connection;

        public TecnologiaService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<TecnologiaDTO_Get>> GetAll(CancellationToken cancellationToken)
        {
            return await _connection.QueryAsync<TecnologiaDTO_Get>(
                new CommandDefinition(
                    $"PF_Tecnologia_GetAll",
                    commandType: CommandType.StoredProcedure,
                    transaction: default,
                    cancellationToken: cancellationToken
            ));
        }

        public async Task<TecnologiaDTO_Get?> GetById(int Id, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstOrDefaultAsync<TecnologiaDTO_Get>(
                new CommandDefinition(
                    $"PF_Tecnologia_GetById",
                    new { Id },
                    commandType: CommandType.StoredProcedure,
                    transaction: default,
                    cancellationToken: cancellationToken
            ));
        }

        public async Task<ResponseDB> Insert(TecnologiaDTO_PostPut dto, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
              new CommandDefinition(
                  $"PF_Tecnologia_Insert",
                  new { dto.Nombre, dto.ImgUrl },
                  commandType: CommandType.StoredProcedure,
                  transaction: default,
                  cancellationToken: cancellationToken
          ));
        }

        public async Task<ResponseDB> Update(int Id, TecnologiaDTO_PostPut dto, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
               new CommandDefinition(
                   $"PF_Tecnologia_Update",
                   new { Id, dto.Nombre, dto.ImgUrl },
                   commandType: CommandType.StoredProcedure,
                   transaction: default,
                   cancellationToken: cancellationToken
            ));
        }

        public async Task<ResponseDB> Delete(int Id, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
               new CommandDefinition(
                   $"PF_Tecnologia_Delete",
                   new { Id },
                   commandType: CommandType.StoredProcedure,
                   transaction: default,
                   cancellationToken: cancellationToken
            ));
        }
    }
}
