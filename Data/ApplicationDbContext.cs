using System;
using DashboardMVC.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DashboardMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, IdentityUserClaim<Guid>, ApplicationUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public DbSet<ApplicationGroup> ApplicationGroups { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { get; set; }
        public DbSet<ApplicationUserGroup> ApplicationUserGroups { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUserGroup>().HasKey(e => new
            {
                e.GroupId,
                e.UserId
            });
            builder.Entity<ApplicationRoleGroup>().HasKey(e => new
            {
                e.GroupId,
                e.RoleId
            });
            builder.Entity<ApplicationUser>()
                .HasMany(ur => ur.UserGroups)
                .WithOne(u => u.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
            builder.Entity<ApplicationUser>()
                .HasMany(ur => ur.UserGroups)
                .WithOne(u => u.User)
                .HasForeignKey(e => e.GroupId)
                .IsRequired();

            builder.Entity<ApplicationRole>()
                .HasMany(ur => ur.RoleGroups)
                .WithOne(u => u.Role)
                .HasForeignKey(e => e.RoleId)
                .IsRequired();
            builder.Entity<ApplicationRole>()
                .HasMany(ur => ur.RoleGroups)
                .WithOne(u => u.Role)
                .HasForeignKey(e => e.GroupId)
                .IsRequired();

            base.OnModelCreating(builder);
        }
    }
}