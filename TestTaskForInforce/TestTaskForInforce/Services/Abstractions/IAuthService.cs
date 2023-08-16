using System.Security.Claims;
namespace TestTaskForInforce.Services.Abstractions
{
    public interface IAuthService
    {
        Task<ClaimsIdentity> LoginUserAsync(string email, string password);
    }
}
