using Computers.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Computers.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly DataBase _context;

        public UserRepository(DataBase context)
        {
            _context = context;
        }

        public async Task AddAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(Expression<Func<User, bool>> condition)
        {
            var user = await _context.Users.FirstOrDefaultAsync(condition) 
                ?? throw new ArgumentNullException("User does not exist");

            _context.Users.Remove(user);
            await SaveAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>> condition)
        {
            IQueryable<User> users = _context.Users;
            return await users.Where(condition).ToListAsync();
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> condition)
        {
            return await _context.Users.FirstOrDefaultAsync(condition);
        }

        public async Task<IEnumerable<User>> GetFirstAsync(Expression<Func<User, bool>> condition, int limit)
        {
            return await _context.Users.OrderBy(x => x.Id).Where(condition).Take(limit).AsNoTracking().ToListAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            await SaveAsync();
        }

        private async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
