namespace GamesZone.Data.ApplicationDbContext
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Games> Games { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<GameDevice> GameDevices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category[]
                {
                new Category { Id = 1, Name = "Action" },
                new Category { Id = 2, Name = "Adventure" },
                new Category { Id = 3, Name = "RPG" },
                new Category { Id = 4, Name = "Simulation" },
                new Category { Id = 5, Name = "Strategy" },
                new Category { Id = 6, Name = "Sports" },
                new Category { Id = 7, Name = "Puzzle" },
                new Category { Id = 8, Name = "Idle" },
                new Category { Id = 9, Name = "Casual" },
                new Category { Id = 10, Name = "Arcade" },
                new Category { Id = 11, Name = "Racing" },
                new Category { Id = 12, Name = "Horror" },
                new Category { Id = 13, Name = "Shooter" },
                new Category { Id = 14, Name = "Fighting" },
                new Category { Id = 15, Name = "Survival" },
                new Category { Id = 16, Name = "MMO" },
                new Category { Id = 17, Name = "Battle Royale" },
                new Category { Id = 18, Name = "Tower Defense" },
                new Category { Id = 19, Name = "Card" },
                new Category { Id = 20, Name = "Music" },
                new Category { Id = 21, Name = "Educational" },
                new Category { Id = 22, Name = "Trivia" },
                new Category { Id = 23, Name = "Board" },
                new Category { Id = 24, Name = "Word" },
                new Category { Id = 25, Name = "Casino" },
                new Category { Id = 26, Name = "Rhythm" },
                new Category { Id = 27, Name = "Poker" },
                new Category { Id = 28, Name = "Idle" },
                new Category { Id = 29, Name = "Simulation" },
                new Category { Id = 30, Name = "Strategy" },
                new Category { Id = 31, Name = "Sports" },
                new Category { Id = 32, Name = "Puzzle" }, });

            modelBuilder.Entity<Device>().HasData(new Device[]
                {
                    new Device { Id = 1, Name = "PC", Icon = "fas fa-desktop" },
                    new Device { Id = 2, Name = "PlayStation", Icon = "fab fa-playstation" },
                    new Device { Id = 3, Name = "Xbox", Icon = "fab fa-xbox" },
                    new Device { Id = 4, Name = "Nintendo", Icon = "fas fa-gamepad" },

                });

            modelBuilder.Entity<GameDevice>()
                .HasKey(gd => new { gd.GameId, gd.DeviceId });
        }
    }
}
