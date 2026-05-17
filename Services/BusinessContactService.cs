using System;
using System.Collections.Generic;
using System.Linq;
using I_and_J_Store_Inventory__Business_And_Tab_Ledger.DATA;
using I_and_J_Store_Inventory__Business_And_Tab_Ledger.Entities;
using Microsoft.EntityFrameworkCore;

namespace I_and_J_Store_Inventory__Business_And_Tab_Ledger.Services
{
    public class BusinessContactService
    {
        private readonly AppDbContext _context;

        public BusinessContactService(AppDbContext context)
        {
            _context = context;
            EnsureTableExists(); // Auto-create table if missing
        }

        // Auto-create the table if it doesn't exist
        private void EnsureTableExists()
        {
            try
            {
                // Check if table exists
                var tableExists = _context.Database.SqlQueryRaw<int>(
                    "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='BusinessContacts'")
                    .ToList()
                    .FirstOrDefault() > 0;

                if (!tableExists)
                {
                    // Create the table
                    _context.Database.ExecuteSqlRaw(@"
                        CREATE TABLE BusinessContacts (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            ContactName TEXT NOT NULL,
                            ContactNumber TEXT NOT NULL,
                            BusinessStatus TEXT NOT NULL,
                            DateCreated TEXT NOT NULL,
                            DateUpdated TEXT NULL
                        )");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error ensuring table exists: {ex.Message}");
            }
        }

        public List<BusinessContact> GetAllContacts()
        {
            try
            {
                return _context.BusinessContacts
                    .OrderByDescending(c => c.DateCreated)
                    .ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting contacts: {ex.Message}");
                return new List<BusinessContact>();
            }
        }

        public void AddContact(BusinessContact contact)
        {
            if (contact == null)
                throw new ArgumentNullException(nameof(contact));

            contact.DateCreated = DateTime.Now;
            _context.BusinessContacts.Add(contact);
            _context.SaveChanges();
        }

        public void UpdateContact(BusinessContact contact)
        {
            if (contact == null)
                throw new ArgumentNullException(nameof(contact));

            contact.DateUpdated = DateTime.Now;
            _context.BusinessContacts.Update(contact);
            _context.SaveChanges();
        }

        public void DeleteContact(int id)
        {
            var contact = _context.BusinessContacts.Find(id);
            if (contact != null)
            {
                _context.BusinessContacts.Remove(contact);
                _context.SaveChanges();
            }
        }

        public BusinessContact GetContactById(int id)
        {
            return _context.BusinessContacts.Find(id);
        }

        public List<BusinessContact> SearchContacts(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return GetAllContacts();
            }

            try
            {
                return _context.BusinessContacts
                    .Where(c => c.ContactName.ToLower().Contains(searchTerm.ToLower()) ||
                               c.ContactNumber.Contains(searchTerm))
                    .OrderByDescending(c => c.DateCreated)
                    .ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error searching contacts: {ex.Message}");
                return new List<BusinessContact>();
            }
        }
    }
}