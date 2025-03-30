using Domain.Dto;
using Domain.Dto.Create;

namespace Service.Abstract
{
    public interface IAuthService
    {
        public Task<string?> LoginAsync(LoginDto user);
        public Task RegisterAsync(CreateUserDto userDto);
        public Task ChangePassword(ChangePasswordDto changePasswordDto);
    }
}
