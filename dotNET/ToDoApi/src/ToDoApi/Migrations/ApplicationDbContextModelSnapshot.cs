using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ToDoApi.Data;

namespace ToDoApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-preview1-22509");

            modelBuilder.Entity("ToDoApi.Models.Account", b =>
                {
                    b.Property<string>("AccountId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FullName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Phone");

                    b.HasKey("AccountId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ToDoApi.Models.Event", b =>
                {
                    b.Property<string>("EventId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountId");

                    b.Property<string>("Description");

                    b.Property<string>("EventName");

                    b.Property<string>("Location");

                    b.Property<DateTime>("Start");

                    b.HasKey("EventId");

                    b.HasIndex("AccountId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("ToDoApi.Models.Event", b =>
                {
                    b.HasOne("ToDoApi.Models.Account", "Account")
                        .WithMany("Events")
                        .HasForeignKey("AccountId");
                });
        }
    }
}
