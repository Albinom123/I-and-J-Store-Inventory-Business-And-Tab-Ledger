using System;

namespace I_and_J_Store_Inventory__Business_And_Tab_Ledger.Entities
{
    public class DebtItem
    {
        public int Id { get; set; }
        // public int CustomerId { get; set; }  // ← COMMENT THIS OUT OR DELETE IT
        public string ItemName { get; set; } = string.Empty;
        public int ItemSold { get; set; }
        public decimal TotalAmount { get; set; }
        public string Category { get; set; } = string.Empty;
        public DateTime TimeAndDate { get; set; }
    }
}