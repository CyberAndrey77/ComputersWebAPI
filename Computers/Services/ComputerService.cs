using Computers.Models;
using Computers.Models.Dto;
using Computers.Repository;

namespace Computers.Services
{
    public class ComputerService : IComputerService
    {
        private readonly IRepository<Computer> _computerRepository;
        private readonly IRepository<User> _userRepository;
        public ComputerService(IRepository<Computer> repository, IRepository<User> repositoryUser)
        {
            _computerRepository = repository;
            _userRepository = repositoryUser;
        }

        public async Task<AnswerDto> CreateComputerAsync(AuthUserDto model, ComputerDto computer)
        {
            if (computer == null)
            {
                return new ErrorDto("Computer data is null");
            }
            if (string.IsNullOrWhiteSpace(computer.Name))
            {
                return new ErrorDto("Computer name can not be empty");
            }

            //тут нет заносим в ошибку в Answer, так пользователь был авторизован и должен быть в бд,
            //а если его нет, то значит это не ошибка пользователя
            var user = await _userRepository.GetAsync(u => u.Id == model.Id) 
                ?? throw new ArgumentNullException($"User with id = {model.Id} does not exist");

            var newComputer = new Computer()
            {
                Name = computer.Name,
                User = user,
                UserId = user.Id
            };
            await _computerRepository.AddAsync(newComputer);
            return new AnswerDto($"Computer \'{computer.Name}\' created");
        }

        public async Task<AnswerDto> DeleteComputerAsync(int computerId)
        {
            try
            {
                await _computerRepository.DeleteAsync(c => c.Id == computerId);
            }
            catch (ArgumentNullException ex)
            {
                return new ErrorDto($"Computer with id = {computerId} does not exist");
            }
            return new AnswerDto("Computer deleted");
        }

        public async Task<IEnumerable<Computer>> GetComputersAsync(AuthUserDto model, int lastId, int count)
        {
            IEnumerable<Computer> computers;
            if (model.Role == Role.Admin)
            {
                computers = await _computerRepository.GetFirstAsync(c => c.Id > lastId, count);
            }
            else
            {
                computers = await _computerRepository.GetFirstAsync(c => c.Id > lastId && 
                                                            c.UserId == model.Id, count);
            }
            return computers;
        }

        public async Task<AnswerDto> UpdateComputerAsync(AuthUserDto model, ComputerDto computer, int computerId)
        {
            if (computer == null)
            {
                return new ErrorDto("Computer data is null");
            }
            if (string.IsNullOrWhiteSpace(computer.Name))
            {
                return new ErrorDto("Computer name can not be empty");
            }

            var computerFromDb = await _computerRepository.GetAsync(c => computerId == c.Id);
            if (computerFromDb == null)
            {
                return new ErrorDto("Computer does not exist");
            }

            if (computerFromDb.UserId != model.Id)
            {
                return new ErrorDto("Computer is not your");
            }

            computerFromDb.Name = computer.Name;
            await _computerRepository.UpdateAsync(computerFromDb);

            return new AnswerDto("Computer update");
        }
    }
}
