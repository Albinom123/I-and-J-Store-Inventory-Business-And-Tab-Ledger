using System;

namespace I_and_J_Store_Inventory__Business_And_Tab_Ledger.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public string? ItemName { get; set; }
        public int ItemSold { get; set; }
        public string? Category { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime TimeAndDate { get; set; }
    }
}