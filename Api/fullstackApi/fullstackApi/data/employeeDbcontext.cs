using fullstackApi.models;
using Microsoft.EntityFrameworkCore;

namespace fullstackApi.data
{
    public class EmployeeDbcontext : DbContext
    {
        public EmployeeDbcontext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }

    }
}
