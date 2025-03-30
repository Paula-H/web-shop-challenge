using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Abstract;

namespace Repository.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<User?> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            return _dbContext.Users
                .FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var result = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task UpdateUserAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
