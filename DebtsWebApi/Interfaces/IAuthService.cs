using DebtsWebApi.Entities;

namespace DebtsWebApi.Interfaces
{
    public interface IAuthService
    {
        AuthResult Authenticate(string login, string password);
    }
}
