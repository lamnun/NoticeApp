using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace NoticeApp.Modells
{
    /// <summary>
    /// [5] 
    /// </summary>
    public class NoticeAppDbContext : DbContext
    {
        public NoticeAppDbContext ()
        {
            //empty

        }

        public NoticeAppDbContext(DbContextOptions<NoticeAppDbContext> options) : base(options)
        {
             //공식과 같은 코드??
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = ConfigurationManager.ConnectionStrings[
            "ConnectionString"].ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
                    
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notice>().Property(m => m.Created).HasDefaultValueSql("GetDate()");
        }

        public DbSet<Notice> Notices { get; set; }
    }
}
