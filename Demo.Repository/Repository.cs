using Demo.Data;
using Demo.IRepositories;
using Demo.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Demo.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DemoDBContext context;

        public Repository(DemoDBContext _context)
        {
            context = _context;
        }

        public async Task<T> CreateAsync(T t)
        {
            try
            {
                var addT = await context.Set<T>().AddAsync(t);
                var savedT = await context.SaveChangesAsync();
                return addT.Entity;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(T t)
        {
            try
            {
                context.Set<T>().Remove(t);
                int row = await context.SaveChangesAsync();
                return row > 0;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync(int start, int limit, string q)
        {
            try
            {
                var skip = (start - 1) * limit;
                
                var tList = await context.Set<T>().Skip(skip).Take(limit).ToListAsync();
                return tList;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public virtual async Task<T> FindByIdAsync(int id)
        {
            try
            {
                var t = await context.Set<T>().FindAsync(id);
                if (t != null) { return t; }
                throw new KeyNotFoundException($"No data with Id:{id}");
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<T> UpdateAsync(T t)
        {
            try
            {
                var savedT = context.Update(t);
                var isUpdaed = await context.SaveChangesAsync();
                return savedT.Entity;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        
    }
}
