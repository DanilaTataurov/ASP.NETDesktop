using System.Data.Entity.ModelConfiguration;
using ASP.NETDesktop.Domain.Entities;

namespace ASP.NETDesktop.DAL.DbConfig {
    public class DeveloperConfig : EntityTypeConfiguration<Developer> {
        public DeveloperConfig() {
            HasKey(t => t.Id)
                .HasMany(t => t.Projects)
                .WithMany(p => p.Developers)
                .Map(cs => {
                    cs.MapLeftKey("DeveloperId");
                    cs.MapRightKey("ProjectId");
                    cs.ToTable("DeveloperProjects");
                });
        }
    }
}
