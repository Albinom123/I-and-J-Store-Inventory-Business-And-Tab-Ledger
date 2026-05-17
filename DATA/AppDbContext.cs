using Microsoft.EntityFrameworkCore;
using I_and_J_Store_Inventory__Business_And_Tab_Ledger.Entities;

namespace I_and_J_Store_Inventory__Business_And_Tab_Ledger.DATA
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DebtItem> DebtItems { get; set; }
        public DbSet<DebtRecord> DebtRecords { get; set; }
        public DbSet<BusinessContact> BusinessContacts { get; set; }
        public DbSet<AppSetting> AppSettings { get; set; }  // ADD THIS LINE

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=inventory.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Optional: Configure BusinessContact table
            modelBuilder.Entity<BusinessContact>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ContactName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.ContactNumber).IsRequired().HasMaxLength(50);
                entity.Property(e => e.BusinessStatus).HasMaxLength(50);
                entity.Property(e => e.DateCreated).IsRequired();
            });
        }
    }
}