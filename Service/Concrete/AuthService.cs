using Repository.Abstract;
using Service.Abstract;
using Domain.Entity;
using Domain.Dto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using FluentValidation;
using Domain.Dto.Create;
using AutoMapper;

namespace Service.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository userRepository;
        private readonly AbstractValidator<User> userValidator;
        private readonly string jwtSecret;
        private readonly IMapper _mapper;

        public AuthService(
            IUserRepository userRepository,
            AbstractValidator<User> userValidator,
            IMapper mapper)
        {
            this.userRepository = userRepository;
            jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
            this.userValidator = userValidator;
            _mapper = mapper;
        }


        public async Task<string?> LoginAsync(LoginDto loginDto)
        {
            var user = await userRepository.GetUserByEmailAndPasswordAsync(loginDto.Email, loginDto.Password);
            if (user == null)
            {
                return null;
            }
            return GenerateJwtToken(user);
        }

        public async Task RegisterAsync(CreateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var validation = userValidator.Validate(user);
            if (!validation.IsValid)
            {
                throw new Exception(validation.ToString());
            }
            await userRepository.CreateUserAsync(user);
        }

        public async Task ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var user = await userRepository.GetUserByIdAsync(changePasswordDto.Id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (user.Password == changePasswordDto.OldPassword)
            {
                user.Password = changePasswordDto.NewPassword;
                await userRepository.UpdateUserAsync(user);
            }
            else
            {
                throw new Exception("Old password is incorrect");
            }
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Name + user.Surname),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
