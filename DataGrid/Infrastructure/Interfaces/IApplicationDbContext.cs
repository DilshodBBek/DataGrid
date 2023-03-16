using DataGrid.Models;
using Microsoft.EntityFrameworkCore;

namespace DataGrid.Infrastructure.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Employee> Employees { get; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
