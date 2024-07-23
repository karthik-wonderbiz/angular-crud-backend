using Demo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.IServices
{
    public interface IUserServices
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync(int start, int limit, string q, string filter);

        Task<UserDTO> GetUserByIdAsync(int id);

        Task<bool> DeleteUserAsync(int id);

        Task<UserDTO> UpdateUserAsync(int id, UpdateUserDTO updateUserDTO);

        Task<UserDTO> CreateUserAsync(CreateUserDTO createUserDTO);
    }
}
