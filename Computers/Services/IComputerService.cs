using Computers.Models;
using Computers.Models.Dto;

namespace Computers.Services
{
    public interface IComputerService
    {
        Task<IEnumerable<Computer>> GetComputersAsync(AuthUserDto model, int lastId, int count);

        Task<AnswerDto> CreateComputerAsync(AuthUserDto model, ComputerDto computer);

        Task<AnswerDto> UpdateComputerAsync(AuthUserDto model, ComputerDto computer, int computerId);
        Task<AnswerDto> DeleteComputerAsync(int computerId);
    }
}
