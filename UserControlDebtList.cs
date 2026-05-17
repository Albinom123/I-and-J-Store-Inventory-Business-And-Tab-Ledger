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
        private readonly InventoryService _inventoryService;
        private int _editingDebtId = 0;

        public UserControlDebtList(Customer customer, InventoryService inventoryService)
        {
            InitializeComponent();
            _customer = customer ?? new Customer { Name = "Unknown" };
            _context = new AppDbContext();
            _inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(inventoryService));

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
            // FIXED: Load all items into memory first
            var allItems = _context.DebtItems.ToList();

            var customerItems = allItems
                .Where(d => d.ItemName != null && d.ItemName.StartsWith($"[{_customer.Name.ToLower()}]", StringComparison.OrdinalIgnoreCase))
                .OrderBy(d => d.TimeAndDate)
                .ToList();

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
            _editingDebtId = 0;
        }

        private void btnDlAddItem_Click(object sender, EventArgs e)
        {
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

            int quantityForDebt = (int)(totalAmount / product.Price);

            if (quantityForDebt <= 0)
            {
                MessageBox.Show($"Amount (₱{totalAmount:N2}) is less than unit price (₱{product.Price:N2}).",
                    "Quantity Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var stockResult = _inventoryService.SubtractStock(product.Name, quantityForDebt);

            if (!stockResult.Success)
            {
                MessageBox.Show(stockResult.Message, "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
            MessageBox.Show($"Item added to debt list!\n\n{quantityForDebt} {product.Name}(s) deducted from inventory.",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // FIXED: btnPaydebt_Click with proper in-memory filtering
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

                // FIXED: Load all debt items into memory first, then filter
                var allDebtItems = context.DebtItems.ToList();
                var debtItems = allDebtItems
                    .Where(d => d.ItemName != null && d.ItemName.StartsWith($"[{_customer.Name.ToLower()}]", StringComparison.OrdinalIgnoreCase))
                    .OrderBy(d => d.TimeAndDate)
                    .ToList();

                decimal remainingPayment = paidAmount;
                var itemsToTransfer = new System.Collections.Generic.List<DebtItem>();
                var partiallyPaidItem = new { Item = (DebtItem)null, PartialAmount = 0m, PartialQuantity = 0 };

                foreach (var item in debtItems)
                {
                    if (remainingPayment <= 0) break;

                    if (item.TotalAmount <= remainingPayment)
                    {
                        itemsToTransfer.Add(item);
                        remainingPayment -= item.TotalAmount;
                    }
                    else
                    {
                        int partialQuantity = (int)((remainingPayment / item.TotalAmount) * item.ItemSold);
                        partiallyPaidItem = new { Item = item, PartialAmount = remainingPayment, PartialQuantity = partialQuantity };
                        remainingPayment = 0;
                    }
                }

                string confirmMessage = $"Payment of ₱{paidAmount:N2} from {_customer.Name} will be recorded as official sales:\n\n";
                foreach (var item in itemsToTransfer)
                {
                    string itemName = item.ItemName.Contains("] ") ? item.ItemName.Split(new[] { "] " }, StringSplitOptions.None)[1] : item.ItemName;
                    confirmMessage += $"• {itemName} - ₱{item.TotalAmount:N2}\n";
                }
                if (partiallyPaidItem.Item != null)
                {
                    string itemName = partiallyPaidItem.Item.ItemName.Contains("] ") ?
                        partiallyPaidItem.Item.ItemName.Split(new[] { "] " }, StringSplitOptions.None)[1] :
                        partiallyPaidItem.Item.ItemName;
                    confirmMessage += $"• {itemName} - ₱{partiallyPaidItem.PartialAmount:N2} (Partial)\n";
                }

                DialogResult confirm = MessageBox.Show(confirmMessage + "\n\nProceed with payment?",
                    "Confirm Payment", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                // Transfer fully paid items to SALES LEDGER
                foreach (var item in itemsToTransfer)
                {
                    string originalItemName = item.ItemName.Contains("] ") ?
                        item.ItemName.Split(new[] { "] " }, StringSplitOptions.None)[1] :
                        item.ItemName;

                    var sale = new Sale
                    {
                        ItemName = originalItemName,
                        Category = item.Category,
                        ItemSold = item.ItemSold,
                        TotalAmount = item.TotalAmount,
                        TimeAndDate = DateTime.Now
                    };
                    context.Sales.Add(sale);
                    context.DebtItems.Remove(item);
                }

                // Handle partial payment
                if (partiallyPaidItem.Item != null)
                {
                    string originalItemName = partiallyPaidItem.Item.ItemName.Contains("] ") ?
                        partiallyPaidItem.Item.ItemName.Split(new[] { "] " }, StringSplitOptions.None)[1] :
                        partiallyPaidItem.Item.ItemName;

                    var sale = new Sale
                    {
                        ItemName = originalItemName,
                        Category = partiallyPaidItem.Item.Category,
                        ItemSold = partiallyPaidItem.PartialQuantity,
                        TotalAmount = partiallyPaidItem.PartialAmount,
                        TimeAndDate = DateTime.Now
                    };
                    context.Sales.Add(sale);

                    partiallyPaidItem.Item.TotalAmount -= partiallyPaidItem.PartialAmount;
                    partiallyPaidItem.Item.ItemSold -= partiallyPaidItem.PartialQuantity;
                    context.DebtItems.Update(partiallyPaidItem.Item);
                }

                customer.ActiveBalance -= paidAmount;
                customer.DebtStatus = customer.ActiveBalance == 0 ? "Clear" : "Unpaid";

                context.SaveChanges();
            }

            LoadDebtHistory();
            textPaidAmount.Clear();
            ClearInputFields();

            MessageBox.Show($"Payment of ₱{paidAmount:N2} recorded successfully!\n\n" +
                "Paid items have been transferred to the Sales Ledger as official sales.",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    _editingDebtId = (int)idProperty.GetValue(selectedItem);
                    string displayName = (string)itemNameProperty.GetValue(selectedItem);
                    string category = categoryProperty != null ? (string)categoryProperty.GetValue(selectedItem) : "";
                    decimal amount = amountProperty != null ? (decimal)amountProperty.GetValue(selectedItem) : 0;

                    txtDlItemName.Text = displayName;
                    txtDlAmount.Text = amount.ToString("0.00");

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

       
       

        private decimal GetProductPrice(AppDbContext context, string itemName)
        {
            var product = context.Products
                .FirstOrDefault(p => p.Name != null && itemName.ToLower().Contains(p.Name.ToLower()));
            return product?.Price ?? 0;
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
                        var allProducts = _inventoryService.GetAllProducts();
                        var product = allProducts.FirstOrDefault(p => itemName.ToLower().Contains(p.Name.ToLower()));

                        if (product != null)
                        {
                            _inventoryService.AddStock(product.Name, quantityToReturn);

                            using (var context = new AppDbContext())
                            {
                                var debtItem = context.DebtItems.Find(debtId);
                                if (debtItem != null)
                                {
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
                        _editingDebtId = 0;
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
        }
    }
}