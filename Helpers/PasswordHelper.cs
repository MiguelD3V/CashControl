using Cashcontrol.API.Helpers.Interface;
using Cashcontrol.API.Models.Dtos.User;
using Microsoft.AspNetCore.Identity;

namespace Cashcontrol.API.Helpers
{
    public class PasswordHelper : IPasswordHelper
    {
        public PasswordEncryptionResponse CreatePasswordHash(string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            { 
                return new PasswordEncryptionResponse(
                passwordSalt: hmac.Key,
                passwordHash: hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))
                );
            }
        }
        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }
    }
}
