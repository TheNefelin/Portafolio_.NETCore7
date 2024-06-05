using BibliotecaAuth.Classes;
using WebApi.Utils;

namespace WebApi.Services
{
    public interface IAuthService
    {
        public Task<ResponseDB> Register(NewRegister newRegister, CancellationToken cancellationToken);
        public Task<LoginCredential?> Login(string email, CancellationToken cancellationToken);
    }
}
