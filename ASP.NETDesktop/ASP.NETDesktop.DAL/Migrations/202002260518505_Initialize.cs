namespace ASP.NETDesktop.DAL.Migrations {
    using System.Data.Entity.Migrations;

    public partial class Initialize : DbMigration {
        public override void Up() {
            CreateTable(
                    "dbo.Developers",
                    c => new {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Grade = c.String(),
                        Location = c.String(),
                        Room = c.String(),
                        Skype = c.String(),
                        Email = c.String(),
                        HomePhone = c.String(),
                        CellPhone = c.String(),
                        Schedule = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.Projects",
                    c => new {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Client = c.String(),
                        Company = c.String(),
                        Source = c.String(),
                        Contact = c.String(),
                        Status = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        GitUrl = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.Vacations",
                    c => new {
                        Id = c.Guid(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Comment = c.String(),
                        DeveloperId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Developers", t => t.DeveloperId, cascadeDelete: true)
                .Index(t => t.DeveloperId);

            CreateTable(
                    "dbo.Roles",
                    c => new {
                        Id = c.Guid(nullable: false), Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.UserRoles",
                    c => new {
                        UserId = c.Guid(nullable: false), RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new {
                    t.UserId, t.RoleId
                })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                    "dbo.Users",
                    c => new {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.UserClaims",
                    c => new {
                        Id = c.Int(nullable: false, identity: true), UserId = c.Guid(nullable: false), ClaimType = c.String(), ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                    "dbo.UserLogins",
                    c => new {
                        UserId = c.Guid(nullable: false), LoginProvider = c.String(nullable: false, maxLength: 128), ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new {
                    t.UserId, t.LoginProvider, t.ProviderKey
                })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                    "dbo.DeveloperProjects",
                    c => new {
                        DeveloperId = c.Guid(nullable: false), ProjectId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new {
                    t.DeveloperId, t.ProjectId
                })
                .ForeignKey("dbo.Developers", t => t.DeveloperId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.DeveloperId)
                .Index(t => t.ProjectId);

        }

        public override void Down() {
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Vacations", "DeveloperId", "dbo.Developers");
            DropForeignKey("dbo.DeveloperProjects", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.DeveloperProjects", "DeveloperId", "dbo.Developers");
            DropIndex("dbo.DeveloperProjects", new[] {
                "ProjectId"
            });
            DropIndex("dbo.DeveloperProjects", new[] {
                "DeveloperId"
            });
            DropIndex("dbo.UserLogins", new[] {
                "UserId"
            });
            DropIndex("dbo.UserClaims", new[] {
                "UserId"
            });
            DropIndex("dbo.UserRoles", new[] {
                "RoleId"
            });
            DropIndex("dbo.UserRoles", new[] {
                "UserId"
            });
            DropIndex("dbo.Vacations", new[] {
                "DeveloperId"
            });
            DropTable("dbo.DeveloperProjects");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.Vacations");
            DropTable("dbo.Projects");
            DropTable("dbo.Developers");
        }
    }
}
