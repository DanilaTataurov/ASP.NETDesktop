using System;
using System.Data.Entity;
using ASP.NETDesktop.DAL.DbConfig;
using ASP.NETDesktop.Domain.Entities;
using ASP.NETDesktop.Domain.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ASP.NETDesktop.DAL.Context {
    public class NETContext : IdentityDbContext<User, Role, Guid, UserLogin, UserRole, UserClaim> {
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Vacation> Vacations { get; set; }

        public NETContext() : base("DefaultConnection") { }

        public static NETContext Create() {
            return new NETContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Configurations.Add(new DeveloperConfig());
            modelBuilder.Configurations.Add(new ProjectConfig());
            modelBuilder.Configurations.Add(new VacationConfig());

            modelBuilder.Configurations.Add(new UserConfig());
            modelBuilder.Configurations.Add(new RoleConfig());
            modelBuilder.Configurations.Add(new UserLoginConfig());
            modelBuilder.Configurations.Add(new UserRoleConfig());
        }
    }
}
