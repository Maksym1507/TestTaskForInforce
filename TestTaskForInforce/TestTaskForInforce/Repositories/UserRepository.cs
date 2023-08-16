using Microsoft.EntityFrameworkCore;
using TestTaskForInforce.Data;
using TestTaskForInforce.Data.Entities;
using TestTaskForInforce.Repositories.Abstractions;

namespace TestTaskForInforce.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserEntity?> GetByEmailAndPasswordAsync(string email, string password)
        {
            return await _dbContext.Users.Include(i => i.Role).FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
        }

        public async Task<UserEntity?> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.Include(i => i.Role).FirstOrDefaultAsync(a => a.Email == email);
        }
    }
}
