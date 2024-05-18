using Microsoft.EntityFrameworkCore;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Fifth.DbContext;

public class CasinoDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<CasinoPlayer> playersrating { get; set; }
    public DbSet<CasinoLog> pokeroperationslog { get; set; }
    private DbConnection _dbConnection;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CasinoPlayer>()
            .ToTable("playersrating")
            .HasKey(c => c.PlayerName); 

        modelBuilder.Entity<CasinoLog>()
            .ToTable("pokeroperationslog")
            .HasKey(c => c.PlayerName); 
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _dbConnection = new DbConnection();
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(_dbConnection.GetConnection());
        }
    }
}