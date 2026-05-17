using I_and_J_Store_Inventory__Business_And_Tab_Ledger.DATA;
using I_and_J_Store_Inventory__Business_And_Tab_Ledger.Entities;
using I_and_J_Store_Inventory__Business_And_Tab_Ledger.Services;
using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace I_and_J_Store_Inventory__Business_And_Tab_Ledger
{
    public partial class Form1 : MaterialForm
    {
        #region Private Fields / Variables

        private readonly InventoryService _inventoryService;
        private readonly SalesService _salesService;
        private readonly AppDbContext _context;
        private readonly BusinessContactService _businessContactService;
        private readonly SettingsService _settingsService;

        // Edit mode tracking variables
        private bool isEditMode = false;
        private int editingContactId = 0;

        #endregion

        #region Constructor & Initialization

        public Form1()
        {
            InitializeComponent();

            using (var db = new AppDbContext())
            {
                db.Database.EnsureCreated();
            }

            _context = new AppDbContext();
            _context.Database.EnsureCreated();
            _businessContactService = new BusinessContactService(new AppDbContext());
            _inventoryService = new InventoryService(new AppDbContext());
            _salesService = new SalesService(new AppDbContext());
            _settingsService = new SettingsService(new AppDbContext());

            // MaterialSkin Setup
            var msm = MaterialSkinManager.Instance;
            msm.AddFormToManage(this);
            msm.Theme = MaterialSkinManager.Themes.LIGHT;
            msm.ColorScheme = new ColorScheme(
              Color.FromArgb(184, 134, 11),
              Color.FromArgb(153, 101, 21),
              Color.FromArgb(218, 165, 32),
              Color.FromArgb(255, 215, 0),
              TextShade.WHITE
            );

            labelSL.Font = new Font("Montserrat", 30f, FontStyle.Bold | FontStyle.Underline);
            labelSL.ForeColor = Color.FromArgb(153, 101, 21);

            labelInv.Font = new Font("Montserrat", 30f, FontStyle.Bold | FontStyle.Underline);
            labelInv.ForeColor = Color.FromArgb(153, 101, 21);

            labelTL.Font = new Font("Montserrat", 30f, FontStyle.Bold | FontStyle.Underline);
            labelTL.ForeColor = Color.FromArgb(153, 101, 21);

            LblHome.Font = new Font("Montserrat", 30f, FontStyle.Bold | FontStyle.Underline);
            LblHome.ForeColor = Color.FromArgb(153, 101, 21);

            lblSettings.Font = new Font("Montserrat", 30f, FontStyle.Bold | FontStyle.Underline);
            lblSettings.ForeColor = Color.FromArgb(153, 101, 21);

            this.MaximizeBox = false;

            InitializeHistoryComboBox();
            SetupHomeGridColumns();
            LoadHomeData();
            LoadInventoryData();
            LoadSalesData();
            RefreshTabLedgerGrid();
            LoadSettingsToUI();  // Load saved settings

            if (materialTabControl1 != null)
            {
                materialTabControl1.SelectedIndexChanged += MaterialTabControl1_SelectedIndexChanged;
            }
        }

        private void InitializeHistoryComboBox()
        {
            comboBoxDLHistory.Items.Clear();
            comboBoxDLHistory.Items.Add("Current Day");
            comboBoxDLHistory.Items.Add("Last 5 days");
            comboBoxDLHistory.Items.Add("Last week");
            comboBoxDLHistory.Items.Add("Last Month");
            comboBoxDLHistory.Items.Add("Last year");
            comboBoxDLHistory.SelectedIndex = 0;
        }

        private void MaterialTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadInventoryData();
            LoadSalesData();
            RefreshTabLedgerGrid();
        }

        #endregion

        #region SETTINGS SECTION

        private void LoadSettingsToUI()
        {
            try
            {
                // Load Inventory Settings
                chkLowStockAlert.Checked = _settingsService.IsLowStockAlertEnabled();
                numLowStockThreshold.Value = _settingsService.GetLowStockThreshold();
                numCriticalStockThreshold.Value = _settingsService.GetCriticalStockThreshold();

                // Load Debt Settings
                numCreditLimit.Value = _settingsService.GetCreditLimit();
                numDueDays.Value = _settingsService.GetPaymentDueDays();
                numLateFeePercent.Value = _settingsService.GetLateFeePercentage();
                chkAutoApplyLateFee.Checked = _settingsService.IsAutoApplyLateFeeEnabled();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading settings: {ex.Message}");
            }
        }

        private void btnSaveset_Click(object sender, EventArgs e)
        {
            try
            {
                // Save Inventory Settings
                _settingsService.SetSetting("LowStockAlertEnabled", chkLowStockAlert.Checked.ToString().ToLower());
                _settingsService.SetSetting("LowStockThreshold", numLowStockThreshold.Value.ToString());
                _settingsService.SetSetting("CriticalStockThreshold", numCriticalStockThreshold.Value.ToString());

                // Save Debt Settings
                _settingsService.SetSetting("CreditLimit", numCreditLimit.Value.ToString());
                _settingsService.SetSetting("PaymentDueDays", numDueDays.Value.ToString());
                _settingsService.SetSetting("LateFeePercentage", numLateFeePercent.Value.ToString());
                _settingsService.SetSetting("AutoApplyLateFee", chkAutoApplyLateFee.Checked.ToString().ToLower());

                MessageBox.Show("Settings saved successfully!\n\n" +
                    "INVENTORY SETTINGS:\n" +
                    $"• Low Stock Alert: {(chkLowStockAlert.Checked ? "Enabled" : "Disabled")}\n" +
                    $"• Alert Threshold: {numLowStockThreshold.Value} items\n" +
                    $"• Critical Level: {numCriticalStockThreshold.Value} items\n\n" +
                    "DEBT SETTINGS:\n" +
                    $"• Credit Limit: ₱{numCreditLimit.Value:N0}\n" +
                    $"• Payment Due Days: {numDueDays.Value} days\n" +
                    $"• Late Fee: {numLateFeePercent.Value}%\n" +
                    $"• Auto Apply Late Fee: {(chkAutoApplyLateFee.Checked ? "Enabled" : "Disabled")}",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btndefaultset_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Reset all settings to default values?\n\n" +
                "INVENTORY SETTINGS:\n" +
                "• Low Stock Alert: Enabled\n" +
                "• Alert Threshold: 10 items\n" +
                "• Critical Level: 5 items\n\n" +
                "DEBT SETTINGS:\n" +
                "• Credit Limit: ₱5,000\n" +
                "• Payment Due Days: 30 days\n" +
                "• Late Fee: 5%\n" +
                "• Auto Apply Late Fee: Disabled\n\n" +
                "You will need to click 'Save Settings' to keep these changes.",
                "Confirm Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // Reset Inventory Settings values
                chkLowStockAlert.Checked = true;
                numLowStockThreshold.Value = 10;
                numCriticalStockThreshold.Value = 5;

                // Reset Debt Settings values
                numCreditLimit.Value = 5000;
                numDueDays.Value = 30;
                numLateFeePercent.Value = 5;
                chkAutoApplyLateFee.Checked = false;

                MessageBox.Show("Settings have been reset to default values!\n\n" +
                    "Click 'Save Settings' to permanently save these changes.",
                    "Reset Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region INVENTORY SECTION

        private void LoadInventoryData()
        {
            var data = _inventoryService.GetAllProducts();

            dgvInventory.DataSource = null;
            dgvInventory.AutoGenerateColumns = false;

            if (dgvInventory.Columns.Contains("colInvName"))
                dgvInventory.Columns["colInvName"].DataPropertyName = "Name";

            if (dgvInventory.Columns.Contains("colInvCategory"))
                dgvInventory.Columns["colInvCategory"].DataPropertyName = "Category";

            if (dgvInventory.Columns.Contains("colInvPrice"))
                dgvInventory.Columns["colInvPrice"].DataPropertyName = "Price";

            if (dgvInventory.Columns.Contains("colInvStocks"))
                dgvInventory.Columns["colInvStocks"].DataPropertyName = "Stocks";

            dgvInventory.DataSource = data;

            dgvInventory.Columns["colInvName"].HeaderText = "Item Name";
            dgvInventory.Columns["colInvCategory"].HeaderText = "Category";
            dgvInventory.Columns["colInvPrice"].HeaderText = "Prices (₱)";
            dgvInventory.Columns["colInvStocks"].HeaderText = "Stocks";

            if (dgvInventory.Columns["Id"] != null)
                dgvInventory.Columns["Id"].Visible = false;
        }

        private void btnInvAddItem_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtInvItemPrice.Text, out decimal price) &&
              int.TryParse(txtInvItemStocks.Text, out int stocks))
            {
                var newProduct = new Product
                {
                    Name = txtInvItemName.Text,
                    Category = cmbInvCategory.Text,
                    Price = price,
                    Stocks = stocks
                };

                _inventoryService.AddProduct(newProduct);
                LoadInventoryData();
                ClearInvFields();
            }
            else
            {
                MessageBox.Show("Please enter valid price and stock values.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClearInvFields()
        {
            txtInvItemName.Clear();
            txtInvItemPrice.Clear();
            txtInvItemStocks.Clear();
            cmbInvCategory.SelectedIndex = -1;
        }

        private void dgvInventory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var product = (Product)dgvInventory.Rows[e.RowIndex].DataBoundItem;

                if (product != null)
                {
                    txtInvItemName.Text = product.Name;
                    cmbInvCategory.Text = product.Category;
                    txtInvItemPrice.Text = product.Price.ToString();
                    txtInvItemStocks.Text = product.Stocks.ToString();
                }
            }
        }

        private void btnInvDeleteItem_Click(object sender, EventArgs e)
        {
            if (dgvInventory.CurrentRow != null)
            {
                var product = (Product)dgvInventory.CurrentRow.DataBoundItem;

                var result = MessageBox.Show($"Delete {product.Name}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    _inventoryService.DeleteProduct(product.Id);
                    LoadInventoryData();
                    ClearInvFields();
                }
            }
        }

        private void btnInvEditItem_Click(object sender, EventArgs e)
        {
            if (dgvInventory.CurrentRow != null)
            {
                var product = (Product)dgvInventory.CurrentRow.DataBoundItem;

                product.Name = txtInvItemName.Text;
                product.Category = cmbInvCategory.Text;

                if (decimal.TryParse(txtInvItemPrice.Text, out decimal price) &&
                  int.TryParse(txtInvItemStocks.Text, out int stocks))
                {
                    product.Price = price;
                    product.Stocks = stocks;

                    _inventoryService.UpdateProduct(product);
                    LoadInventoryData();
                    ClearInvFields();
                    MessageBox.Show("Item updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnInvSearch_Click(object sender, EventArgs e)
        {
            string term = txtInvSearch.Text.Trim();

            if (!string.IsNullOrEmpty(term))
            {
                var filteredData = _inventoryService.SearchProducts(term);
                dgvInventory.DataSource = filteredData;
            }
            else
            {
                LoadInventoryData();
            }
        }

        private void btnInvFilterApply_Click(object sender, EventArgs e)
        {
            string selectedCategory = cmbInvFilterCategory.SelectedItem?.ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(selectedCategory) || selectedCategory == "All" || selectedCategory == "Categories")
            {
                LoadInventoryData();
                return;
            }

            var filteredData = _inventoryService.GetProductsByCategory(selectedCategory) ?? new System.Collections.Generic.List<Product>();

            dgvInventory.DataSource = null;
            dgvInventory.DataSource = filteredData;

            if (filteredData.Count == 0)
            {
                MessageBox.Show($"No inventory items found under '{selectedCategory}'.", "Empty Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region SALES LEDGER SECTION

        private void SetupSalesGridColumns()
        {
            dgvSalesLedger.Columns.Clear();
            dgvSalesLedger.AutoGenerateColumns = false;

            dgvSalesLedger.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSLName", HeaderText = "Item Name", DataPropertyName = "ItemName" });
            dgvSalesLedger.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSLItemsold", HeaderText = "Item Sold", DataPropertyName = "ItemSold" });
            dgvSalesLedger.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSLCategory", HeaderText = "Category", DataPropertyName = "Category" });
            dgvSalesLedger.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSlPrice", HeaderText = "Total Amount (₱)", DataPropertyName = "TotalAmount" });
            dgvSalesLedger.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSLDateTime", HeaderText = "Time and Date", DataPropertyName = "TimeAndDate" });
        }

        private void LoadSalesData()
        {
            using (var context = new AppDbContext())
            {
                dgvSalesLedger.DataSource = null;
                dgvSalesLedger.AutoGenerateColumns = false;

                if (dgvSalesLedger.Columns.Count == 0)
                {
                    SetupSalesGridColumns();
                }

                if (dgvSalesLedger.Columns.Contains("colSLName"))
                    dgvSalesLedger.Columns["colSLName"].DataPropertyName = "ItemName";

                if (dgvSalesLedger.Columns.Contains("colSLItemsold"))
                    dgvSalesLedger.Columns["colSLItemsold"].DataPropertyName = "ItemSold";

                if (dgvSalesLedger.Columns.Contains("colSLCategory"))
                    dgvSalesLedger.Columns["colSLCategory"].DataPropertyName = "Category";

                if (dgvSalesLedger.Columns.Contains("colSlPrice"))
                    dgvSalesLedger.Columns["colSlPrice"].DataPropertyName = "TotalAmount";

                if (dgvSalesLedger.Columns.Contains("colSLDateTime"))
                    dgvSalesLedger.Columns["colSLDateTime"].DataPropertyName = "TimeAndDate";

                var salesList = context.Sales.OrderByDescending(s => s.TimeAndDate).ToList();
                dgvSalesLedger.DataSource = salesList;
            }
        }

        private void btnSlAddItem_Click(object sender, EventArgs e)
        {
            if (sender is Button btn) btn.Enabled = false;

            try
            {
                string itemName = txtSlItemName.Text?.Trim() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(itemName))
                {
                    MessageBox.Show("Please enter an item name.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string searchName = itemName.ToLower();
                var inventoryItem = _inventoryService.GetAllProducts()
                  .FirstOrDefault(p => p.Name != null && p.Name.Trim().ToLower() == searchName);

                if (inventoryItem == null)
                {
                    MessageBox.Show("This item doesn't exist in your Inventory.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (decimal.TryParse(txtSlItemPrice.Text, out decimal totalInput))
                {
                    if (inventoryItem.Price <= 0)
                    {
                        MessageBox.Show("Inventory price is 0. Please update the price first.", "Price Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int quantitySold = (int)(totalInput / inventoryItem.Price);

                    if (quantitySold <= 0)
                    {
                        MessageBox.Show("The amount entered is less than the price of a single item.", "Quantity Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (inventoryItem.Stocks < quantitySold)
                    {
                        MessageBox.Show($"Insufficient stock available! Only {inventoryItem.Stocks} remaining.", "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var stockResult = _inventoryService.SubtractStock(inventoryItem.Name, quantitySold);

                    if (stockResult.Success)
                    {
                        var newSale = new Sale
                        {
                            ItemName = inventoryItem.Name ?? "Unknown Item",
                            Category = inventoryItem.Category ?? "Uncategorized",
                            ItemSold = quantitySold,
                            TotalAmount = totalInput,
                            TimeAndDate = DateTime.Now
                        };

                        _salesService.AddSale(newSale);
                        LoadSalesData();
                        LoadInventoryData();
                        ClearSlFields();

                        if (!string.IsNullOrEmpty(stockResult.Message))
                        {
                            MessageBox.Show(stockResult.Message, "Stock Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        MessageBox.Show("Sale recorded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(stockResult.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid Total Amount.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            finally
            {
                if (sender is Button buttonControl) buttonControl.Enabled = true;
            }
        }

        private void ClearSlFields()
        {
            txtSlItemName.Clear();
            txtSlItemPrice.Clear();
            cmbSlCategory.SelectedIndex = -1;
        }

        private void btnSlDeleteItem_Click(object sender, EventArgs e)
        {
            if (dgvSalesLedger.CurrentRow?.DataBoundItem is Sale sale)
            {
                var confirm = MessageBox.Show($"Delete sale record for {sale.ItemName}? Stocks will be returned to Inventory.",
                               "Confirm Recovery", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    var product = _inventoryService.GetAllProducts()
                                 .FirstOrDefault(p => p.Name != null && p.Name.Equals(sale.ItemName, StringComparison.OrdinalIgnoreCase));

                    if (product != null)
                    {
                        product.Stocks += sale.ItemSold;
                        _inventoryService.UpdateProduct(product);
                    }

                    _salesService.DeleteSale(sale.Id);
                    LoadSalesData();
                    LoadInventoryData();
                    MessageBox.Show("Sale record deleted and stock restored.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a sale record to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvSalesLedger_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var sale = (Sale)dgvSalesLedger.Rows[e.RowIndex].DataBoundItem;
                if (sale != null)
                {
                    txtSlItemName.Text = sale.ItemName;
                    cmbSlCategory.Text = sale.Category;
                    txtSlItemPrice.Text = sale.TotalAmount.ToString();
                }
            }
        }

        private void dgvSalesLedger_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSalesLedger.CurrentRow != null && dgvSalesLedger.CurrentRow.DataBoundItem is Sale selectedSale)
            {
                txtSlItemName.Text = selectedSale.ItemName;
                cmbSlCategory.Text = selectedSale.Category;
                txtSlItemPrice.Text = selectedSale.TotalAmount.ToString("0.00");
            }
        }

        private void btnSlShowDate_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value;
            var filteredSales = _salesService.GetSalesByDate(selectedDate);

            dgvSalesLedger.DataSource = null;
            dgvSalesLedger.DataSource = filteredSales;

            if (filteredSales.Count == 0)
            {
                MessageBox.Show($"No sales found for {selectedDate.ToShortDateString()}.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSlSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSlSearch.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                LoadSalesData();
                return;
            }

            var filteredResults = _salesService.GetAllSales()
                .Where(s => s.ItemName != null && s.ItemName.ToLower().Contains(searchTerm.ToLower()))
                .ToList();

            dgvSalesLedger.DataSource = filteredResults;

            if (filteredResults.Count == 0)
            {
                MessageBox.Show($"No sales found matching '{searchTerm}'.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSlFilterApply_Click(object sender, EventArgs e)
        {
            string selectedCategory = cmbSlFilterCategory.SelectedItem?.ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(selectedCategory) || selectedCategory == "All")
            {
                LoadSalesData();
                return;
            }

            var filteredSales = _salesService.GetAllSales()
                .Where(s => s.Category != null && s.Category.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase))
                .ToList();

            dgvSalesLedger.DataSource = null;
            dgvSalesLedger.DataSource = filteredSales;

            if (filteredSales.Count == 0)
            {
                MessageBox.Show($"No sales found for category: {selectedCategory}", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSLhistoryCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    string selectedFilter = comboBoxDLHistory.SelectedItem?.ToString() ?? "Current Day";

                    DateTime startDate;
                    DateTime endDate = DateTime.Now;

                    switch (selectedFilter)
                    {
                        case "Current Day":
                            startDate = DateTime.Today;
                            break;
                        case "Last 5 days":
                            startDate = DateTime.Today.AddDays(-5);
                            break;
                        case "Last week":
                            startDate = DateTime.Today.AddDays(-7);
                            break;
                        case "Last Month":
                            startDate = DateTime.Today.AddMonths(-1);
                            break;
                        case "Last year":
                            startDate = DateTime.Today.AddYears(-1);
                            break;
                        default:
                            startDate = DateTime.Today;
                            break;
                    }

                    var allSales = context.Sales.ToList();

                    var filteredSales = allSales
                        .Where(s => s.TimeAndDate >= startDate && s.TimeAndDate <= endDate)
                        .ToList();

                    decimal totalAmount = 0;
                    foreach (var sale in filteredSales)
                    {
                        totalAmount += sale.TotalAmount;
                    }

                    materialTextBoxTotalSLSold.Text = $"₱{totalAmount:N2}";
                    dgvSalesLedger.DataSource = null;
                    dgvSalesLedger.DataSource = filteredSales;

                    if (filteredSales.Count == 0)
                    {
                        MessageBox.Show($"No sales found for '{selectedFilter}'.", "No Results",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region TAB LEDGER SECTION

        private void RefreshTabLedgerGrid()
        {
            using (var context = new AppDbContext())
            {
                dgvTabLedger.DataSource = null;
                dgvTabLedger.AutoGenerateColumns = false;

                if (dgvTabLedger.Columns.Contains("colTabName"))
                    dgvTabLedger.Columns["colTabName"].DataPropertyName = "Name";

                if (dgvTabLedger.Columns.Contains("colTabBalance"))
                    dgvTabLedger.Columns["colTabBalance"].DataPropertyName = "ActiveBalance";

                if (dgvTabLedger.Columns.Contains("colTabStatus"))
                    dgvTabLedger.Columns["colTabStatus"].DataPropertyName = "DebtStatus";

                if (dgvTabLedger.Columns.Contains("colTabDate"))
                    dgvTabLedger.Columns["colTabDate"].DataPropertyName = "BeginDate";

                var customerList = context.Customers.OrderBy(c => c.Name).ToList();
                dgvTabLedger.DataSource = customerList;
            }
        }

        private void btnTabAddCustomer_Click(object sender, EventArgs e)
        {
            string customerName = txtTabCustomerName.Text.Trim();
            if (string.IsNullOrWhiteSpace(customerName))
            {
                MessageBox.Show("Please enter a customer name before registering.",
                                "Input Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var context = new AppDbContext())
            {
                bool nameExists = context.Customers.Any(c => c.Name.ToLower() == customerName.ToLower());
                if (nameExists)
                {
                    MessageBox.Show($"A customer named '{customerName}' is already registered in the ledger system.",
                                    "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var newCustomer = new Customer
                {
                    Name = customerName,
                    ActiveBalance = 0,
                    DebtStatus = "N/A",
                    BeginDate = "N/A"
                };

                context.Customers.Add(newCustomer);
                context.SaveChanges();
            }

            txtTabCustomerName.Clear();
            RefreshTabLedgerGrid();

            MessageBox.Show($"{customerName} has been successfully added to your Tab Ledger!",
                            "Registration Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnVCDL_Click(object sender, EventArgs e)
        {
            if (dgvTabLedger.CurrentRow?.DataBoundItem is Customer selectedCustomer)
            {
                UserControlDebtList debtListControl = new UserControlDebtList(selectedCustomer, _inventoryService);
                debtListControl.Dock = DockStyle.Fill;

                FloatingHostForm hostForm = new FloatingHostForm();
                hostForm.Text = $"Debt Details - {selectedCustomer.Name}";
                hostForm.Controls.Add(debtListControl);
                hostForm.ShowDialog();

                RefreshTabLedgerGrid();
            }
            else
            {
                MessageBox.Show("Please select a customer from the table first!", "Selection Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTabDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (dgvTabLedger.CurrentRow == null)
            {
                MessageBox.Show("Please select a customer from the list to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedCustomer = dgvTabLedger.CurrentRow.DataBoundItem as Customer;

            if (selectedCustomer == null)
            {
                MessageBox.Show("Unable to determine the selected customer record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (selectedCustomer.ActiveBalance > 0)
            {
                MessageBox.Show($"Cannot delete {selectedCustomer.Name} because they still have an outstanding debt balance of ₱{selectedCustomer.ActiveBalance:N2}.\n\nPlease settle their debts before removing them.",
                    "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            DialogResult confirmResult = MessageBox.Show(
                $"Are you absolutely sure you want to permanently delete customer '{selectedCustomer.Name}'?\n\nThis will remove their history from the ledger.",
                "Confirm Permanent Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    using (var context = new AppDbContext())
                    {
                        var customerToDelete = context.Customers.Find(selectedCustomer.Id);
                        if (customerToDelete != null)
                        {
                            context.Customers.Remove(customerToDelete);
                            context.SaveChanges();
                        }
                    }

                    MessageBox.Show("Customer profile successfully deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshTabLedgerGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected database error occurred while trying to delete: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTabEditCustomer_Click(object sender, EventArgs e)
        {
            if (dgvTabLedger.CurrentRow == null)
            {
                MessageBox.Show("Please select a customer to edit.", "Selection Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedCustomer = dgvTabLedger.CurrentRow.DataBoundItem as Customer;

            if (selectedCustomer == null)
            {
                MessageBox.Show("Unable to determine the selected customer record.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Form editForm = new Form();
            editForm.Text = "Edit Customer";
            editForm.Size = new Size(350, 150);
            editForm.StartPosition = FormStartPosition.CenterParent;
            editForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            editForm.MaximizeBox = false;
            editForm.MinimizeBox = false;

            Label lblName = new Label() { Text = "Customer Name:", Left = 20, Top = 20, Width = 100 };
            TextBox txtName = new TextBox() { Text = selectedCustomer.Name, Left = 130, Top = 17, Width = 180 };
            Button btnSave = new Button() { Text = "Save", Left = 80, Top = 60, Width = 80, DialogResult = DialogResult.OK };
            Button btnCancel = new Button() { Text = "Cancel", Left = 180, Top = 60, Width = 80, DialogResult = DialogResult.Cancel };

            editForm.Controls.Add(lblName);
            editForm.Controls.Add(txtName);
            editForm.Controls.Add(btnSave);
            editForm.Controls.Add(btnCancel);

            if (editForm.ShowDialog() == DialogResult.OK)
            {
                string newName = txtName.Text.Trim();

                if (string.IsNullOrWhiteSpace(newName))
                {
                    MessageBox.Show("Customer name cannot be empty.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var context = new AppDbContext())
                {
                    bool nameExists = context.Customers.Any(c => c.Id != selectedCustomer.Id &&
                        c.Name.ToLower() == newName.ToLower());

                    if (nameExists)
                    {
                        MessageBox.Show($"A customer named '{newName}' already exists.",
                            "Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        selectedCustomer.Name = newName;
                        context.Customers.Update(selectedCustomer);

                        var oldNameTag = $"[{selectedCustomer.Name.ToLower()}]";
                        var newNameTag = $"[{newName.ToLower()}]";

                        var allItems = context.DebtItems.ToList();
                        var customerItems = allItems.Where(d => d.ItemName != null &&
                            d.ItemName.StartsWith(oldNameTag, StringComparison.OrdinalIgnoreCase));

                        foreach (var item in customerItems)
                        {
                            item.ItemName = newNameTag + item.ItemName.Substring(oldNameTag.Length);
                        }

                        context.SaveChanges();
                        RefreshTabLedgerGrid();

                        MessageBox.Show($"Customer name changed to '{newName}'!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error updating customer: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnTabSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtTabSearchName.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("Please enter a customer name to search.", "Search Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var context = new AppDbContext())
            {
                var searchResults = context.Customers
                    .Where(c => c.Name != null && c.Name.ToLower().Contains(searchTerm.ToLower()))
                    .OrderBy(c => c.Name)
                    .ToList();

                if (searchResults.Count == 0)
                {
                    MessageBox.Show($"No customers found matching '{searchTerm}'.", "No Results",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dgvTabLedger.DataSource = null;
                dgvTabLedger.AutoGenerateColumns = false;

                if (dgvTabLedger.Columns.Contains("colTabName"))
                    dgvTabLedger.Columns["colTabName"].DataPropertyName = "Name";

                if (dgvTabLedger.Columns.Contains("colTabBalance"))
                    dgvTabLedger.Columns["colTabBalance"].DataPropertyName = "ActiveBalance";

                if (dgvTabLedger.Columns.Contains("colTabStatus"))
                    dgvTabLedger.Columns["colTabStatus"].DataPropertyName = "DebtStatus";

                if (dgvTabLedger.Columns.Contains("colTabDate"))
                    dgvTabLedger.Columns["colTabDate"].DataPropertyName = "BeginDate";

                dgvTabLedger.DataSource = searchResults;
            }
        }

        #endregion

        #region HOME SECTION (Business Contacts)

        private void SetupHomeGridColumns()
        {
            dataGridViewHome.SuspendLayout();
            try
            {
                dataGridViewHome.AutoGenerateColumns = false;
                dataGridViewHome.Columns.Clear();
                dataGridViewHome.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewHome.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dataGridViewHome.RowHeadersVisible = true;

                dataGridViewHome.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colContactName",
                    HeaderText = "Contact Name",
                    DataPropertyName = "ContactName",
                    FillWeight = 35
                });

                dataGridViewHome.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colContactNumber",
                    HeaderText = "Contact Number",
                    DataPropertyName = "ContactNumber",
                    FillWeight = 25
                });

                dataGridViewHome.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colBusinessStatus",
                    HeaderText = "Business Status",
                    DataPropertyName = "BusinessStatus",
                    FillWeight = 20
                });

                dataGridViewHome.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colDateCreated",
                    HeaderText = "Date Created",
                    DataPropertyName = "DateCreated",
                    FillWeight = 20,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd HH:mm" }
                });
            }
            finally
            {
                dataGridViewHome.ResumeLayout();
            }
        }

        private void LoadHomeData()
        {
            try
            {
                var data = _businessContactService.GetAllContacts();
                dataGridViewHome.DataSource = null;
                dataGridViewHome.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading contacts: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearHomeFields()
        {
            textBoxBusinessConhome.Clear();
            textBoxhomeConNum.Clear();
            comboxBusinesSatushome.SelectedIndex = -1;
        }

        private void ExitEditMode()
        {
            isEditMode = false;
            editingContactId = 0;
            buttonEditHome.Text = "Edit Contact";
            buttonEditHome.BackColor = Color.Empty;
            buttonHomeSave.Enabled = true;
            ClearHomeFields();
        }

        private void buttonHomeSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (isEditMode)
                {
                    ExitEditMode();
                }

                if (string.IsNullOrWhiteSpace(textBoxBusinessConhome.Text))
                {
                    MessageBox.Show("Please enter a contact name.", "Input Required",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxhomeConNum.Text))
                {
                    MessageBox.Show("Please enter a contact number.", "Input Required",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string businessStatus = comboxBusinesSatushome.SelectedItem?.ToString() ?? "Active";

                var newContact = new BusinessContact
                {
                    ContactName = textBoxBusinessConhome.Text.Trim(),
                    ContactNumber = textBoxhomeConNum.Text.Trim(),
                    BusinessStatus = businessStatus,
                    DateCreated = DateTime.Now
                };

                _businessContactService.AddContact(newContact);
                MessageBox.Show("Contact saved successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadHomeData();
                ClearHomeFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving contact: {ex.InnerException?.Message ?? ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonEditHome_Click(object sender, EventArgs e)
        {
            if (isEditMode)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(textBoxBusinessConhome.Text))
                    {
                        MessageBox.Show("Please enter a contact name.", "Input Required",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(textBoxhomeConNum.Text))
                    {
                        MessageBox.Show("Please enter a contact number.", "Input Required",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string businessStatus = comboxBusinesSatushome.SelectedItem?.ToString() ?? "Active";

                    var contact = _businessContactService.GetContactById(editingContactId);
                    if (contact != null)
                    {
                        contact.ContactName = textBoxBusinessConhome.Text.Trim();
                        contact.ContactNumber = textBoxhomeConNum.Text.Trim();
                        contact.BusinessStatus = businessStatus;
                        _businessContactService.UpdateContact(contact);

                        MessageBox.Show("Contact updated successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    ExitEditMode();
                    LoadHomeData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating contact: {ex.InnerException?.Message ?? ex.Message}",
                        "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (dataGridViewHome.CurrentRow == null || !(dataGridViewHome.CurrentRow.DataBoundItem is BusinessContact selectedContact))
                {
                    MessageBox.Show("Please select a contact to edit.", "Selection Required",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                textBoxBusinessConhome.Text = selectedContact.ContactName;
                textBoxhomeConNum.Text = selectedContact.ContactNumber;
                editingContactId = selectedContact.Id;
                isEditMode = true;

                for (int i = 0; i < comboxBusinesSatushome.Items.Count; i++)
                {
                    if (comboxBusinesSatushome.Items[i].ToString() == selectedContact.BusinessStatus)
                    {
                        comboxBusinesSatushome.SelectedIndex = i;
                        break;
                    }
                }

                buttonEditHome.Text = "Save Changes";
                buttonEditHome.BackColor = Color.Green;
                buttonHomeSave.Enabled = false;

                MessageBox.Show($"Editing '{selectedContact.ContactName}'. Make your changes and click 'Save Changes'.",
                    "Edit Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonhomedelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewHome.CurrentRow?.DataBoundItem is BusinessContact selectedContact)
            {
                if (isEditMode && editingContactId == selectedContact.Id)
                {
                    ExitEditMode();
                }

                DialogResult confirm = MessageBox.Show(
                    $"Are you sure you want to delete '{selectedContact.ContactName}'?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    _businessContactService.DeleteContact(selectedContact.Id);
                    LoadHomeData();
                    ClearHomeFields();
                    MessageBox.Show("Contact deleted successfully!", "Deleted",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a contact to delete.", "Selection Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridViewHome_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!isEditMode && e.RowIndex >= 0 && dataGridViewHome.Rows[e.RowIndex].DataBoundItem is BusinessContact selectedContact)
            {
                textBoxBusinessConhome.Text = selectedContact.ContactName;
                textBoxhomeConNum.Text = selectedContact.ContactNumber;

                for (int i = 0; i < comboxBusinesSatushome.Items.Count; i++)
                {
                    if (comboxBusinesSatushome.Items[i].ToString() == selectedContact.BusinessStatus)
                    {
                        comboxBusinesSatushome.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void btnHomeSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtHomeSearch.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                LoadHomeData();
                return;
            }

            var searchResults = _businessContactService.SearchContacts(searchTerm);
            dataGridViewHome.DataSource = null;
            dataGridViewHome.DataSource = searchResults;

            if (searchResults.Count == 0)
            {
                MessageBox.Show($"No contacts found matching '{searchTerm}'.", "No Results",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion
    }
}