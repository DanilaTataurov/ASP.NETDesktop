using System.Data.Entity.ModelConfiguration;
using ASP.NETDesktop.Domain.Entities;

namespace ASP.NETDesktop.DAL.DbConfig {
    public class ProjectConfig : EntityTypeConfiguration<Project> {
        public ProjectConfig() {
            HasKey(t => t.Id)
                .HasMany(t => t.Developers)
                .WithMany(p => p.Projects)
                .Map(cs => {
                    cs.MapLeftKey("ProjectId");
                    cs.MapRightKey("DeveloperId");
                    cs.ToTable("DeveloperProjects");
                });
        }
    }
}
