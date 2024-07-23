using Demo.Data;
using Demo.IRepositories;
using Demo.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DemoDBContext context;
        public UserRepository(DemoDBContext _context) : base(_context)
        {
            context = _context;
        }

        public override async Task<User> FindByIdAsync(int id)
        {
            try
            {
                var user = await context.Users.Include(g => g.Gender).FirstOrDefaultAsync(u => u.UserId == id);
                if (user != null)
                {
                    return user;
                }
                throw new KeyNotFoundException($"No user with Id: {id}");
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<User> FindUserByIdAsync(int id)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.UserId == id);
                if (user != null)
                {
                    return user;
                }
                throw new KeyNotFoundException($"No user with Id: {id}");
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public override async Task<IEnumerable<User>> FindAllAsync(int start, int limit, string q)
        {
            try
            {
                var skip = (start - 1) * limit;
                var users = await context.Users.Where(e => e.Name.StartsWith(q)).Skip(skip).Take(limit).Include(g => g.Gender).ToListAsync();
                return users;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> FindAllWithFilters(int start, int limit, string q, int genderId)
        {
            try
            {
                var skip = (start - 1) * limit;
                var users = await context.Users.Where(e => e.Name.StartsWith(q)).Where(e=>e.GenderId.Equals(genderId)).Skip(skip).Take(limit).Include(g => g.Gender).ToListAsync();
                return users;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<int> FindGenderByNameAsync(string genderName)
        {
            try
            {
                var gender = await context.Genders.FirstOrDefaultAsync(e => e.GenderName.Contains(genderName));
                if (gender != null)
                {
                    return gender.GenderId;
                }
                return 0;
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
