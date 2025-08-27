using Microsoft.EntityFrameworkCore;
using StatsApi.Models;

namespace StatsApi.Data
{
    public class StatsDbContext : DbContext
    {
        public StatsDbContext(DbContextOptions<StatsDbContext> options) : base(options) { }

        public DbSet<MatchStats> MatchStats { get; set; }
    }
}
