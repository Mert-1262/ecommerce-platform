using ECommerce.Business.Security.JWT;
using ECommerce.Entities.DTOs;

namespace ECommerce.Business.Abstract
{
    public interface IAuthService
    {
        string Register(RegisterDto registerDto);

        AccessToken Login(LoginDto loginDto);
    }
}