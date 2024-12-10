using Microsoft.EntityFrameworkCore;
using OS_and_BD_lab_2.Models;
using static System.Net.Mime.MediaTypeNames;

namespace OS_and_BD_lab_2
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
    }
}
