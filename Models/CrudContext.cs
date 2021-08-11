using Microsoft.EntityFrameworkCore;

namespace Crud.Models
{
    public class CrudContext : DbContext
    {
        public CrudContext(DbContextOptions options) : base(options) { }
        public DbSet<Dishes> Dishes { get; set; }

    }
}