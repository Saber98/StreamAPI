namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SIMSDBv1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressTypeId = c.Int(nullable: false),
                        Address1 = c.String(nullable: false),
                        Address2 = c.String(),
                        City = c.String(nullable: false),
                        StateId = c.Int(nullable: false),
                        Zip = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(nullable: false),
                        Archived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AddressTypeLookups", t => t.AddressTypeId, cascadeDelete: true)
                .ForeignKey("dbo.StateLookups", t => t.StateId, cascadeDelete: true)
                .Index(t => t.AddressTypeId)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.AddressTypeLookups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressType = c.String(nullable: false, maxLength: 10),
                        Archived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        EmailAddress = c.String(),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(nullable: false),
                        Archived = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        AccountRepId = c.Int(nullable: false),
                        ShipToBillInd = c.Boolean(nullable: false),
                        WebsiteUrl = c.String(),
                        AccountNumber = c.String(),
                        IsParentInd = c.Boolean(nullable: false),
                        ParentId = c.Int(),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(nullable: false),
                        Archived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.AccountRepId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.ParentId)
                .Index(t => t.AccountRepId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.PhoneNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhoneTypeId = c.Int(nullable: false),
                        AreaCode = c.String(maxLength: 3),
                        Number = c.String(maxLength: 8),
                        Extension = c.String(maxLength: 6),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(nullable: false),
                        Archived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PhoneTypeLookups", t => t.PhoneTypeId, cascadeDelete: true)
                .Index(t => t.PhoneTypeId);
            
            CreateTable(
                "dbo.PhoneTypeLookups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhoneType = c.String(nullable: false, maxLength: 8),
                        Archived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StateLookups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateAbbreviation = c.String(nullable: false, maxLength: 2),
                        StateName = c.String(nullable: false, maxLength: 35),
                        Archived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        EmailAddress = c.String(nullable: false),
                        ContactId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(nullable: false),
                        Archived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.UserSecurity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Password = c.String(nullable: false),
                        LastLogin = c.DateTime(nullable: false),
                        LockedOut = c.Boolean(nullable: false),
                        IpAddress = c.String(),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ContactAddresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false),
                        ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AddressId, t.ContactId })
                .ForeignKey("dbo.Contacts", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.Addresses", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.AddressId)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.CustomerAddresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AddressId, t.CustomerId })
                .ForeignKey("dbo.Customers", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.Addresses", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.AddressId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.CustomerContacts",
                c => new
                    {
                        ContactId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ContactId, t.CustomerId })
                .ForeignKey("dbo.Customers", t => t.ContactId, cascadeDelete: false)
                .ForeignKey("dbo.Contacts", t => t.CustomerId, cascadeDelete: false)
                .Index(t => t.ContactId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.CustomerPhoneNumbers",
                c => new
                    {
                        PhoneNumberId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PhoneNumberId, t.CustomerId })
                .ForeignKey("dbo.Customers", t => t.PhoneNumberId, cascadeDelete: true)
                .ForeignKey("dbo.PhoneNumbers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.PhoneNumberId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.ContactPhoneNumbers",
                c => new
                    {
                        PhoneNumberId = c.Int(nullable: false),
                        ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PhoneNumberId, t.ContactId })
                .ForeignKey("dbo.Contacts", t => t.PhoneNumberId, cascadeDelete: true)
                .ForeignKey("dbo.PhoneNumbers", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.PhoneNumberId)
                .Index(t => t.ContactId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSecurity", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Addresses", "StateId", "dbo.StateLookups");
            DropForeignKey("dbo.ContactPhoneNumbers", "ContactId", "dbo.PhoneNumbers");
            DropForeignKey("dbo.ContactPhoneNumbers", "PhoneNumberId", "dbo.Contacts");
            DropForeignKey("dbo.CustomerPhoneNumbers", "CustomerId", "dbo.PhoneNumbers");
            DropForeignKey("dbo.CustomerPhoneNumbers", "PhoneNumberId", "dbo.Customers");
            DropForeignKey("dbo.PhoneNumbers", "PhoneTypeId", "dbo.PhoneTypeLookups");
            DropForeignKey("dbo.Customers", "ParentId", "dbo.Customers");
            DropForeignKey("dbo.CustomerContacts", "CustomerId", "dbo.Contacts");
            DropForeignKey("dbo.CustomerContacts", "ContactId", "dbo.Customers");
            DropForeignKey("dbo.CustomerAddresses", "CustomerId", "dbo.Addresses");
            DropForeignKey("dbo.CustomerAddresses", "AddressId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "AccountRepId", "dbo.Contacts");
            DropForeignKey("dbo.ContactAddresses", "ContactId", "dbo.Addresses");
            DropForeignKey("dbo.ContactAddresses", "AddressId", "dbo.Contacts");
            DropForeignKey("dbo.Addresses", "AddressTypeId", "dbo.AddressTypeLookups");
            DropIndex("dbo.ContactPhoneNumbers", new[] { "ContactId" });
            DropIndex("dbo.ContactPhoneNumbers", new[] { "PhoneNumberId" });
            DropIndex("dbo.CustomerPhoneNumbers", new[] { "CustomerId" });
            DropIndex("dbo.CustomerPhoneNumbers", new[] { "PhoneNumberId" });
            DropIndex("dbo.CustomerContacts", new[] { "CustomerId" });
            DropIndex("dbo.CustomerContacts", new[] { "ContactId" });
            DropIndex("dbo.CustomerAddresses", new[] { "CustomerId" });
            DropIndex("dbo.CustomerAddresses", new[] { "AddressId" });
            DropIndex("dbo.ContactAddresses", new[] { "ContactId" });
            DropIndex("dbo.ContactAddresses", new[] { "AddressId" });
            DropIndex("dbo.UserSecurity", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "ContactId" });
            DropIndex("dbo.PhoneNumbers", new[] { "PhoneTypeId" });
            DropIndex("dbo.Customers", new[] { "ParentId" });
            DropIndex("dbo.Customers", new[] { "AccountRepId" });
            DropIndex("dbo.Addresses", new[] { "StateId" });
            DropIndex("dbo.Addresses", new[] { "AddressTypeId" });
            DropTable("dbo.ContactPhoneNumbers");
            DropTable("dbo.CustomerPhoneNumbers");
            DropTable("dbo.CustomerContacts");
            DropTable("dbo.CustomerAddresses");
            DropTable("dbo.ContactAddresses");
            DropTable("dbo.UserSecurity");
            DropTable("dbo.Users");
            DropTable("dbo.StateLookups");
            DropTable("dbo.PhoneTypeLookups");
            DropTable("dbo.PhoneNumbers");
            DropTable("dbo.Customers");
            DropTable("dbo.Contacts");
            DropTable("dbo.AddressTypeLookups");
            DropTable("dbo.Addresses");
        }
    }
}
