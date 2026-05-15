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
            public string Name { get; set; } = string.Empty;
            public decimal ActiveBalance { get; set; } = 0; // Starts at 0
            public string DebtStatus { get; set; } = "N/A"; // Default N/A
            public string BeginDate { get; set; } = "N/A"; // Using string so we can put "N/A"
        
    }
}
