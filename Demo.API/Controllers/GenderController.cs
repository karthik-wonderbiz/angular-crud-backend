using Demo.DTO;
using Demo.IRepositories;
using Demo.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IGenderServices genderService;

        public GenderController(IGenderServices _genderService)
        {
            genderService = _genderService;
        }

        // GET: api/<GenderController>
        [HttpGet]
        public async Task <ActionResult<IEnumerable<GenderDTO>>> Get()
        {
            try
            {
                var genders = await genderService.GetAllGendersAsync();
                return Ok(genders);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.InnerException?.Message ?? e.Message}");
                throw;
            }
        }
    }
}
