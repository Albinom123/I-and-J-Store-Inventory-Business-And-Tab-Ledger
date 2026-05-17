using System;
using System.Collections.Generic;
using System.Linq;
using I_and_J_Store_Inventory__Business_And_Tab_Ledger.DATA;
using I_and_J_Store_Inventory__Business_And_Tab_Ledger.Entities;

namespace I_and_J_Store_Inventory__Business_And_Tab_Ledger.Services
{
    public class SettingsService
    {
        private readonly AppDbContext _context;

        public SettingsService(AppDbContext context)
        {
            _context = context;
            InitializeDefaultSettings();
        }

        private void InitializeDefaultSettings()
        {
            // Inventory Settings
            AddIfNotExists("LowStockAlertEnabled", "true", "Inventory", "Enable/disable low stock alerts");
            AddIfNotExists("LowStockThreshold", "10", "Inventory", "Items count before low stock warning");
            AddIfNotExists("CriticalStockThreshold", "5", "Inventory", "Items count before critical alert");

            // Debt Settings
            AddIfNotExists("CreditLimit", "5000", "Debt", "Maximum credit limit per customer");
            AddIfNotExists("PaymentDueDays", "30", "Debt", "Days before debt becomes overdue");
            AddIfNotExists("LateFeePercentage", "5", "Debt", "Percentage fee for late payments");
            AddIfNotExists("AutoApplyLateFee", "false", "Debt", "Automatically apply late fees");
        }

        private void AddIfNotExists(string key, string value, string category, string description)
        {
            try
            {
                if (!_context.AppSettings.Any(s => s.SettingKey == key))
                {
                    _context.AppSettings.Add(new AppSetting
                    {
                        SettingKey = key,
                        SettingValue = value,
                        SettingCategory = category,
                        Description = description,
                        LastUpdated = DateTime.Now
                    });
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding setting {key}: {ex.Message}");
            }
        }

        public string GetSetting(string key, string defaultValue = "")
        {
            try
            {
                var setting = _context.AppSettings.FirstOrDefault(s => s.SettingKey == key);
                return setting?.SettingValue ?? defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public void SetSetting(string key, string value)
        {
            try
            {
                var setting = _context.AppSettings.FirstOrDefault(s => s.SettingKey == key);
                if (setting != null)
                {
                    setting.SettingValue = value;
                    setting.LastUpdated = DateTime.Now;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error setting {key}: {ex.Message}");
            }
        }

        // Inventory Settings Helpers
        public bool IsLowStockAlertEnabled()
        {
            return GetSetting("LowStockAlertEnabled", "true") == "true";
        }

        public int GetLowStockThreshold()
        {
            return int.TryParse(GetSetting("LowStockThreshold", "10"), out int result) ? result : 10;
        }

        public int GetCriticalStockThreshold()
        {
            return int.TryParse(GetSetting("CriticalStockThreshold", "5"), out int result) ? result : 5;
        }

        // Debt Settings Helpers
        public decimal GetCreditLimit()
        {
            return decimal.TryParse(GetSetting("CreditLimit", "5000"), out decimal result) ? result : 5000;
        }

        public int GetPaymentDueDays()
        {
            return int.TryParse(GetSetting("PaymentDueDays", "30"), out int result) ? result : 30;
        }

        public decimal GetLateFeePercentage()
        {
            return decimal.TryParse(GetSetting("LateFeePercentage", "5"), out decimal result) ? result : 5;
        }

        public bool IsAutoApplyLateFeeEnabled()
        {
            return GetSetting("AutoApplyLateFee", "false") == "true";
        }
    }
}