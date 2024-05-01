﻿using Microsoft.EntityFrameworkCore;
using MODEL.Models;

namespace DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserAssigmnent> UserAssigmnents { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Repository> Repository { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRepository> UsersRepository { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAssigmnent>()
                 .HasKey(au => new { au.UserID, au.AssigmnentID });
            modelBuilder.Entity<UserAssigmnent>()
                .HasOne(a => a.Assignment)
                .WithMany(au => au.UserAssigmnents)
                .HasForeignKey(a => a.AssigmnentID);
            modelBuilder.Entity<UserAssigmnent>()
                .HasOne(u => u.User)
                .WithMany(au => au.UserAssigmnents)
                .HasForeignKey(u => u.UserID);
            modelBuilder.Entity<UserRepository>()
                .HasKey(ur => new { ur.UserID, ur.RepositoryID });
            modelBuilder.Entity<UserRepository>()
                .HasOne(r => r.Repository)
                .WithMany(ur => ur.UserRepositories)
                .HasForeignKey(r => r.RepositoryID);
            modelBuilder.Entity<UserRepository>()
                .HasOne(u => u.User)
                .WithMany(ur => ur.UserRepositories)
                .HasForeignKey(u => u.UserID);
        }
    }
}
