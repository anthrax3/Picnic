using Microsoft.EntityFrameworkCore;
using Picnic.Model;

namespace Picnic.Stores.EF
{
    /// <summary>
    /// An Entity Framework DbContext for Picnic Entities
    /// </summary>
    public class PicnicDbContext : DbContext
    {
        DbSet<Content> Contents { get; set; }
        DbSet<Page> Pages { get; set; }

        /// <summary>
        /// ctor the Mighty
        /// </summary>
        public PicnicDbContext(DbContextOptions<PicnicDbContext> dbContextOptions) : 
            base((DbContextOptions)dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Content>().ToTable("Content");
            builder.Entity<Page>().ToTable("Page");
        }
    }
}