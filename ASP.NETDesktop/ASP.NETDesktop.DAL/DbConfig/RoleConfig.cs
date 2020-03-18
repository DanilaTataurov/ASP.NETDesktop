using System.Data.Entity.ModelConfiguration;
using ASP.NETDesktop.Domain.Identity;

namespace ASP.NETDesktop.DAL.DbConfig {
    public class RoleConfig : EntityTypeConfiguration<Role> {
        public RoleConfig() {
            HasKey(r => r.Id);
        }
    }
}
