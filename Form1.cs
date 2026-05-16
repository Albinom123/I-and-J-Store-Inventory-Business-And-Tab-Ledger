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
        private readonly InventoryService _inventoryService;
        private readonly SalesService _salesService;
        private readonly AppDbContext _context;

        public Form1()
        {
            InitializeComponent();

            using (var db = new AppDbContext())
            {
                db.Database.EnsureCreated(); // Creates the .db file if it's missing
            }

            _context = new AppDbContext();
            _context.Database.EnsureCreated();

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

            // Initialize Services
            _inventoryService = new InventoryService(new AppDbContext());
            _salesService = new SalesService(new AppDbContext());

            // Load ALL grids right here on startup
            LoadInventoryData();
            LoadSalesData();
            RefreshTabLedgerGrid();

            // Attach the tab change event dynamically to handle updates on tab-switch
            if (materialTabControl1 != null)
            {
                materialTabControl1.SelectedIndexChanged += MaterialTabControl1_SelectedIndexChanged;
            }
        }

        // Real-Time Sync Trigger: Automatically pulls data fresh from SQLite whenever you change tabs
        private void MaterialTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadInventoryData();
            LoadSalesData();
            RefreshTabLedgerGrid();
        }

        private void LoadInventoryData()
        {
            var data = _inventoryService.GetAllProducts();

            dgvInventory.DataSource = null; // Flush cache binding
            dgvInventory.AutoGenerateColumns = false;

            // Check if columns exist before setting DataPropertyName
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

            var filteredData = _inventoryService.GetProductsByCategory(selectedCategory) ?? new List<Product>();

            dgvInventory.DataSource = null;
            dgvInventory.DataSource = filteredData;

            if (filteredData.Count == 0)
            {
                MessageBox.Show($"No inventory items found under '{selectedCategory}'.", "Empty Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //////////////////////////////////////////////////////////////////////////
        // SALES LEDGER METHODS
        //////////////////////////////////////////////////////////////////////////

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

        private void ClearSlFields()
        {
            txtSlItemName.Clear();
            txtSlItemPrice.Clear();
            cmbSlCategory.SelectedIndex = -1;
        }

        private void btnSlEditItem_Click(object sender, EventArgs e)
        {
            if (dgvSalesLedger.CurrentRow?.DataBoundItem is Sale saleToUpdate)
            {
                saleToUpdate.ItemName = txtSlItemName.Text.Trim();
                saleToUpdate.Category = cmbSlCategory.Text;

                if (decimal.TryParse(txtSlItemPrice.Text, out decimal newPrice))
                {
                    saleToUpdate.TotalAmount = newPrice;
                    _salesService.UpdateSale(saleToUpdate);

                    LoadSalesData();
                    ClearSlFields();
                    MessageBox.Show("Changes saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please enter a valid price.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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

        //////////////////////////////////////////////////////////////////////////
        // TAB LEDGER METHODS
        //////////////////////////////////////////////////////////////////////////

        private void RefreshTabLedgerGrid()
        {
            using (var context = new AppDbContext())
            {
                dgvTabLedger.DataSource = null;
                dgvTabLedger.AutoGenerateColumns = false;

                // Check if columns exist before setting DataPropertyName
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
                // Pass the InventoryService to the UserControlDebtList
                UserControlDebtList debtListControl = new UserControlDebtList(selectedCustomer, _inventoryService);
                debtListControl.Dock = DockStyle.Fill;

                FloatingHostForm hostForm = new FloatingHostForm();
                hostForm.Text = $"Debt Details - {selectedCustomer.Name}";
                hostForm.Controls.Add(debtListControl);
                hostForm.ShowDialog();

                // Refresh customer ledger balances when details popup closes
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
            // Check if a row is actually selected
            if (dgvTabLedger.CurrentRow == null)
            {
                MessageBox.Show("Please select a customer from the list to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the Customer object bound to the selected row
            var selectedCustomer = dgvTabLedger.CurrentRow.DataBoundItem as Customer;

            if (selectedCustomer == null)
            {
                MessageBox.Show("Unable to determine the selected customer record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Financial Safety Guard: Prevent deleting customers who owe money
            if (selectedCustomer.ActiveBalance > 0)
            {
                MessageBox.Show($"Cannot delete {selectedCustomer.Name} because they still have an outstanding debt balance of ₱{selectedCustomer.ActiveBalance:N2}.\n\nPlease settle their debts before removing them.",
                    "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // Confirm Deletion Intent
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
            // Check if a customer is selected
            if (dgvTabLedger.CurrentRow == null)
            {
                MessageBox.Show("Please select a customer to edit.", "Selection Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the selected customer
            var selectedCustomer = dgvTabLedger.CurrentRow.DataBoundItem as Customer;

            if (selectedCustomer == null)
            {
                MessageBox.Show("Unable to determine the selected customer record.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create a simple form for editing
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
                    // Check for duplicate name
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
                        // Update customer name
                        selectedCustomer.Name = newName;
                        context.Customers.Update(selectedCustomer);

                        // Update DebtItems tags
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
            // Get the search term from the textbox
            string searchTerm = txtTabSearchName.Text.Trim();

            // Check if search term is empty
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("Please enter a customer name to search.", "Search Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var context = new AppDbContext())
            {
                // Search for customers whose name contains the search term (case-insensitive)
                var searchResults = context.Customers
                    .Where(c => c.Name != null && c.Name.ToLower().Contains(searchTerm.ToLower()))
                    .OrderBy(c => c.Name)
                    .ToList();

                // Check if any results found
                if (searchResults.Count == 0)
                {
                    MessageBox.Show($"No customers found matching '{searchTerm}'.", "No Results",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Display search results in the DataGridView
                dgvTabLedger.DataSource = null;
                dgvTabLedger.AutoGenerateColumns = false;

                // Set column mappings
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
    }
}