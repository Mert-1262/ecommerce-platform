using ECommerce.Business.Abstract;
using ECommerce.Business.Security.JWT;
using ECommerce.DataAccess.Contexts;
using ECommerce.Entities.Concrete;
using ECommerce.Entities.DTOs;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly ECommerceDbContext _context;

        private readonly ITokenHelper _tokenHelper;

        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthManager(
            ECommerceDbContext context,
            ITokenHelper tokenHelper,
            IPasswordHasher<User> passwordHasher)
        {
            _context = context;

            _tokenHelper = tokenHelper;

            _passwordHasher = passwordHasher;
        }

        public string Register(RegisterDto registerDto)
        {
            bool userExists = _context.Users
                .Any(x => x.Email == registerDto.Email);

            if (userExists)
            {
                return "Kullanıcı zaten mevcut.";
            }

            User user = new()
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                Role = "User"
            };

            user.PasswordHash = _passwordHasher
                .HashPassword(user, registerDto.Password);

            _context.Users.Add(user);

            _context.SaveChanges();

            return "Kayıt başarılı.";
        }

        public AccessToken Login(LoginDto loginDto)
        {
            User? user = _context.Users
                .FirstOrDefault(x => x.Email == loginDto.Email);

            if (user == null)
            {
                throw new Exception("Email veya şifre hatalı.");
            }

            var passwordResult = _passwordHasher
                .VerifyHashedPassword(
                    user,
                    user.PasswordHash,
                    loginDto.Password);
            var hashInDb = user.PasswordHash;

            var passwordFromRequest = loginDto.Password;
            if (passwordResult == PasswordVerificationResult.Failed)
            {
                throw new Exception("Email veya şifre hatalı.");
            }

            return _tokenHelper.CreateToken(user);
        }
    }
}