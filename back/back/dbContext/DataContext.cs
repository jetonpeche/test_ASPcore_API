using back.model;
using Microsoft.EntityFrameworkCore;

namespace back.dbContext
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        // Add-Migration
        // update-Database

        public DbSet<Departement> departement { get; set; }
        public DbSet<Pain> pain { get; set; }
    }
}
