using TestTaskForInforce.Data.Entities;

namespace TestTaskForInforce.Services.Abstractions
{
    public interface IUserService
    {
        Task<UserEntity?> GetUserByEmailAndPasswordAsync(string email, string password);
    }
}
