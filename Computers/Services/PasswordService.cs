using System.Security.Cryptography;
using System.Text;

namespace Computers.Services
{
    public class PasswordService : IPasswordService
    {
        public async Task<byte[]> CreateHashFromPasswordAsync(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            Stream stream = new MemoryStream(bytes);
            using SHA256 hash = SHA256.Create();
            return await hash.ComputeHashAsync(stream);
        }

        public async Task<bool> VerifyPasswordAsync(byte[] password, string enteredPassword)
        {
            var newHash = await CreateHashFromPasswordAsync(enteredPassword);

            return newHash.SequenceEqual(password);
        }
    }
}
