using I_and_J_Store_Inventory__Business_And_Tab_Ledger.DATA;
using I_and_J_Store_Inventory__Business_And_Tab_Ledger.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace I_and_J_Store_Inventory__Business_And_Tab_Ledger.Services
{
    public class SalesService
    {
        private readonly AppDbContext _context;

        public SalesService(AppDbContext context)
        {
            _context = context;
        }

        public List<Sale> GetAllSales()
        {
            return _context.Sales
                .OrderByDescending(s => s.TimeAndDate) // Show newest first
                .ToList();
        }

        public void AddSale(Sale sale)
        {
            if (sale == null)
            {
                throw new ArgumentNullException(nameof(sale), "Sale cannot be null.");
            }

            // Ensure TimeAndDate is set
            if (sale.TimeAndDate == default)
            {
                sale.TimeAndDate = DateTime.Now;
            }

            _context.Sales.Add(sale);
            _context.SaveChanges();
        }

        public void DeleteSale(int id)
        {
            var sale = _context.Sales.Find(id);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
                _context.SaveChanges();
            }
        }

        public void UpdateSale(Sale sale)
        {
            if (sale == null)
            {
                throw new ArgumentNullException(nameof(sale), "Sale cannot be null.");
            }

            // Check if sale exists before updating
            var existingSale = _context.Sales.Find(sale.Id);
            if (existingSale == null)
            {
                throw new InvalidOperationException($"Sale with ID {sale.Id} not found.");
            }

            _context.Sales.Update(sale);
            _context.SaveChanges();
        }

        public List<Sale> GetSalesByDate(DateTime date)
        {
            // Filters sales where the Year, Month, and Day match the selected date
            return _context.Sales
                .Where(s => s.TimeAndDate.Date == date.Date)
                .OrderByDescending(s => s.TimeAndDate) // Show newest first within the date
                .ToList();
        }

        // Additional useful methods:

        public List<Sale> GetSalesByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.Sales
                .Where(s => s.TimeAndDate.Date >= startDate.Date && s.TimeAndDate.Date <= endDate.Date)
                .OrderByDescending(s => s.TimeAndDate)
                .ToList();
        }

        public List<Sale> GetSalesByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return GetAllSales();
            }

            return _context.Sales
                .Where(s => s.Category != null && s.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(s => s.TimeAndDate)
                .ToList();
        }

        public decimal GetTotalSalesByDate(DateTime date)
        {
            return _context.Sales
                .Where(s => s.TimeAndDate.Date == date.Date)
                .Sum(s => s.TotalAmount);
        }

        public decimal GetTotalSalesOverall()
        {
            return _context.Sales.Sum(s => s.TotalAmount);
        }

        public int GetTotalItemsSoldByDate(DateTime date)
        {
            return _context.Sales
                .Where(s => s.TimeAndDate.Date == date.Date)
                .Sum(s => s.ItemSold);
        }

        public List<Sale> SearchSalesByItemName(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return GetAllSales();
            }

            return _context.Sales
                .Where(s => s.ItemName != null && s.ItemName.ToLower().Contains(searchTerm.ToLower()))
                .OrderByDescending(s => s.TimeAndDate)
                .ToList();
        }
    }
}