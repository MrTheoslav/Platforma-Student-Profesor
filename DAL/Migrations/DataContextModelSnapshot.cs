﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("MODEL.Models.AssigmnentUser", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AssigmnentID")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsCreated")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("SendDate")
                        .HasColumnType("DATETIME");

                    b.HasKey("UserID", "AssigmnentID");

                    b.HasIndex("AssigmnentID");

                    b.ToTable("AssigmnentUsers");
                });

            modelBuilder.Entity("MODEL.Models.Assignment", b =>
                {
                    b.Property<int>("AssignmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("DATETIME");

                    b.Property<int>("FileSize")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Mark")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RepositoryID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("DATETIME");

                    b.HasKey("AssignmentID");

                    b.HasIndex("RepositoryID");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("MODEL.Models.Repository", b =>
                {
                    b.Property<int>("RepositoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("RepositoryID");

                    b.ToTable("Repository");
                });

            modelBuilder.Entity("MODEL.Models.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("isApproved")
                        .HasColumnType("INTEGER");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("MODEL.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EnterDate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserFirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserLastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserID");

                    b.HasIndex("RoleID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MODEL.Models.UserRepository", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RepositoryID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EnterDate")
                        .HasColumnType("DATETIME");

                    b.Property<bool>("HasPrivilage")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsMember")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserID", "RepositoryID");

                    b.HasIndex("RepositoryID");

                    b.ToTable("UsersRepository");
                });

            modelBuilder.Entity("MODEL.Models.AssigmnentUser", b =>
                {
                    b.HasOne("MODEL.Models.Assignment", "Assignment")
                        .WithMany("AssigmnentUsers")
                        .HasForeignKey("AssigmnentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MODEL.Models.User", "User")
                        .WithMany("AssigmnentUsers")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MODEL.Models.Assignment", b =>
                {
                    b.HasOne("MODEL.Models.Repository", "Repository")
                        .WithMany("Assignments")
                        .HasForeignKey("RepositoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Repository");
                });

            modelBuilder.Entity("MODEL.Models.User", b =>
                {
                    b.HasOne("MODEL.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("MODEL.Models.UserRepository", b =>
                {
                    b.HasOne("MODEL.Models.Repository", "Repository")
                        .WithMany("UserRepositories")
                        .HasForeignKey("RepositoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MODEL.Models.User", "User")
                        .WithMany("UserRepositories")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Repository");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MODEL.Models.Assignment", b =>
                {
                    b.Navigation("AssigmnentUsers");
                });

            modelBuilder.Entity("MODEL.Models.Repository", b =>
                {
                    b.Navigation("Assignments");

                    b.Navigation("UserRepositories");
                });

            modelBuilder.Entity("MODEL.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("MODEL.Models.User", b =>
                {
                    b.Navigation("AssigmnentUsers");

                    b.Navigation("UserRepositories");
                });
#pragma warning restore 612, 618
        }
    }
}
