using Computers.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Computers.Repository
{
    public class ComputerRepository : IRepository<Computer>
    {
        private readonly DataBase _context;

        public ComputerRepository(DataBase context)
        {
            _context = context;
        }

        public async Task AddAsync(Computer entity)
        {
            await _context.Computers.AddAsync(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(Expression<Func<Computer, bool>> condition)
        {
            var computers = _context.Computers.Where(condition);
            _context.Computers.RemoveRange(computers);
            await SaveAsync();
        }

        public async Task<IEnumerable<Computer>> GetAllAsync()
        {
            return await _context.Computers.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Computer>> GetAllAsync(Expression<Func<Computer, bool>> condition)
        {
            IQueryable<Computer> computers = _context.Computers;
            return await computers.Where(condition).AsNoTracking().ToListAsync();
        }

        public async Task<Computer> GetAsync(Expression<Func<Computer, bool>> condition)
        {
            return await _context.Computers.FirstOrDefaultAsync(condition);
        }

        public async Task<IEnumerable<Computer>> GetFirstAsync(Expression<Func<Computer, bool>> condition, int limit)
        {
            return await _context.Computers.OrderBy(x => x.Id).Where(condition).Take(limit).AsNoTracking().ToListAsync();
        }

        public async Task UpdateAsync(Computer entity)
        {
            _context.Computers.Update(entity);
            await SaveAsync();
        }

        private async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
