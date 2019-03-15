namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SIMSDBv3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserRoles", newName: "__mig_tmp__0");
            RenameTable(name: "dbo.UserUserRoles", newName: "UserRoles");
            RenameTable(name: "__mig_tmp__0", newName: "Roles");
            RenameColumn(table: "dbo.UserRoles", name: "UserRole_Id", newName: "Role_Id");
            RenameIndex(table: "dbo.UserRoles", name: "IX_UserRole_Id", newName: "IX_Role_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.UserRoles", name: "IX_Role_Id", newName: "IX_UserRole_Id");
            RenameColumn(table: "dbo.UserRoles", name: "Role_Id", newName: "UserRole_Id");
            RenameTable(name: "Roles", newName: "__mig_tmp__0");
            RenameTable(name: "dbo.UserRoles", newName: "UserUserRoles");
            RenameTable(name: "dbo.__mig_tmp__0", newName: "UserRoles");
        }
    }
}
