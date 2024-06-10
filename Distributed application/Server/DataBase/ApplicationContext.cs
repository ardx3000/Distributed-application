using Microsoft.EntityFrameworkCore;
using Server.DataBase.Entity;
using Server.DataBase.UserCalsses;

namespace Server.DataBase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Logs> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=users.db");
        }
    }
}
