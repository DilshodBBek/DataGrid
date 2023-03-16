using DataGrid.Infrastructure.Interfaces;
using DataGrid.Models;
using Microsoft.EntityFrameworkCore;

namespace DataGrid.Infrastructure.Persistence
{
    public class ApplicationDbContext:DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }

    }
}
