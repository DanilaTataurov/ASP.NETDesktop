using System.Data.Entity.ModelConfiguration;
using ASP.NETDesktop.Domain.Identity;

namespace ASP.NETDesktop.DAL.DbConfig {
    public class UserLoginConfig : EntityTypeConfiguration<UserLogin> {
        public UserLoginConfig() {
            HasKey(r => new {
                r.UserId, r.LoginProvider, r.ProviderKey
            });
        }
    }
}
