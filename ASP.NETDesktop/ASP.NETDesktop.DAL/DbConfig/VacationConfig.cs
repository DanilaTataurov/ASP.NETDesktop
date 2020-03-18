using System.Data.Entity.ModelConfiguration;
using ASP.NETDesktop.Domain.Entities;

namespace ASP.NETDesktop.DAL.DbConfig {
    public class VacationConfig : EntityTypeConfiguration<Vacation> {
        public VacationConfig() {
            HasKey(t => t.Id);
        }
    }
}
