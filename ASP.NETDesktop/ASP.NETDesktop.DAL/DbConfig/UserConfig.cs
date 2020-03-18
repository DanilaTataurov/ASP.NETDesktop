using System.Data.Entity.ModelConfiguration;
using ASP.NETDesktop.Domain.Identity;

namespace ASP.NETDesktop.DAL.DbConfig {
    public class UserConfig : EntityTypeConfiguration<User> {
        public UserConfig() {
            HasKey(t => t.Id);
        }
    }
}
