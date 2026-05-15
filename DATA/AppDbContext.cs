using I_and_J_Store_Inventory__Business_And_Tab_Ledger.Entities;
using Microsoft.EntityFrameworkCore;
using static I_and_J_Store_Inventory__Business_And_Tab_Ledger.Services.SalesService;

namespace I_and_J_Store_Inventory__Business_And_Tab_Ledger.DATA
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            // This command creates the .db file and all tables (Products, Sales, etc.) 
            // if they don't exist yet.
            Database.EnsureCreated();
        }
        // Inventory Table
        public DbSet<Product> Products { get; set; }

        // Tab Ledger (Customers) Table
        public DbSet<Customer> Customers { get; set; }

        // Sales Ledger Table
        public DbSet<Sale> Sales { get; set; }

        // UserControlDebtList (Debt Items) Table
        public DbSet<DebtItem> DebtItems { get; set; }

        public DbSet<DebtRecord> DebtRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // This creates the actual database file named inventory.db
            optionsBuilder.UseSqlite("Data Source=inventory.db");
        }
    }
}