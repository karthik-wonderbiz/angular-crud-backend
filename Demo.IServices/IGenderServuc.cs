using Demo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.IServices
{
    public interface IGenderServices
    {
        Task<IEnumerable<GenderDTO>> GetAllGendersAsync();

    }
}
