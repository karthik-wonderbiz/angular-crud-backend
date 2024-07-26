using Demo.DTO;
using Demo.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices userServices;

        public UserController(IUserServices _userServices)
        {
            userServices = _userServices;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<PaginatedUsersDTO>> Get([FromQuery] int start, [FromQuery] int limit, [FromQuery] string? q, [FromQuery] string? filter)
        {
            try
            {
                var users = await userServices.GetAllUsersAsync(start, limit, q, filter);
                return Ok(users);
            }
            catch (Exception e)
            {
                if (e is KeyNotFoundException) return NotFound(e.Message);
                return StatusCode(500, $"Internal Server error: {e.Message}");
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get(int id)
        {
            try
            {
                var user = await userServices.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                if (e is KeyNotFoundException) return NotFound(e.Message);
                return StatusCode(500, $"Internal Server error: {e.Message}");
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Post([FromBody] CreateUserDTO createUserDTO)
        {
            try
            {
                var createdUser = await userServices.CreateUserAsync(createUserDTO);
                return Ok(createdUser);
            }
            catch (Exception e)
            {
                if (e is DbUpdateException || e is ArgumentException)
                {
                    return StatusCode(400, $"Bad request: {e.InnerException?.Message ?? e.Message}");
                }
                return StatusCode(500, $"Internal Server error: {e.Message}");
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDTO>> Put(int id, [FromBody] UpdateUserDTO updateUserDTO)
        {
            try
            {
                var user = await userServices.UpdateUserAsync(id, updateUserDTO);
                return Ok(user);
            }
            catch (Exception e)
            {
                if (e is DbUpdateException || e is ArgumentException)
                {
                    return StatusCode(400, $"Bad request: {e.InnerException?.Message ?? e.Message}");
                }

                if (e is KeyNotFoundException) return NotFound(e.Message);

                return StatusCode(500, $"Internal Server error: {e.Message}");
            }
        }


        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                var isDeleted = await userServices.DeleteUserAsync(id);
                return Ok(isDeleted);
            }
            catch (Exception e)
            {
                if (e is KeyNotFoundException) return NotFound(e.Message);
                return StatusCode(500, $"Internal Server error: {e.Message}");
            }
        }
    }
}
