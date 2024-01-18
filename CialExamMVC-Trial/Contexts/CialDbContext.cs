using CialExamMVC_Trial.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CialExamMVC_Trial.Contexts
{
    public class CialDbContext : IdentityDbContext
    {
        public CialDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Expert> Experts { get; set; }
    }
}
