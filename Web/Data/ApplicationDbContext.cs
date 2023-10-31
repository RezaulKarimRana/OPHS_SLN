using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<Institute> Institute { get; set; }
        public DbSet<Notice> Notice { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<AboutOurs> AboutOurs { get; set; }
        public DbSet<Speech> Speech { get; set; }
    }
}