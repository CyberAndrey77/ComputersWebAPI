using Computers.Models;
using Computers.Models.Dto;
using Computers.Repository;

namespace Computers.Services
{
    public class LoginService : ILoginService
    {
        private readonly IPasswordService _passwordService;
        private readonly IAuthorizationRepository _authorizationService;
        private readonly IRepository<User> _repository;
        public LoginService(IPasswordService passwordService, IRepository<User> repository, 
            IAuthorizationRepository authorizationService)
        {
            _passwordService = passwordService;
            _repository = repository;
            _authorizationService = authorizationService;
        }

        public async Task<AnswerDto> LoginAsync(LoginDto model)
        {
            if (model == null)
            {
                return new ErrorDto("Data is null");
            }
            if (string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password))
            {
                return new ErrorDto("Login or password is empty");
            }

            var user = await _repository.GetAsync(u => u.Login == model.Login);
            if (user == null)
            {
                return new ErrorDto("Login or password uncorect");
            }
                 
            if (!await _passwordService.VerifyPasswordAsync(user.Password, model.Password)) 
            {
                return new ErrorDto("Login or password uncorect");
            }

            _authorizationService.Authorize(user, out var authUser);

            return new LoginAnswerDto(authUser.Guid, "Loggin");
        }

        public async Task<AnswerDto> RegistrationAsync(RegistrationDto model)
        {
            if (model == null)
            {
                return new ErrorDto("Data is null");
            }
            if (string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password) 
                || string.IsNullOrEmpty(model.RepetedPassword))
            {
                return new ErrorDto("Login or password, or repeted password is empty");
            }
            if (model.Password != model.RepetedPassword)
            {
                return new ErrorDto($"Password not equal to repeted password");
            }
            if (model.Role != Role.Admin.ToString() && model.Role != Role.User.ToString())
            {
                return new ErrorDto("Role uncorrect, must be Admin or User");
            }

            var user = new User()
            {
                Login = model.Login,
                Role = model.Role,
                Password = await _passwordService.CreateHashFromPasswordAsync(model.Password)
            };

            await _repository.AddAsync(user);

            return new AnswerDto("Success registration");
        }
    }
}
