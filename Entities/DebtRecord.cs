using System;

namespace I_and_J_Store_Inventory__Business_And_Tab_Ledger.Entities
{
    public class DebtRecord
    {
        public int Id { get; set; }

        // This links the debt to a specific customer
        public int CustomerId { get; set; }

        public DateTime DateAndTime { get; set; } = DateTime.Now;
        public string Item { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Type { get; set; } = "Debt"; // Can be "Debt" or "Payment"
        public decimal Amount { get; set; }

        // This is for your 'Runing Balance' column
        public decimal RunningBalance { get; set; }
    }
}