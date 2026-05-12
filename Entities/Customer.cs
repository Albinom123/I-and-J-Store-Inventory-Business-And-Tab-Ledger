using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I_and_J_Store_Inventory__Business_And_Tab_Ledger.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TotalBalance { get; set; } // Will show with ₱ sign in UI
        public string DebtStatus { get; set; } // e.g., "Cleared", "Overdue"
        public DateTime LastUpdated { get; set; }
    }
}
