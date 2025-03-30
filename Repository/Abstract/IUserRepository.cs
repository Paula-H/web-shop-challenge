using Domain.Entity;

namespace Repository.Abstract
{
    public interface IUserRepository
    {
        public Task<User?> GetUserByIdAsync(int id);
        public Task<User?> GetUserByEmailAndPasswordAsync(string email, string password);
        public Task<User> CreateUserAsync(User user);
        public Task UpdateUserAsync(User user);
        public Task DeleteUserAsync(User user);
    }
}
