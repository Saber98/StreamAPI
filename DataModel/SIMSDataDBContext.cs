using System.Data.Entity;
using DataModel.DBObjects;
using DataModel.DBObjects.Lookups;
using DataModel.SecurityObjects;

namespace DataModel
{
    public class SIMSDataDBContext : DbContext
    {
        #region DB Set Definition

        public DbSet<StateLookup> States { get; set; }
        public DbSet<AddressTypeLookup> AddressTypes { get; set; }
        public DbSet<PhoneTypeLookup> PhoneNumberTypes { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSecurity> UserSecurity { get; set; }
        public DbSet<Role> Roles { get; set; }
        

        // Login Security Details
        public DbSet<LoginStatus> LoginStatus { get; set; }
        #endregion

        public SIMSDataDBContext() : base("name=SIMSDataDBContext")
        {
           // Database.SetInitializer(new CreateDatabaseIfNotExists<SIMSDataDBContext, Migrations.Configuration>());
           // Database.SetInitializer<EmrDataDbContext>(new DropCreateDatabaseAlways<EmrDataDbContext>());
           // Database.SetInitializer<EmrDataDbContext>(new DropCreateDatabaseIfModelChanges<EmrDataDbContext>());
           
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(u => u.Contacts)
                .WithMany(l => l.Customers)
                .Map(ul =>
                {
                    ul.MapLeftKey("ContactId");
                    ul.MapRightKey("CustomerId");
                    ul.ToTable("CustomerContacts");
                });

            modelBuilder.Entity<Customer>()
                .HasMany(u => u.Addresses)
                .WithMany(l => l.Customers)
                .Map(ul =>
                {
                    ul.MapLeftKey("AddressId");
                    ul.MapRightKey("CustomerId");
                    ul.ToTable("CustomerAddresses");
                });

            modelBuilder.Entity<Customer>()
                .HasMany(u => u.PhoneNumbers)
                .WithMany(l => l.Customers)
                .Map(ul =>
                {
                    ul.MapLeftKey("PhoneNumberId");
                    ul.MapRightKey("CustomerId");
                    ul.ToTable("CustomerPhoneNumbers");
                });

            modelBuilder.Entity<Contact>()
                .HasMany(u => u.PhoneNumbers)
                .WithMany(l => l.Contacts)
                .Map(ul =>
                {
                    ul.MapLeftKey("PhoneNumberId");
                    ul.MapRightKey("ContactId");
                    ul.ToTable("ContactPhoneNumbers");
                });

            modelBuilder.Entity<Contact>()
                .HasMany(u => u.Addresses)
                .WithMany(l => l.Contacts)
                .Map(ul =>
                {
                    ul.MapLeftKey("AddressId");
                    ul.MapRightKey("ContactId");
                    ul.ToTable("ContactAddresses");
                });
        }
    }
}
