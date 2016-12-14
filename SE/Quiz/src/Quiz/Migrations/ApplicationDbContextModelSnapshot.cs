using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Quiz.Data;

namespace Quiz.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Quiz.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Quiz.Models.BaiLam", b =>
                {
                    b.Property<int>("BaiLamId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DeThiId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("BaiLamId");

                    b.HasIndex("DeThiId");

                    b.HasIndex("UserId");

                    b.ToTable("BaiLams");
                });

            modelBuilder.Entity("Quiz.Models.CauHoi", b =>
                {
                    b.Property<int>("CauHoiId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("NhomId");

                    b.Property<string>("NoiDung")
                        .IsRequired();

                    b.HasKey("CauHoiId");

                    b.HasIndex("NhomId");

                    b.ToTable("CauHois");
                });

            modelBuilder.Entity("Quiz.Models.CauHoiDeThi", b =>
                {
                    b.Property<int>("CauHoiDeThiId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CauHoiId");

                    b.Property<int>("DeThiId");

                    b.HasKey("CauHoiDeThiId");

                    b.HasIndex("CauHoiId");

                    b.HasIndex("DeThiId");

                    b.ToTable("CauHoiDeThis");
                });

            modelBuilder.Entity("Quiz.Models.ChiTietBaiLam", b =>
                {
                    b.Property<int>("ChiTietBaiLamId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BaiLamId");

                    b.Property<int>("DapAnId");

                    b.HasKey("ChiTietBaiLamId");

                    b.HasIndex("BaiLamId");

                    b.HasIndex("DapAnId");

                    b.ToTable("ChiTietBaiLams");
                });

            modelBuilder.Entity("Quiz.Models.DapAn", b =>
                {
                    b.Property<int>("DapAnId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CauHoiId");

                    b.Property<bool>("IsTrue");

                    b.Property<string>("NoiDung")
                        .IsRequired();

                    b.HasKey("DapAnId");

                    b.HasIndex("CauHoiId");

                    b.ToTable("DapAns");
                });

            modelBuilder.Entity("Quiz.Models.DeThi", b =>
                {
                    b.Property<int>("DeThiId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Ten")
                        .IsRequired();

                    b.Property<TimeSpan>("ThoiGian");

                    b.HasKey("DeThiId");

                    b.ToTable("DeThis");
                });

            modelBuilder.Entity("Quiz.Models.Nhom", b =>
                {
                    b.Property<int>("NhomId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Ten")
                        .IsRequired();

                    b.HasKey("NhomId");

                    b.ToTable("Nhoms");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Quiz.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Quiz.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Quiz.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Quiz.Models.BaiLam", b =>
                {
                    b.HasOne("Quiz.Models.DeThi", "DeThi")
                        .WithMany("BaiLams")
                        .HasForeignKey("DeThiId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Quiz.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Quiz.Models.CauHoi", b =>
                {
                    b.HasOne("Quiz.Models.Nhom", "Nhom")
                        .WithMany("CauHois")
                        .HasForeignKey("NhomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Quiz.Models.CauHoiDeThi", b =>
                {
                    b.HasOne("Quiz.Models.CauHoi", "CauHoi")
                        .WithMany()
                        .HasForeignKey("CauHoiId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Quiz.Models.DeThi", "DeThi")
                        .WithMany("CauHoiDeThis")
                        .HasForeignKey("DeThiId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Quiz.Models.ChiTietBaiLam", b =>
                {
                    b.HasOne("Quiz.Models.BaiLam")
                        .WithMany("ChiTietBaiLams")
                        .HasForeignKey("BaiLamId");

                    b.HasOne("Quiz.Models.DapAn", "DapAn")
                        .WithMany()
                        .HasForeignKey("DapAnId");
                });

            modelBuilder.Entity("Quiz.Models.DapAn", b =>
                {
                    b.HasOne("Quiz.Models.CauHoi", "CauHoi")
                        .WithMany("DapAns")
                        .HasForeignKey("CauHoiId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
