using TestTaskForInforce.Data.Entities;
using TestTaskForInforce.Repositories.Abstractions;
using TestTaskForInforce.Services.Abstractions;

namespace TestTaskForInforce.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserEntity?> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            return await _userRepository.GetByEmailAndPasswordAsync(email, password);
        }

        public async Task<UserEntity?> GetUserByEmailAsync(string email, string password)
        {
            return await _userRepository.GetByEmailAsync(email);
        }
    }
}
