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
    public class GenderServices : IGenderServices
    {
        private readonly IGenderRepository genderRepository;

        public GenderServices(IGenderRepository _genderRepository)
        {
            genderRepository = _genderRepository;
        }

        public async Task<IEnumerable<GenderDTO>> GetAllGendersAsync()
        {
            try
            {
                var genders = await genderRepository.FindAllAsync();
                var mappedGenders = genders.Select(x => new GenderDTO(x.GenderId, x.GenderName));
                return mappedGenders;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
