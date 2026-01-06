using System.Collections.Generic;
using System.Net;
using backEndDbConnection.Models;
using Microsoft.EntityFrameworkCore;


namespace backEndDbConnection.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}