using Microsoft.EntityFrameworkCore;
using TicketService.Models;

namespace TicketService;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Ticket> Ticket { get; set; }
}