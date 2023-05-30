using Computers.Models;
using Computers.Models.Dto;

namespace Computers.Repository
{
    public interface IAuthorizationRepository
    {
        /// <summary>
        /// Берёт данные авторизированного пользователя по его guid
        /// </summary>
        /// <param name="guid">Guid пользователя</param>
        /// <param name="user"></param>
        /// <returns></returns>
        bool GetAuthorizationUser(Guid guid, out AuthUserDto user);

        /// <summary>
        /// Проверяет доступность действия с текущей ролью пользователя
        /// </summary>
        /// <param name="roleSource">Роль требуемая для действия</param>
        /// <param name="userRole">Текущая роль пользователя</param>
        /// <returns></returns>
        bool IsValidRole(Role roleSource, Role userRole);

        /// <summary>
        /// Заносит в список авторизованых пользователей
        /// </summary>
        /// <param name="user">Данные пользователя</param>
        /// <param name="authorizationUser">Данные авторизованного пользователя</param>
        /// <returns></returns>
        bool Authorize(User user, out AuthUserDto authorizationUser);

        /// <summary>
        /// Удаляет всех авторизованных пользователей по истечению срока сессии
        /// </summary>
        void DeleteAutorizationUsers();
    }
}
