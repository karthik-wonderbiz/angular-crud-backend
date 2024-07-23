using Demo.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Demo.Data
{
    public class DemoDBContext : DbContext
    {
        public DemoDBContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Gender> Genders { get; set; }

    }
}
