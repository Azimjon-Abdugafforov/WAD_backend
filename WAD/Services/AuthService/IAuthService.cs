using Microsoft.AspNetCore.Mvc;
using WAD.Models;

namespace WAD.Services.AuthService
{
    public interface IAuthService
    {
       public object Login(MainUser mainUser);
       public  object RefreshToken();
        public void SetRefreshToken(RefreshToken refreshToken);
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public RefreshToken GenerateRefreshToken();
        public string CreateToken(UserDto user);

    }
}
