using Microsoft.EntityFrameworkCore;
using MovieCollection.Movie.Domain.Entities;
using MovieCollection.Movie.Domain.Enums;

namespace MovieCollection.Movie.Persistence
{
    public class MovieDataContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Collection> Collection { get; set; }
        public DbSet<Domain.Entities.Movie> Movie { get; set; }
        public DbSet<Domain.Entities.MovieCollection> MovieCollection { get; set; }

        public MovieDataContext(DbContextOptions<MovieDataContext> options) : base(options)
        {
            // To avoid this issue: Cannot write DateTime with Kind=UTC to PostgreSQL type 'timestamp without time zone'
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelUser(modelBuilder);
            ModelCollection(modelBuilder);
            ModelMovie(modelBuilder);
            ModelMovieCollection(modelBuilder);
        }

        private void ModelUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().HasData(
                new User(1, "test@mail.com", "AQAAAAEAACcQAAAAEAap7bv4XkwO9GMc9E19yA5qcnHJYwttBDlZmUODzn/h2Bx6DQOl5VMOg09am5cAWA==", "Super", "Admin", UserRole.Admin, new DateTime(2024, 01, 01)) //pass: Test@123
                );
        }

        private void ModelCollection(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Collection>().HasKey(x => x.Id);
            modelBuilder.Entity<Collection>().Property(x => x.Id).ValueGeneratedOnAdd();
        }

        private void ModelMovie(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Movie>().HasKey(x => x.Id);
            modelBuilder.Entity<Domain.Entities.Movie>().Property(x => x.Id).ValueGeneratedOnAdd();
        }

        private void ModelMovieCollection(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.MovieCollection>()
                      .HasKey(x => new { x.CollectionId, x.MovieId });

            modelBuilder.Entity<Domain.Entities.MovieCollection>()
                 .HasOne(bc => bc.Movie)
                 .WithMany(b => b.MovieCollection)
                 .HasForeignKey(bc => bc.MovieId);

            modelBuilder.Entity<Domain.Entities.MovieCollection>()
                .HasOne(bc => bc.Collection)
                .WithMany(c => c.MovieCollection)
                .HasForeignKey(bc => bc.CollectionId);
        }
    }
}
