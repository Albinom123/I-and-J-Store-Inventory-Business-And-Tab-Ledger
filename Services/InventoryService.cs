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
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return _context.Products.ToList();
            }

            return _context.Products
                .Where(p => p.Name != null && p.Name.ToLower().Contains(searchTerm.ToLower()))
                .ToList();
        }

        // Filter by Category
        public List<Product> GetProductsByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return GetAllProducts();
            }

            return _context.Products
                .Where(p => p.Category != null && p.Category == category)
                .ToList();
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public void AddProduct(Product product)
        {
            if (product == null)
            {
                throw new System.ArgumentNullException(nameof(product), "Product cannot be null.");
            }

            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public void UpdateProduct(Product updatedProduct)
        {
            if (updatedProduct == null)
            {
                throw new System.ArgumentNullException(nameof(updatedProduct), "Updated product cannot be null.");
            }

            _context.Products.Update(updatedProduct);
            _context.SaveChanges();
        }

        public (bool Success, string Message) SubtractStock(string itemName, int quantity)
        {
            // Validate parameters
            if (string.IsNullOrWhiteSpace(itemName))
            {
                return (false, "Item name cannot be null or empty.");
            }

            if (quantity <= 0)
            {
                return (false, "Quantity must be greater than zero.");
            }

            // Clean the input
            string cleanedName = itemName.Trim();

            // FIX: Use ToLower() instead of StringComparison
            var product = _context.Products
                .FirstOrDefault(p => p.Name != null && p.Name.ToLower() == cleanedName.ToLower());

            if (product == null)
            {
                return (false, $"Product '{itemName}' not found in inventory!");
            }

            if (product.Stocks < quantity)
            {
                return (false, $"Not enough stock! Only {product.Stocks} {product.Name}(s) remaining.");
            }

            product.Stocks -= quantity;
            _context.SaveChanges();

            string message = product.Stocks == 0 ?
                $"Warning: {product.Name} is now out of stock!" :
                $"Successfully deducted {quantity} {product.Name}(s). {product.Stocks} remaining.";

            return (true, message);
        }

        public (bool Success, string Message) AddStock(string itemName, int quantity)
        {
            if (string.IsNullOrWhiteSpace(itemName))
            {
                return (false, "Item name cannot be null or empty.");
            }

            if (quantity <= 0)
            {
                return (false, "Quantity must be greater than zero.");
            }

            string cleanedName = itemName.Trim();

            var product = _context.Products
                .FirstOrDefault(p => p.Name != null && p.Name.ToLower() == cleanedName.ToLower());

            if (product == null)
            {
                return (false, $"Product '{itemName}' not found in inventory!");
            }

            product.Stocks += quantity;
            _context.SaveChanges();

            return (true, $"Added {quantity} {product.Name}(s). New stock: {product.Stocks}");
        }

        public bool ProductExists(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            string cleanedName = name.Trim();
            return _context.Products.Any(p => p.Name != null && p.Name.ToLower() == cleanedName.ToLower());
        }

        public Product GetProductByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            string cleanedName = name.Trim();
            return _context.Products
                .FirstOrDefault(p => p.Name != null && p.Name.ToLower() == cleanedName.ToLower());
        }
    }
}