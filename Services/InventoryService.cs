using System.Collections.Generic;
using System.Linq;
using I_and_J_Store_Inventory__Business_And_Tab_Ledger.DATA;
using I_and_J_Store_Inventory__Business_And_Tab_Ledger.Entities;

namespace I_and_J_Store_Inventory__Business_And_Tab_Ledger.Services
{
    public class InventoryService
    {
        private readonly AppDbContext _context;

        public InventoryService(AppDbContext context)
        {
            _context = context;
        }

        // Search by Name
        public List<Product> SearchProducts(string searchTerm)
        {
            // 1. Safety Check: If the search box is empty or null, return everything (or an empty list)
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return _context.Products.ToList();
            }

            // 2. Safe Query: Use null-checks for the database property and the input
            return _context.Products
                .Where(p => p.Name != null &&
                            p.Name.ToLower().Contains(searchTerm.ToLower()))
                .ToList();
        }

        // Filter by Category
        public List<Product> GetProductsByCategory(string category)
        {
            return _context.Products
                .Where(p => p.Category == category)
                .ToList();
        }

        // Logic to get all items for your dgvInventory
        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        // Logic to save a new item from your textboxes
        public void AddProduct(Product product)
        {
            // Make sure this is "Products" to match your AppDbContext
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id); // Find it in the database
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public void UpdateProduct(Product updatedProduct)
        {
            _context.Products.Update(updatedProduct);
            _context.SaveChanges();
        }

        public (bool Success, string Message) SubtractStock(string itemName, int quantity)
        {
            // 1. Get the list from the database first
            var allProducts = _context.Products.ToList();

            // 2. Find the product ignoring spaces and capitalization
            // 1. Ensure the list itself isn't null before searching
            if (allProducts == null)
            {
                return (false, "Inventory list is unavailable.");
            }

            // 2. Perform the search
            var product = allProducts.FirstOrDefault(p =>
                p.Name != null && // Extra safety check for the Name property itself
                p.Name.Trim().Equals(itemName.Trim(), StringComparison.OrdinalIgnoreCase));

            // 3. The "Safe Check" for the result (Fixes Warning CS8602)
            if (product == null)
            {
                return (false, "Product not found!");
            }

            // Now C# knows for a fact 'product' is not null, so this is safe:
            product.Stocks -= quantity;

            if (product == null)
                return (false, "Product not found!"); // This is what you see now

            if (product.Stocks < quantity)
                return (false, "Not enough stock!");

            product.Stocks -= quantity;
            _context.SaveChanges();

            return (true, "");
        }
    }
}