using Courseproject.Common.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Infrastructure;

public class ApplicationDbContext:IdentityDbContext<IdentityUser,IdentityRole,string>
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Address>().HasKey(u=>u.Id);
        builder.Entity<Employee>().HasKey(u=>u.Id);
        builder.Entity<Team>().HasKey(u=>u.Id);
        builder.Entity<Job>().HasKey(u=>u.Id);

        builder.Entity<Employee>().HasOne(u => u.Address);
        builder.Entity<Employee>().HasOne(u => u.Job);

        builder.Entity<Team>().HasMany(u => u.Employees).WithMany(e=>e.Teams);


        base.OnModelCreating(builder);
    }
}
