using Microsoft.EntityFrameworkCore;
using MoviesApp.Data.Interfaces;
using MoviesApp.Data.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Domain.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly MoviesContext _context;

        public GenericRepository(MoviesContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
           var entity = await GetByIdAsync(id);
            if(entity != null)
             _context.Set<T>().Remove(entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
           _context.Entry(entity).State = EntityState.Modified;  
        }
        public async Task SaveAsync()
        {
          await _context.SaveChangesAsync();
        }
    }
}
