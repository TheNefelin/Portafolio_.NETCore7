using BibliotecaPortafolio.DTOs;
using Dapper;
using System.Data;
using WebApi.Utils;

namespace WebApi.Services
{
    public class ProyectoService : IBaseService<ProyectoDTO_Get, ProyectoDTO_PostPut>
    {
        private readonly IDbConnection _connection;

        public ProyectoService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<ProyectoDTO_Get>> GetAll(CancellationToken cancellationToken)
        {
            return await _connection.QueryAsync<ProyectoDTO_Get>(
                new CommandDefinition(
                    $"PF_Proyecto_GetAll",
                    commandType: CommandType.StoredProcedure,
                    transaction: default,
                    cancellationToken: cancellationToken
            ));
        }

        public async Task<ProyectoDTO_Get?> GetById(int Id, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstOrDefaultAsync<ProyectoDTO_Get>(
                new CommandDefinition(
                    $"PF_Proyecto_GetById",
                    new { Id },
                    commandType: CommandType.StoredProcedure,
                    transaction: default,
                    cancellationToken: cancellationToken
            ));
        }

        public async Task<ResponseDB> Insert(ProyectoDTO_PostPut dto, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
                new CommandDefinition(
                    $"PF_Proyecto_Insert",
                    new { dto.Nombre, dto.ImgUrl },
                    commandType: CommandType.StoredProcedure,
                    transaction: default,
                    cancellationToken: cancellationToken
            ));
        }

        public async Task<ResponseDB> Update(int Id, ProyectoDTO_PostPut dto, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstAsync<ResponseDB>(
               new CommandDefinition(
                   $"PF_Proyecto_Update",
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
                   $"PF_Proyecto_Delete",
                   new { Id },
                   commandType: CommandType.StoredProcedure,
                   transaction: default,
                   cancellationToken: cancellationToken
            ));
        }
    }
}
