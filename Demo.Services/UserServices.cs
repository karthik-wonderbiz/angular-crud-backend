using Demo.DTO;
using Demo.IRepositories;
using Demo.IServices;
using Demo.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Demo.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository userRepository;

        public UserServices(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserDTO createUserDTO)
        {
            try
            {
                var user = new User();
                user.Name = createUserDTO.Name;
                user.Email = createUserDTO.Email;
                user.IsActive = createUserDTO.IsActive;
                user.GenderId = createUserDTO.GenderId;
                user.CreatedBy = createUserDTO.CreatedBy;
                user.UpdatedBy = createUserDTO.CreatedBy;
                user.CreatedDate = DateTimeOffset.Now;
                user.UpdatedDate = DateTimeOffset.Now;

                var newUser = await userRepository.CreateAsync(user);
                var populatedUser = await userRepository.FindByIdAsync(newUser.UserId);
                var mappedUser = new UserDTO(
                populatedUser.UserId,
                    populatedUser.Name,
                    populatedUser.Email,
                    populatedUser.IsActive,
                    populatedUser.Gender.GenderName,
                    populatedUser.CreatedDate
                );
                return mappedUser;

            }
            catch (DbUpdateException d)
            {
                throw new DbUpdateException(d.InnerException?.Message ?? d.Message);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var oldUser = await userRepository.FindByIdAsync(id);
                if (oldUser != null)
                {
                    var isDeleted = await userRepository.DeleteAsync(oldUser);
                    return isDeleted;
                }
                throw new KeyNotFoundException($"No user with Id: {id}");
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync(int start, int limit, string q, string filter)
        {
            try
            {
                int roleId;
                start = start == 0 ? 1 : start;
                limit = limit == 0 ? 10 : limit;
                q = q ?? "";
                roleId = await userRepository.FindGenderByNameAsync(filter);
                var users = await userRepository.FindAllWithFilters(start, limit, q, roleId);
                var mappedUsers = users.Select(x => new UserDTO(
                    x.UserId,
                    x.Name,
                    x.Email,
                    x.IsActive,
                    x.Gender.GenderName,
                    x.CreatedDate
                ));
                return mappedUsers;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await userRepository.FindByIdAsync(id);
                if (user != null)
                {
                    var mappedUser = new UserDTO(
                        user.UserId,
                        user.Name,
                        user.Email,
                        user.IsActive,
                        user.Gender.GenderName,
                        user.CreatedDate
                    );
                    return mappedUser;
                }
                throw new KeyNotFoundException($"No user with Id: {id}");
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<UserDTO> UpdateUserAsync(int id, UpdateUserDTO updateUserDTO)
        {
            try
            {
                var oldUser = await userRepository.FindUserByIdAsync(id);
                if (oldUser != null)
                {
                    oldUser.Name = updateUserDTO.Name ?? oldUser.Name;
                    oldUser.Email = updateUserDTO.Email ?? oldUser.Email;
                    oldUser.IsActive = updateUserDTO.IsActive ?? oldUser.IsActive;
                    oldUser.GenderId = updateUserDTO.GenderId == oldUser.GenderId ? oldUser.GenderId : updateUserDTO.GenderId;
                    oldUser.UpdatedBy = updateUserDTO.UpdatedBy;
                    oldUser.UpdatedDate = DateTimeOffset.Now;
                    var updatedUser = await userRepository.UpdateAsync(oldUser);
                    var populatedUser = await userRepository.FindByIdAsync(updatedUser.UserId);
                    var mappedUser = new UserDTO(
                        populatedUser.UserId,
                        populatedUser.Name,
                        populatedUser.Email,
                        populatedUser.IsActive,
                        populatedUser.Gender.GenderName,
                        populatedUser.CreatedDate
                    );
                    return mappedUser;
                }
                throw new KeyNotFoundException($"No user with Id: {id}");
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
