using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Quiz.Models;

namespace Quiz.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BaiLam> BaiLams { get; set; }

        public DbSet<CauHoiDeThi> CauHoiDeThis { get; set; }
        public DbSet<CauHoi> CauHois { get; set; }
        public DbSet<ChiTietBaiLam> ChiTietBaiLams { get; set; }

        public DbSet<DapAn> DapAns { get; set; }

        public DbSet<DeThi> DeThis { get; set; }

        public DbSet<Nhom> Nhoms { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ChiTietBaiLam>()
                .HasOne<DapAn>(ctbl => ctbl.DapAn)
                .WithMany()
                .HasForeignKey(ctbl => ctbl.DapAnId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}