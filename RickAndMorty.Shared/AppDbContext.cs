using Microsoft.EntityFrameworkCore;
using RickAndMorty.Shared.Models;

namespace RickAndMorty.Shared;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Character> Characters { get; set; }
}
