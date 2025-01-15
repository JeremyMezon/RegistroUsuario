using Microsoft.EntityFrameworkCore;
using RegistroUsuario.Models;

namespace RegistroUsuario.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Phone> Phones { get; set; }
    }
}
