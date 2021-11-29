using System.Net.Mime;
using System;
using DashboardMVC.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DashboardMVC.Data
{
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
            ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
            ApplicationRoleClaim, ApplicationUserToken>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationGroup> ApplicationGroups { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { get; set; }
        public DbSet<ApplicationUserGroup> ApplicationUserGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationRoleClaim>().ToTable("ApplicationRoleClaim");
            builder.Entity<ApplicationUserClaim>().ToTable("ApplicationUserClaim");
            builder.Entity<ApplicationUserLogin>().ToTable("ApplicationUserLogin");
            builder.Entity<ApplicationUserRole>().ToTable("ApplicationUserRole");
            builder.Entity<ApplicationUserToken>().ToTable("ApplicationUserToken");
            builder.Entity<ApplicationRole>().ToTable("ApplicationRole");
            builder.Entity<ApplicationUser>().ToTable("ApplicationUser");

            // builder.Entity<ApplicationUser>().HasMany(ur => ur.UserRoles).WithOne(u => u.User).HasForeignKey(ur => ur.UserId).IsRequired();

            // builder.Entity<ApplicationRole>().HasMany(ur => ur.UserRoles).WithOne(u => u.Role).HasForeignKey(ur => ur.RoleId).IsRequired();


            builder
                .Entity<ApplicationUserGroup>()
                .HasKey(e => new
                {
                    e.GroupId,
                    e.UserId
                });
            builder
                .Entity<ApplicationRoleGroup>()
                .HasKey(e => new
                {
                    e.GroupId,
                    e.RoleId
                });

            builder.Entity<ApplicationUserGroup>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.UserId)
                .IsRequired();

            builder.Entity<ApplicationRoleGroup>()
                .HasOne(ur => ur.Role)
                .WithMany(u => u.RoleGroups)
                .HasForeignKey(e => e.RoleId)
                .IsRequired();
        }
    }
}