using TestTaskForInforce.Data.Entities;

namespace TestTaskForInforce.Repositories.Abstractions
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetByEmailAndPasswordAsync(string email, string password);

        Task<UserEntity?> GetByEmailAsync(string email);
    }
}
