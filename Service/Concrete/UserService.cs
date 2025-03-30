using AutoMapper;
using Domain.Dto.View;
using Repository.Abstract;
using Service.Abstract;

namespace Service.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            return _mapper.Map<UserDto>(await userRepository.GetUserByIdAsync(id));
        }
    }
}
