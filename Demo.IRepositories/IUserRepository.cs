using Demo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindUserByIdAsync(int id);
        Task<int> FindGenderByNameAsync(string genderName);

        Task<(Dictionary<string, int>, IEnumerable<User>)> FindAllWithFilters(int start, int limit, string q, string genderName);
    }
}
