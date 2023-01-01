using Microsoft.EntityFrameworkCore;

namespace GameRating.Models
{
    public class HomeContext : DbContext  {
       private readonly IConfiguration _configuration;

        public HomeContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSQL"));

        }
        public DbSet<User> Kullanicilar { get; set; }
        public DbSet<Game> Oyunlar { get; set; }
        public DbSet<GameComment> Yorumlar { get; set; }

    }

}