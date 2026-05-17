using System;

namespace I_and_J_Store_Inventory__Business_And_Tab_Ledger.Entities
{
    public class AppSetting
    {
        public int Id { get; set; }
        public string SettingKey { get; set; } = string.Empty;
        public string SettingValue { get; set; } = string.Empty;
        public string SettingCategory { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime LastUpdated { get; set; }
    }
}