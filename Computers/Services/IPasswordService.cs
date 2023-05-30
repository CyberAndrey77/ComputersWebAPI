namespace Computers.Services
{
    public interface IPasswordService
    {
        Task<byte[]> CreateHashFromPasswordAsync(string password);

        Task<bool> VerifyPasswordAsync(byte[] password, string repetedPassword);
    }
}
