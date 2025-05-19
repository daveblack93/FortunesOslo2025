using Microsoft.EntityFrameworkCore;

namespace Fortunes.Data;

public class FortuneContext : DbContext
{
    protected FortuneContext()
    {
    }

    public FortuneContext(DbContextOptions<FortuneContext> options) : base(options)
    {
    }

    public DbSet<Fortune> Fortunes { get; set; }
}