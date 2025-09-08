using Cashcontrol.API.Models.Dtos.User;

namespace Cashcontrol.API.Helpers.Interface
{
    public interface IPasswordHelper
    {
        public PasswordEncryptionResponse CreatePasswordHash(string password);
        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
    }
}
