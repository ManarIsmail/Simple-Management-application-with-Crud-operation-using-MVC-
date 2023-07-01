using Demo.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Contexts
{
    public class MVCAPPDBContext : IdentityDbContext< ApplicationUser,ApplicationRole, string>
    {
        public MVCAPPDBContext(DbContextOptions<MVCAPPDBContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          => optionsBuilder.UseSqlServer("server=KIRITO\\MSSQLSERVER2 ; database=MVCAppDb; integrated security=true; MultipleActiveResultSets= true;");

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
