using Domain.Dto.View;

namespace Service.Abstract
{
    public interface IUserService
    {
        public Task<UserDto?> GetUserByIdAsync(int id);
    }
}
