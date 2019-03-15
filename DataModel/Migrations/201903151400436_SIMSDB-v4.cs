namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SIMSDBv4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoginStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        JwToken = c.String(),
                        TokenExpires = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LoginStatus");
        }
    }
}
