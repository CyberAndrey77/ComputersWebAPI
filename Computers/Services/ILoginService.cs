using Computers.Models;
using Computers.Models.Dto;

namespace Computers.Services
{
    public interface ILoginService
    {
        Task<AnswerDto> LoginAsync(LoginDto model);
        Task<AnswerDto> RegistrationAsync(RegistrationDto model);
    }
}
