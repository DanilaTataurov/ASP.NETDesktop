namespace ASP.NETDesktop.DAL.Migrations {
    using System.Data.Entity.Migrations;
    
    public partial class UpdateVacations : DbMigration {
        public override void Up() {
            AddColumn("dbo.Vacations", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down() {
            DropColumn("dbo.Vacations", "Status");
        }
    }
}
