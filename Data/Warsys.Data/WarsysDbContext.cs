using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Warsys.Data.Models;

namespace Warsys.Data
{
    public class WarsysDbContext : IdentityDbContext<WarsysUser, IdentityRole, string>
    {
        public WarsysDbContext(DbContextOptions<WarsysDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
