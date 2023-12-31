﻿using Microsoft.EntityFrameworkCore;
using OutBoxPattern.Sample.Models;

namespace OutBoxPattern.Sample;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().Property(o => o.Price).HasColumnType("decimal(18,4)");
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Order> Orders { get; set; }
    public DbSet<EmailOutbox> EmailOutbox { get; set; }
}