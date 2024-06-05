using BibliotecaPortafolio.DTOs;
using Dapper;
using System.Data;
using WebApi.Utils;

namespace WebApi.Services
{
    public class LenguajeService : IBaseService<LenguajeDTO_Get, LenguajeDTO_PostPut>
    {
        private readonly IDbConnection _connection;

        public LenguajeService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<LenguajeDTO_Get>> GetAll(CancellationToken cancellationToken)
        {
            return await _connection.QueryAsync<LenguajeDTO_Get>(
                new CommandDefinition(
                    $"PF_Lenguaje_GetAll",
                    commandType: CommandType.StoredProcedure,
                    transaction: default,
                    cancellationToken: cancellationToken
            ));
        }

        public async Task<LenguajeDTO_Get?> GetById(int Id, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstOrDefaultAsync<LenguajeDTO_Get>(
              new CommandDefinition(
                  $"PF_Lenguaje_GetById",
                  new { Id },
                  commandType: CommandType.StoredProcedure,
                  transaction: default,
                  cancellationToken: cancellationToken
          ));
        }

        public async Task<ResponseDB> Insert(LenguajeDTO_PostPut dto, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
            new CommandDefinition(
                $"PF_Lenguaje_Insert",
                new { dto.Nombre, dto.ImgUrl },
                commandType: CommandType.StoredProcedure,
                transaction: default,
                cancellationToken: cancellationToken
        ));
        }

        public async Task<ResponseDB> Update(int Id, LenguajeDTO_PostPut dto, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
               new CommandDefinition(
                   $"PF_Lenguaje_Update",
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
                   $"PF_Lenguaje_Delete",
                   new { Id },
                   commandType: CommandType.StoredProcedure,
                   transaction: default,
                   cancellationToken: cancellationToken
            ));
        }
    }
}
