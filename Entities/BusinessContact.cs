using System;

namespace I_and_J_Store_Inventory__Business_And_Tab_Ledger.Entities
{
    public class BusinessContact
    {
        public int Id { get; set; }
        public string ContactName { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string BusinessStatus { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}