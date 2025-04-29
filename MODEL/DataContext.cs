using Microsoft.EntityFrameworkCore;
using MODEL.Entities;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace MODEL;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Transaction> Trans { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Transfer> Transfers { get; set; }

}
