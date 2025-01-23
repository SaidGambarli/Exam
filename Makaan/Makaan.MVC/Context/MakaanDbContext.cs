using Makaan.MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Makaan.MVC.Context;

public class MakaanDbContext : IdentityDbContext
{
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Agent> Agents { get; set; }
    public MakaanDbContext(DbContextOptions opt) : base(opt) {}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MakaanDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
