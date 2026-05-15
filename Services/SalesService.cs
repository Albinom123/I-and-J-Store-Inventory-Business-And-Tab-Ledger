using I_and_J_Store_Inventory__Business_And_Tab_Ledger.DATA;
using I_and_J_Store_Inventory__Business_And_Tab_Ledger.Entities; // Critical: Links to Step A
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

        public List<Sale> GetAllSales() => _context.Sales.ToList();

        public void AddSale(Sale sale)
        {
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
            _context.Sales.Update(sale);
            _context.SaveChanges();
        }

        public List<Sale> GetSalesByDate(DateTime date)
        {
            // Filters sales where the Year, Month, and Day match the selected date
            return _context.Sales
                .Where(s => s.TimeAndDate.Date == date.Date)
                .ToList();
        }
    }

}