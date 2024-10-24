using Capstone.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Capstone.Common;

public class CapstoneContext : DbContext
{
    public DbSet<Congress> Congress { get; set; }
    public DbSet<CongressMember> CongressMember { get; set; }
    public DbSet<Bill> Bill { get; set; }
    
    public CapstoneContext(DbContextOptions<CapstoneContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Congress>().HasAlternateKey(c => c.Number);
        //modelBuilder.Entity<CongressMember>().HasAlternateKey(c => c.BioGuideId);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
}