using I_and_J_Store_Inventory__Business_And_Tab_Ledger.DATA;
using I_and_J_Store_Inventory__Business_And_Tab_Ledger.Entities;
using I_and_J_Store_Inventory__Business_And_Tab_Ledger.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace I_and_J_Store_Inventory__Business_And_Tab_Ledger
{
    public partial class UserControlDebtList : UserControl
    {
        private Customer _customer;
        private readonly AppDbContext _context;

        public UserControlDebtList(Customer customer)
        {
            InitializeComponent();
            _customer = customer ?? new Customer { Name = "Unknown" };
            _context = new AppDbContext();

            lblDlCustomerName.Text = $"Customer Name: {_customer.Name}";
            SetupDataGridView();
            LoadDebtHistory();
        }

        // In UserControlDebtList.cs - Update the constructor and add field
        private readonly InventoryService _inventoryService;

        public UserControlDebtList(Customer customer, InventoryService inventoryService)
        {
            InitializeComponent();
            _customer = customer ?? new Customer { Name = "Unknown" };
            _context = new AppDbContext();
            _inventoryService = inventoryService;  // Store the service

            lblDlCustomerName.Text = $"Customer Name: {_customer.Name}";
            SetupDataGridView();
            LoadDebtHistory();
        }
        private void SetupDataGridView()
        {
            dgvDebtList.AutoGenerateColumns = false;
            dgvDebtList.Columns.Clear();
            dgvDebtList.RowHeadersVisible = true;

            dgvDebtList.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDlDate",
                HeaderText = "Date and Time",
                DataPropertyName = "TimeAndDate",
                Width = 150
            });

            dgvDebtList.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDlItem",
                HeaderText = "Item",
                DataPropertyName = "ItemName",
                Width = 200
            });

            dgvDebtList.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDlCategory",
                HeaderText = "Category",
                DataPropertyName = "Category",
                Width = 150
            });

            dgvDebtList.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDlAmount",
                HeaderText = "Amount (₱)",
                DataPropertyName = "TotalAmount",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvDebtList.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDlRunningBalance",
                HeaderText = "Running Balance (₱)",
                DataPropertyName = "RunningBalance",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
        }

        private void LoadDebtHistory()
        {
            var allItems = _context.DebtItems.ToList();

            // Get all items for this customer and order by date (ascending for correct running balance)
            var customerItems = allItems
                .Where(d => d.ItemName != null && d.ItemName.StartsWith($"[{_customer.Name.ToLower()}]", StringComparison.OrdinalIgnoreCase))
                .OrderBy(d => d.TimeAndDate)
                .ToList();

            // Create a list with running balance calculated
            var displayList = new System.Collections.Generic.List<dynamic>();
            decimal runningBalance = 0;

            foreach (var item in customerItems)
            {
                runningBalance += item.TotalAmount;

                displayList.Add(new
                {
                    item.Id,
                    item.TimeAndDate,
                    ItemName = item.ItemName.Contains("] ") ? item.ItemName.Split(new[] { "] " }, StringSplitOptions.None)[1] : item.ItemName,
                    item.Category,
                    item.TotalAmount,
                    RunningBalance = runningBalance,
                    item.ItemSold
                });
            }

            dgvDebtList.DataSource = null;
            dgvDebtList.DataSource = displayList;
        }

        private void btnDlAddItem_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtDlItemName.Text))
            {
                MessageBox.Show("Please enter an item name.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtDlAmount.Text, out decimal totalAmount) || totalAmount <= 0)
            {
                MessageBox.Show("Please enter a valid positive amount.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string category = cmbDlCategory.SelectedItem?.ToString() ?? "";
            string itemName = txtDlItemName.Text.Trim();

            // Get product from inventory service
            var allProducts = _inventoryService.GetAllProducts();
            var product = allProducts.FirstOrDefault(p => p.Name != null &&
                p.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (product == null)
            {
                string availableItems = string.Join(", ", allProducts.Select(p => p.Name).Take(10));
                MessageBox.Show($"Item '{itemName}' not found in inventory!\n\nAvailable items: {availableItems}",
                    "Item Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Calculate quantity
            int quantityForDebt = (int)(totalAmount / product.Price);

            if (quantityForDebt <= 0)
            {
                MessageBox.Show($"Amount (₱{totalAmount:N2}) is less than unit price (₱{product.Price:N2}).",
                    "Quantity Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // USE INVENTORY SERVICE to subtract stock (same as Sales Ledger)
            var stockResult = _inventoryService.SubtractStock(product.Name, quantityForDebt);

            if (!stockResult.Success)
            {
                MessageBox.Show(stockResult.Message, "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create debt item
            var newDebtItem = new DebtItem
            {
                ItemName = $"[{_customer.Name.ToLower()}] " + product.Name,
                Category = category,
                ItemSold = quantityForDebt,
                TotalAmount = totalAmount,
                TimeAndDate = DateTime.Now
            };

            using (var context = new AppDbContext())
            {
                context.DebtItems.Add(newDebtItem);

                // Update customer balance
                var customer = context.Customers.Find(_customer.Id);
                if (customer != null)
                {
                    customer.ActiveBalance += totalAmount;
                    customer.DebtStatus = "Unpaid";
                    if (string.IsNullOrEmpty(customer.BeginDate) || customer.BeginDate == "N/A")
                    {
                        customer.BeginDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }

                context.SaveChanges();
            }

            LoadDebtHistory();
            ClearInputFields();
            MessageBox.Show($"Item added to debt list!\n\n{quantityForDebt} {product.Name}(s) deducted from inventory.\n{stockResult.Message}",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void btnPaydebt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textPaidAmount.Text))
            {
                MessageBox.Show("Please enter an amount to pay.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(textPaidAmount.Text, out decimal paidAmount) || paidAmount <= 0)
            {
                MessageBox.Show("Please enter a valid positive payment amount.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var context = new AppDbContext())
            {
                var customer = context.Customers.Find(_customer.Id);
                if (customer == null)
                {
                    MessageBox.Show("Customer not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (paidAmount > customer.ActiveBalance)
                {
                    MessageBox.Show($"Payment exceeds current balance of ₱{customer.ActiveBalance:N2}",
                        "Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Update customer balance
                decimal oldBalance = customer.ActiveBalance;
                customer.ActiveBalance -= paidAmount;
                customer.DebtStatus = customer.ActiveBalance == 0 ? "Clear" : "Unpaid";

                bool isFullyPaid = customer.ActiveBalance == 0;

                // If balance becomes zero, ask user what to do
                if (isFullyPaid)
                {
                    DialogResult clearItems = MessageBox.Show(
                        "Customer's debt is now fully paid!\n\n" +
                        "Do you want to remove all debt items from the list?\n\n" +
                        "Yes = Remove all items\nNo = Keep items in the list",
                        "Debt Cleared", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (clearItems == DialogResult.Yes)
                    {
                        // Find and delete all debt items for this customer
                        var customerDebtItems = context.DebtItems
                            .Where(d => d.ItemName != null && d.ItemName.StartsWith($"[{_customer.Name.ToLower()}]"))
                            .ToList();

                        context.DebtItems.RemoveRange(customerDebtItems);

                        MessageBox.Show($"All {customerDebtItems.Count} debt item(s) have been removed from the list.",
                            "Items Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                context.SaveChanges();
            }

            // Refresh the debt history display
            LoadDebtHistory();
            textPaidAmount.Clear();
            ClearInputFields();
            buttonSave.Tag = null;

            MessageBox.Show($"Payment of ₱{paidAmount:N2} recorded successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvDebtList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvDebtList.Rows[e.RowIndex].DataBoundItem != null)
            {
                var selectedItem = dgvDebtList.Rows[e.RowIndex].DataBoundItem;
                var idProperty = selectedItem.GetType().GetProperty("Id");
                var itemNameProperty = selectedItem.GetType().GetProperty("ItemName");
                var categoryProperty = selectedItem.GetType().GetProperty("Category");
                var amountProperty = selectedItem.GetType().GetProperty("TotalAmount");

                if (idProperty != null && itemNameProperty != null)
                {
                    int debtId = (int)idProperty.GetValue(selectedItem);
                    string displayName = (string)itemNameProperty.GetValue(selectedItem);
                    string category = categoryProperty != null ? (string)categoryProperty.GetValue(selectedItem) : "";
                    decimal amount = amountProperty != null ? (decimal)amountProperty.GetValue(selectedItem) : 0;

                    txtDlItemName.Text = displayName;
                    txtDlAmount.Text = amount.ToString("0.00");
                    buttonSave.Tag = debtId;

                    if (!string.IsNullOrEmpty(category))
                    {
                        for (int i = 0; i < cmbDlCategory.Items.Count; i++)
                        {
                            if (cmbDlCategory.Items[i].ToString() == category)
                            {
                                cmbDlCategory.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (buttonSave.Tag == null)
            {
                MessageBox.Show("Please select a debt item to edit first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int debtId = (int)buttonSave.Tag;

            if (!decimal.TryParse(txtDlAmount.Text, out decimal newAmount) || newAmount <= 0)
            {
                MessageBox.Show("Please enter a valid positive amount.", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDlItemName.Text))
            {
                MessageBox.Show("Please enter an item name.", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string category = cmbDlCategory.SelectedItem?.ToString() ?? "";

            using (var context = new AppDbContext())
            {
                var debtItem = context.DebtItems.Find(debtId);
                if (debtItem != null)
                {
                    decimal oldAmount = debtItem.TotalAmount;
                    decimal difference = newAmount - oldAmount;
                    int oldQuantity = debtItem.ItemSold;
                    int newQuantity = (int)(newAmount / GetProductPrice(context, debtItem.ItemName));

                    string oldTag = debtItem.ItemName.Split(']')[0] + "]";
                    debtItem.ItemName = oldTag + " " + txtDlItemName.Text.Trim();
                    debtItem.Category = category;
                    debtItem.TotalAmount = newAmount;
                    debtItem.ItemSold = newQuantity > 0 ? newQuantity : oldQuantity;

                    // Update inventory stock based on quantity change
                    var product = context.Products
                        .FirstOrDefault(p => p.Name != null && p.Name.ToLower() == txtDlItemName.Text.Trim().ToLower());

                    if (product != null)
                    {
                        int quantityDifference = newQuantity - oldQuantity;
                        if (quantityDifference != 0)
                        {
                            if (product.Stocks >= quantityDifference)
                            {
                                product.Stocks -= quantityDifference;
                            }
                            else
                            {
                                MessageBox.Show("Insufficient stock for this change!", "Stock Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }

                    var customer = context.Customers.Find(_customer.Id);
                    if (customer != null)
                    {
                        customer.ActiveBalance += difference;
                        customer.DebtStatus = customer.ActiveBalance > 0 ? "Unpaid" : "Clear";
                    }

                    context.SaveChanges();

                    buttonSave.Tag = null;
                    LoadDebtHistory();
                    ClearInputFields();
                    MessageBox.Show("Debt item updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private decimal GetProductPrice(AppDbContext context, string itemName)
        {
            var product = context.Products
                .FirstOrDefault(p => p.Name != null && itemName.ToLower().Contains(p.Name.ToLower()));
            return product?.Price ?? 0;
        }

        private void btnDlEditDebt_Click(object sender, EventArgs e)
        {
            if (dgvDebtList.CurrentRow?.DataBoundItem != null)
            {
                var selectedItem = dgvDebtList.CurrentRow.DataBoundItem;
                var itemNameProperty = selectedItem.GetType().GetProperty("ItemName");
                var categoryProperty = selectedItem.GetType().GetProperty("Category");
                var amountProperty = selectedItem.GetType().GetProperty("TotalAmount");
                var idProperty = selectedItem.GetType().GetProperty("Id");

                if (itemNameProperty != null && idProperty != null)
                {
                    string displayName = (string)itemNameProperty.GetValue(selectedItem);
                    string category = categoryProperty != null ? (string)categoryProperty.GetValue(selectedItem) : "";
                    decimal amount = amountProperty != null ? (decimal)amountProperty.GetValue(selectedItem) : 0;
                    int debtId = (int)idProperty.GetValue(selectedItem);

                    txtDlItemName.Text = displayName;
                    txtDlAmount.Text = amount.ToString("0.00");
                    buttonSave.Tag = debtId;

                    if (!string.IsNullOrEmpty(category))
                    {
                        for (int i = 0; i < cmbDlCategory.Items.Count; i++)
                        {
                            if (cmbDlCategory.Items[i].ToString() == category)
                            {
                                cmbDlCategory.SelectedIndex = i;
                                break;
                            }
                        }
                    }

                    MessageBox.Show("You can now edit the item. Click Save when done.", "Edit Mode",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a debt item to edit first.", "Selection Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDlDeleteDebt_Click(object sender, EventArgs e)
        {
            if (dgvDebtList.CurrentRow?.DataBoundItem != null)
            {
                var selectedItem = dgvDebtList.CurrentRow.DataBoundItem;
                var idProperty = selectedItem.GetType().GetProperty("Id");
                var itemNameProperty = selectedItem.GetType().GetProperty("ItemName");
                var amountProperty = selectedItem.GetType().GetProperty("TotalAmount");
                var itemSoldProperty = selectedItem.GetType().GetProperty("ItemSold");

                if (idProperty != null && itemNameProperty != null && amountProperty != null && itemSoldProperty != null)
                {
                    int debtId = (int)idProperty.GetValue(selectedItem);
                    string itemName = (string)itemNameProperty.GetValue(selectedItem);
                    decimal amount = (decimal)amountProperty.GetValue(selectedItem);
                    int quantityToReturn = (int)itemSoldProperty.GetValue(selectedItem);

                    DialogResult confirm = MessageBox.Show($"Delete '{itemName}' worth ₱{amount:N2}?\n\n" +
                        $"This will return {quantityToReturn} item(s) back to inventory.",
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (confirm == DialogResult.Yes)
                    {
                        // Find the actual product name from inventory
                        var allProducts = _inventoryService.GetAllProducts();
                        var product = allProducts.FirstOrDefault(p => itemName.ToLower().Contains(p.Name.ToLower()));

                        if (product != null)
                        {
                            // RETURN STOCK using InventoryService (add back)
                            var addStockResult = _inventoryService.AddStock(product.Name, quantityToReturn);

                            using (var context = new AppDbContext())
                            {
                                var debtItem = context.DebtItems.Find(debtId);
                                if (debtItem != null)
                                {
                                    // Subtract from customer's debt balance
                                    var customer = context.Customers.Find(_customer.Id);
                                    if (customer != null)
                                    {
                                        customer.ActiveBalance -= debtItem.TotalAmount;
                                        if (customer.ActiveBalance < 0) customer.ActiveBalance = 0;
                                        customer.DebtStatus = customer.ActiveBalance > 0 ? "Unpaid" : "Clear";
                                    }

                                    context.DebtItems.Remove(debtItem);
                                    context.SaveChanges();
                                }
                            }
                        }

                        LoadDebtHistory();
                        ClearInputFields();
                        buttonSave.Tag = null;
                        MessageBox.Show($"Debt item removed!\n\n{quantityToReturn} item(s) returned to inventory.",
                            "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a debt item to delete first.", "Selection Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void buttonReturn_Click(object sender, EventArgs e)
        {
            Form parentForm = this.FindForm();
            if (parentForm != null)
            {
                parentForm.Close();
            }
        }

        private void ClearInputFields()
        {
            txtDlItemName.Clear();
            txtDlAmount.Clear();
            textPaidAmount.Clear();
            cmbDlCategory.SelectedIndex = -1;
            buttonSave.Tag = null;
        }
    }
}