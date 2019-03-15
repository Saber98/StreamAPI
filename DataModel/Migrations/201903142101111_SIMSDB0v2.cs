namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SIMSDB0v2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                        Description = c.String(),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(nullable: false),
                        Archived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserUserRoles",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        UserRole_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.UserRole_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserRoles", t => t.UserRole_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.UserRole_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserUserRoles", "UserRole_Id", "dbo.UserRoles");
            DropForeignKey("dbo.UserUserRoles", "User_Id", "dbo.Users");
            DropIndex("dbo.UserUserRoles", new[] { "UserRole_Id" });
            DropIndex("dbo.UserUserRoles", new[] { "User_Id" });
            DropTable("dbo.UserUserRoles");
            DropTable("dbo.UserRoles");
        }
    }
}
