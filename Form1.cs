using I_and_J_Store_Inventory__Business_And_Tab_Ledger.DATA;
using I_and_J_Store_Inventory__Business_And_Tab_Ledger.Entities;
using I_and_J_Store_Inventory__Business_And_Tab_Ledger.Services;
using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace I_and_J_Store_Inventory__Business_And_Tab_Ledger
{
    public partial class Form1 : MaterialForm
    {
        // FIX: Ensure the variable name matches your service file
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

            // THIS IS THE MISSING PIECE:
            _context = new AppDbContext();

            // Now that _context is NOT null, we make sure the DB exists
            _context.Database.EnsureCreated();

            // Now this won't crash anymore!
            
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

            // FIX: Initialize the service to stop "Context" errors
            _inventoryService = new InventoryService(new AppDbContext());
            _salesService = new SalesService(new AppDbContext());
            _context = new AppDbContext(); // Initialize it here

            LoadInventoryData();
        }


        private void LoadInventoryData()
        {
            var data = _inventoryService.GetAllProducts();

            // FIX: Stop double columns
            dgvInventory.AutoGenerateColumns = false;

            // FIX: Link Designer columns to Database properties
            dgvInventory.Columns["colInvName"].DataPropertyName = "Name";
            dgvInventory.Columns["colInvCategory"].DataPropertyName = "Category";
            dgvInventory.Columns["colInvPrice"].DataPropertyName = "Price";
            dgvInventory.Columns["colInvStocks"].DataPropertyName = "Stocks";

            dgvInventory.DataSource = data;

            // Header Formatting
            dgvInventory.Columns["colInvName"].HeaderText = "Item Name";
            dgvInventory.Columns["colInvCategory"].HeaderText = "Category";
            dgvInventory.Columns["colInvPrice"].HeaderText = "Prices (₱)";
            dgvInventory.Columns["colInvStocks"].HeaderText = "Stocks";

            if (dgvInventory.Columns["Id"] != null) dgvInventory.Columns["Id"].Visible = false;
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
                // Get the Product object from the clicked row
                var product = (Product)dgvInventory.Rows[e.RowIndex].DataBoundItem;

                if (product != null)
                {
                    // Fill the textboxes with the selected item's info
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
                // Get the specific product from the grid
                var product = (Product)dgvInventory.CurrentRow.DataBoundItem;

                var result = MessageBox.Show($"Delete {product.Name}?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Use the Service to delete by ID
                    _inventoryService.DeleteProduct(product.Id);

                    LoadInventoryData(); // Refresh the list
                    ClearInvFields();
                }
            }
        }

        private void btnInvEditItem_Click(object sender, EventArgs e)
        {
            if (dgvInventory.CurrentRow != null)
            {
                var product = (Product)dgvInventory.CurrentRow.DataBoundItem;

                // Update the object with your new textbox info
                product.Name = txtInvItemName.Text;
                product.Category = cmbInvCategory.Text;

                if (decimal.TryParse(txtInvItemPrice.Text, out decimal price) &&
                  int.TryParse(txtInvItemStocks.Text, out int stocks))
                {
                    product.Price = price;
                    product.Stocks = stocks;

                    _inventoryService.UpdateProduct(product); // Save changes
                    LoadInventoryData();
                    ClearInvFields();
                    MessageBox.Show("Item updated!");
                }
            }
        }

        private void btnInvSearch_Click(object sender, EventArgs e)
        {
            string term = txtInvSearch.Text.Trim(); // Replace with your actual TextBox name

            if (!string.IsNullOrEmpty(term))
            {
                var filteredData = _inventoryService.SearchProducts(term);
                dgvInventory.DataSource = filteredData;
            }
            else
            {
                LoadInventoryData(); // Show everything if search is empty
            }
        }

        private void btnInvFilterApply_Click(object sender, EventArgs e)
        {
            // 1. Get the selected category safely
            // Using SelectedItem?.ToString() is safer than .Text for ComboBoxes
            string selectedCategory = cmbInvFilterCategory.SelectedItem?.ToString() ?? string.Empty;

            // 2. Handle "All", "Categories" (placeholder), or Empty selection
            // This allows the user to reset the view to show everything
            if (string.IsNullOrEmpty(selectedCategory) ||
        selectedCategory == "All" ||
        selectedCategory == "Categories")
            {
                dgvInventory.DataSource = null; // Prevent doubling columns
                dgvInventory.AutoGenerateColumns = false;
                LoadInventoryData(); // Resets the grid
                return;
            }

            // 3. Filter the inventory data
            // We call the service and ensure the result isn't null
            var filteredData = _inventoryService.GetProductsByCategory(selectedCategory) ?? new List<Product>();

            // 4. Update the Grid safely
            dgvInventory.DataSource = null;
            dgvInventory.AutoGenerateColumns = false;
            dgvInventory.DataSource = filteredData;

            // 5. Notify if the category is empty
            if (filteredData.Count == 0)
            {
                MessageBox.Show($"No inventory items found under '{selectedCategory}'.", "Empty Category");
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void SetupSalesGridColumns()
        {
            dgvSalesLedger.Columns.Clear();
            dgvSalesLedger.AutoGenerateColumns = false;

            // Create the columns manually
            dgvSalesLedger.Columns.Add(new DataGridViewTextBoxColumn { Name = "colName", HeaderText = "Item Name", DataPropertyName = "ItemName" });
            dgvSalesLedger.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSold", HeaderText = "Item Sold", DataPropertyName = "ItemSold" });
            dgvSalesLedger.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCat", HeaderText = "Category", DataPropertyName = "Category" });
            dgvSalesLedger.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPrice", HeaderText = "Total Amount (₱)", DataPropertyName = "TotalAmount" });
            dgvSalesLedger.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDate", HeaderText = "Time and Date", DataPropertyName = "TimeAndDate" });
        }
        private void btnSlAddItem_Click(object sender, EventArgs e)
        {
            string itemName = txtSlItemName.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(itemName))
            {
                MessageBox.Show("Please enter an item name.");
                return;
            }

            var inventoryItem = _inventoryService.GetAllProducts()
              .FirstOrDefault(p => p.Name != null && p.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (inventoryItem == null)
            {
                MessageBox.Show("This item doesn't exist in your Inventory.");
                return;
            }

            if (decimal.TryParse(txtSlItemPrice.Text, out decimal totalInput))
            {
                if (inventoryItem.Price <= 0)
                {
                    MessageBox.Show("Inventory price is 0. Please update the price first.");
                    return;
                }

                int quantitySold = (int)(totalInput / inventoryItem.Price);

                if (quantitySold <= 0)
                {
                    MessageBox.Show("The amount entered is less than the price of a single item.");
                    return;
                }

                // Subtract stock and get the result
                var stockResult = _inventoryService.SubtractStock(itemName, quantitySold);

                // FIXED: Removed 'stockResult != null' because Tuples cannot be null
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
                }
                else
                {
                    // FIXED: Removed stockResult?.Message because stockResult is a ValueType
                    MessageBox.Show(stockResult.Message);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid Total Amount.");
            }
        }

        private void LoadSalesData()
        {
            // 1. Completely detach the data first
            dgvSalesLedger.DataSource = null;

            // 2. Ensure AutoGenerate is OFF right before mapping
            dgvSalesLedger.AutoGenerateColumns = false;

            // 3. Double-check these property names against your Sale.cs file
            // These MUST match the public properties (e.g., public string ItemName)
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

            // 4. Finally, grab the list from the service
            var salesList = _salesService.GetAllSales();
            dgvSalesLedger.DataSource = salesList;
        }

        private void btnSlDeleteItem_Click(object sender, EventArgs e)
        {
            // 1. Safe Selection Check
            // This safely checks if a row is selected and if it's actually a 'Sale' object
            if (dgvSalesLedger.CurrentRow?.DataBoundItem is Sale sale)
            {
                var confirm = MessageBox.Show($"Delete sale record for {sale.ItemName}? Stocks will be returned to Inventory.",
                               "Confirm Recovery", MessageBoxButtons.YesNo);

                if (confirm == DialogResult.Yes)
                {
                    // 2. Safe Inventory Lookup
                    // Using ?. and checking for null Name to prevent crashes
                    var product = _inventoryService.GetAllProducts()
                 .FirstOrDefault(p => p.Name != null &&
                 p.Name.Equals(sale.ItemName, StringComparison.OrdinalIgnoreCase));

                    if (product != null)
                    {
                        product.Stocks += sale.ItemSold; // Give the items back
                        _inventoryService.UpdateProduct(product); // Save inventory change
                    }
                    else
                    {
                        // Optional: Notify user if the stock couldn't be returned
                        MessageBox.Show("Note: Item no longer exists in Inventory. Record deleted without stock recovery.");
                    }

                    // 3. Delete the record
                    _salesService.DeleteSale(sale.Id);

                    // 4. Refresh both views
                    LoadSalesData();
                    LoadInventoryData();
                }
            }
            else
            {
                MessageBox.Show("Please select a sale record to delete.");
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
            // 1. Ensure a row is selected
            if (dgvSalesLedger.CurrentRow?.DataBoundItem is Sale saleToUpdate)
            {
                // 2. Update the object with the NEW info from the textboxes
                saleToUpdate.ItemName = txtSlItemName.Text.Trim();
                saleToUpdate.Category = cmbSlCategory.Text;

                if (decimal.TryParse(txtSlItemPrice.Text, out decimal newPrice))
                {
                    saleToUpdate.TotalAmount = newPrice;

                    // 3. Save to database via your service
                    _salesService.UpdateSale(saleToUpdate);

                    // 4. Refresh and Clear
                    LoadSalesData();
                    ClearSlFields();
                    MessageBox.Show("Changes saved successfully!");
                }
                else
                {
                    MessageBox.Show("Please enter a valid price.");
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

                    // We show the Total Amount in the price box for editing
                    txtSlItemPrice.Text = sale.TotalAmount.ToString();
                    dgvSalesLedger_CellClick(sender, e);
                    MessageBox.Show("Ready to edit! Change the price and click 'Edit Item'.");

                    // The date is purely for reference here as it updates on Save
                }
            }
        }
        private void dgvSalesLedger_SelectionChanged(object sender, EventArgs e)
        {
            // 1. Check if a row is actually selected
            if (dgvSalesLedger.CurrentRow != null && dgvSalesLedger.CurrentRow.DataBoundItem is Sale selectedSale)
            {
                // 2. Safely fill the textboxes and combo box
                txtSlItemName.Text = selectedSale.ItemName;
                cmbSlCategory.Text = selectedSale.Category;

                // 3. Fill the price (Total Amount)
                txtSlItemPrice.Text = selectedSale.TotalAmount.ToString("0.00");
            }
        }

        private void btnSlShowDate_Click(object sender, EventArgs e)
        {
            // 1. COMPLETELY RESET the grid before loading new data
            // This stops the doubling/tripling of columns
            dgvSalesLedger.DataSource = null;
            dgvSalesLedger.Columns.Clear(); // Optional: clears manual columns if you want a fresh slate
            dgvSalesLedger.AutoGenerateColumns = false;

            // 2. Re-add your manual column mappings 
            // This ensures your data goes into "Item Name" and not a new "ItemName" column
            SetupSalesGridColumns();

            // 3. Get the date and filter
            DateTime selectedDate = dateTimePicker1.Value;
            var filteredSales = _salesService.GetSalesByDate(selectedDate);

            // 4. Bind the data
            dgvSalesLedger.DataSource = filteredSales;

            if (filteredSales.Count == 0)
            {
                MessageBox.Show($"No sales found for {selectedDate.ToShortDateString()}.");
            }
        }

        private void btnSlSearch_Click(object sender, EventArgs e)
        {
            // 1. Get the search text safely
            string searchTerm = txtSlSearch.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                // If empty, just show everything
                LoadSalesData();
                return;
            }

            // 2. Filter the sales list
            // We use .ToLower() to make sure "COCO" finds "coco"
            var filteredResults = _salesService.GetAllSales()
        .Where(s => s.ItemName != null &&
              s.ItemName.ToLower().Contains(searchTerm.ToLower()))
        .ToList();

            // 3. Update the Grid
            dgvSalesLedger.DataSource = filteredResults;

            if (filteredResults.Count == 0)
            {
                MessageBox.Show($"No sales found matching '{searchTerm}'.");
            }
        }

        private void btnSlFilterApply_Click(object sender, EventArgs e)
        {
            // 1. Get the selected category safely
            string selectedCategory = cmbSlFilterCategory.SelectedItem?.ToString() ?? string.Empty;

            // 2. Handle "All" or Empty selection
            // If "All" is selected, we clear the grid and reload everything
            if (string.IsNullOrEmpty(selectedCategory) || selectedCategory == "All")
            {
                dgvSalesLedger.DataSource = null; // Prevent doubling columns
                dgvSalesLedger.AutoGenerateColumns = false;
                LoadSalesData(); // Resets the grid to show everything
                return;
            }

            // 3. Filter the results
            // We check s.Category != null to prevent CS8602 warnings
            var filteredSales = _salesService.GetAllSales()
        .Where(s => s.Category != null && s.Category.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase))
        .ToList();

            // 4. Update the Grid
            dgvSalesLedger.DataSource = null; // Reset before binding new data
            dgvSalesLedger.DataSource = filteredSales;

            // 5. Notify if no results found
            if (filteredSales.Count == 0)
            {
                MessageBox.Show($"No sales found for category: {selectedCategory}");
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////      





        private void btnTabAddCustomer_Click(object sender, EventArgs e)
        {
            // 1. Get data from UI (just like you do for Product Name)
            string name = txtTabCustomerName.Text.Trim();

            if (string.IsNullOrEmpty(name)) return;

            // 2. Create the object (Similar to: new Product { ... })
            var newCustomer = new Customer
            {
                Name = name,
                ActiveBalance = 0,      // Starting values as we discussed
                DebtStatus = "N/A",
                BeginDate = "N/A"
            };

            // 3. Save to Database (Same pattern as Inventory)
            _context.Customers.Add(newCustomer);
            _context.SaveChanges();

            // 4. Refresh the Table
            txtTabCustomerName.Clear();
          
        }



        private void BtnVCDL_Click(object sender, EventArgs e)
        {
            // 1. Get the selected customer from the grid
            if (dgvTabLedger.CurrentRow?.DataBoundItem is Customer selectedCustomer)
            {
                // 2. Initialize the UserControl with the selected customer
                UserControlDebtList debtListControl = new UserControlDebtList(selectedCustomer);
                debtListControl.Dock = DockStyle.Fill;

                // 3. Create the FloatingHostForm to hold it
                FloatingHostForm hostForm = new FloatingHostForm();
                hostForm.Text = "Customer Debt Details";
                hostForm.Controls.Add(debtListControl);

                // 4. Show it as a popup
                hostForm.ShowDialog();

                // 5. Refresh the main grid when the popup closes 
               
            }
            else
            {
                MessageBox.Show("Please select a customer from the table first!");
            }
        }



























    }
}
