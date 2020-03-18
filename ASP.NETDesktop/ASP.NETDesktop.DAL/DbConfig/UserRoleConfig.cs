using System.Data.Entity.ModelConfiguration;
using ASP.NETDesktop.Domain.Identity;

namespace ASP.NETDesktop.DAL.DbConfig {
    public class UserRoleConfig : EntityTypeConfiguration<UserRole> {
        public UserRoleConfig() {
            HasKey(r => new {
                r.UserId, r.RoleId
            });
        }
    }
}
