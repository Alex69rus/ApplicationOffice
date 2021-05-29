﻿// <auto-generated />
using System;
using ApplicationOffice.Approvals.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApplicationOffice.Approvals.Data.Migrations
{
    [DbContext(typeof(ApprovalsDbContext))]
    partial class ApprovalsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("ApplicationOffice.Approvals.Data.Entities.Application", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("applications");
                });

            modelBuilder.Entity("ApplicationOffice.Approvals.Data.Entities.ApplicationApprover", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("ApplicationId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("UserId");

                    b.ToTable("application_approvers");
                });

            modelBuilder.Entity("ApplicationOffice.Approvals.Data.Entities.ApplicationField", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("ApplicationId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("application_fields");
                });

            modelBuilder.Entity("ApplicationOffice.Approvals.Data.Entities.Unit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("units");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            ModifiedAt = new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Title = "Отдел ИТ-разработки"
                        });
                });

            modelBuilder.Entity("ApplicationOffice.Approvals.Data.Entities.UnitApprover", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("ApproverId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UnitId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ApproverId");

                    b.HasIndex("UnitId");

                    b.ToTable("unit_approvers");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ApproverId = 1L,
                            CreatedAt = new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            ModifiedAt = new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Title = "Руководитель отдела",
                            UnitId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            ApproverId = 2L,
                            CreatedAt = new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            ModifiedAt = new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Title = "Отдел кадров",
                            UnitId = 1L
                        });
                });

            modelBuilder.Entity("ApplicationOffice.Approvals.Data.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("UnitId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UnitId");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            ModifiedAt = new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Name = "Петров Пётр Петрович",
                            UnitId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            CreatedAt = new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            ModifiedAt = new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Name = "Иванов Иван Иванович",
                            UnitId = 1L
                        },
                        new
                        {
                            Id = 3L,
                            CreatedAt = new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            ModifiedAt = new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Name = "Алексеев Алексей Алексеевич",
                            UnitId = 1L
                        });
                });

            modelBuilder.Entity("ApplicationOffice.Approvals.Data.Entities.Application", b =>
                {
                    b.HasOne("ApplicationOffice.Approvals.Data.Entities.User", "Author")
                        .WithMany("CreatedApplications")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("ApplicationOffice.Approvals.Data.Entities.ApplicationApprover", b =>
                {
                    b.HasOne("ApplicationOffice.Approvals.Data.Entities.Application", "Application")
                        .WithMany("Approvers")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ApplicationOffice.Approvals.Data.Entities.User", "User")
                        .WithMany("Assignees")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Application");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ApplicationOffice.Approvals.Data.Entities.ApplicationField", b =>
                {
                    b.HasOne("ApplicationOffice.Approvals.Data.Entities.Application", "Application")
                        .WithMany("Fields")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Application");
                });

            modelBuilder.Entity("ApplicationOffice.Approvals.Data.Entities.UnitApprover", b =>
                {
                    b.HasOne("ApplicationOffice.Approvals.Data.Entities.User", "Approver")
                        .WithMany()
                        .HasForeignKey("ApproverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationOffice.Approvals.Data.Entities.Unit", "Unit")
                        .WithMany("Approvers")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Approver");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("ApplicationOffice.Approvals.Data.Entities.User", b =>
                {
                    b.HasOne("ApplicationOffice.Approvals.Data.Entities.Unit", "Unit")
                        .WithMany("Employees")
                        .HasForeignKey("UnitId");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("ApplicationOffice.Approvals.Data.Entities.Application", b =>
                {
                    b.Navigation("Approvers");

                    b.Navigation("Fields");
                });

            modelBuilder.Entity("ApplicationOffice.Approvals.Data.Entities.Unit", b =>
                {
                    b.Navigation("Approvers");

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("ApplicationOffice.Approvals.Data.Entities.User", b =>
                {
                    b.Navigation("Assignees");

                    b.Navigation("CreatedApplications");
                });
#pragma warning restore 612, 618
        }
    }
}
