using Computers.Models;
using Computers.Models.Dto;
using Computers.Services;
using System.Collections.Concurrent;

namespace Computers.Repository
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly ConcurrentDictionary<Guid, AuthUserDto> _authUsers = new();
        private readonly int _timeSession = 30;
        public AuthorizationRepository()
        {
            ITimerService timer = new TimerService(this);
            timer.StartTimer();
        }

        public bool Authorize(User user, out AuthUserDto authorizationUser)
        {
            //Не дадим пользователю авторизоваться несколько раз
            authorizationUser = _authUsers.FirstOrDefault(u => u.Value.Login == user.Login 
                                                          && u.Value.Role.ToString() == user.Role).Value;
            if (authorizationUser == null)
            {
                authorizationUser = new AuthUserDto
                {
                    Guid = Guid.NewGuid(),
                    Id = user.Id,
                    Login = user.Login,
                    Role = (Role)Enum.Parse(typeof(Role), user.Role)
                };
            }
            UpdateTime(authorizationUser);
            return _authUsers.TryAdd(authorizationUser.Guid, authorizationUser);
        }

        public void DeleteAutorizationUsers()
        {
            //Удаляем пользователей, которые не активны последние полчаса
            var keys = new List<Guid>();
            keys = _authUsers.Where(u => u.Value.LastUpdate - DateTime.Now <= TimeSpan.FromMinutes(_timeSession))
                .Select(x => x.Key).ToList();
            foreach (var key in keys)
            {
                if (_authUsers.TryGetValue(key, out var user))
                {
                    _authUsers.TryRemove(key, out user);
                }
            }
        }

        public bool GetAuthorizationUser(Guid guid, out AuthUserDto user)
        {
            var result = _authUsers.TryGetValue(guid, out user);
            if (result) 
            { 
                UpdateTime(user); 
            }
            return result;
        }

        public bool IsValidRole(Role sourceRole, Role userRole)
        {
            return userRole >= sourceRole;
        }

        private void UpdateTime(AuthUserDto authUser)
        {
            authUser.LastUpdate = DateTime.Now;
        }
    }
}
