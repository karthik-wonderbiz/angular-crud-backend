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
    public class GenderRepository : Repository<Gender>, IGenderRepository
    {
        public GenderRepository(DemoDBContext _context) : base(_context)
        {
        }

    }
}
