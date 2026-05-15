using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I_and_J_Store_Inventory__Business_And_Tab_Ledger.Entities
{
    public class Product
    {
       
        
            public int Id { get; set; }
            // Adding the '?' tells C# it's okay if these start as null
            public string? Name { get; set; }
            public string? Category { get; set; }
            public decimal Price { get; set; }
            public int Stocks { get; set; }
        
    }
}
