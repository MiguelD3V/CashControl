namespace Cashcontrol.API.Models.Dtos.User
{
    public class PasswordEncryptionResponse
    {
        public PasswordEncryptionResponse(byte[] passwordHash, byte[] passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
