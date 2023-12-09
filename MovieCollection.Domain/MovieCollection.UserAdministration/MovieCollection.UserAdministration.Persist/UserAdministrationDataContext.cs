using Microsoft.EntityFrameworkCore;
using MovieCollection.UserAdministration.Domain.Entities;

namespace MovieCollection.UserAdministration.Persistence
{
    public class UserAdministrationDataContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public UserAdministrationDataContext(DbContextOptions<UserAdministrationDataContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelUser(modelBuilder);
        }

        private void ModelUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
