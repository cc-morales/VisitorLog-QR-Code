using Microsoft.EntityFrameworkCore;
using VisitorLog.Models;

namespace VisitorLog.ApplicationDBContextService
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<VisitorModel> Visitors { get; set; } = null!;
        public DbSet<QRCodeModel> QRCodes { get; set; } = null!;
        public DbSet<QRSetModel> QRSets { get; set; } = null!;
        public DbSet<LogModel> Logs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ensure explicit table names (also set via attributes on models)
            modelBuilder.Entity<VisitorModel>().ToTable("visitors");
            modelBuilder.Entity<QRCodeModel>().ToTable("qrcodes");
            modelBuilder.Entity<QRSetModel>().ToTable("qrsets");
            modelBuilder.Entity<LogModel>().ToTable("logs");

            base.OnModelCreating(modelBuilder);
        }
    }
}

