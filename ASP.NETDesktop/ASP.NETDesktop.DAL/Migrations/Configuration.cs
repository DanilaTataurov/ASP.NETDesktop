using System;
using System.Linq;
using ASP.NETDesktop.DAL.Context;
using ASP.NETDesktop.Domain.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ASP.NETDesktop.DAL.Migrations {
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<NETContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NETContext context) {
            var adminExist = context.Users.Any(u => u.UserName == "admin@net.com");
            var adminRoleExist = context.Roles.Any(r => r.Name == "Admin");

            var userStore = new UserStore<User, Role, Guid, UserLogin, UserRole, UserClaim>(context);
            var userManager = new UserManager<User, Guid>(userStore);


            if (!adminRoleExist) {
                context.Roles.Add(
                    new Role {
                        Id = Guid.NewGuid(), Name = "Admin"
                    });
                context.SaveChanges();
            }

            User admin = null;
            if (!adminExist) {
                admin = new User {
                    Id = Guid.NewGuid(),
                    UserName = "admin@net.com",
                    Email = "admin@net.com",
                    CreatedAt = DateTime.UtcNow,
                    EmailConfirmed = true,
                };
                userManager.Create(admin, "1qa@WS3ed");
            } else {
                admin = userManager.FindByName("admin@net.com");
            }

            if (!userManager.IsInRole(admin.Id, "Admin")) {
                userManager.AddToRole(admin.Id, "Admin");
            }
        }
    }
}
