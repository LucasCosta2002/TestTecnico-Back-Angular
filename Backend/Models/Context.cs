using Microsoft.EntityFrameworkCore;
namespace Backend.Models
{
    public class Context : DbContext
    {

        public DbSet<Mascota> Mascotas { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
        }

    }
}
