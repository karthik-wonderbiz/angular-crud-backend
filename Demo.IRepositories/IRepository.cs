using Demo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> FindByIdAsync(int id);

        Task<IEnumerable<T>> FindAllAsync(int start, int limit, string q);

        Task<bool> DeleteAsync(T t);

        Task<T> UpdateAsync(T t);

        Task<T> CreateAsync(T t);
    }
}
