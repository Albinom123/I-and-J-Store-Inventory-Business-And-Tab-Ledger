using I_and_J_Store_Inventory__Business_And_Tab_Ledger.DATA;
using I_and_J_Store_Inventory__Business_And_Tab_Ledger.Entities;
using Microsoft.EntityFrameworkCore;

namespace I_and_J_Store_Inventory__Business_And_Tab_Ledger
{
    public partial class UserControlDebtList : UserControl
    {
        private Customer _customer;
        private readonly AppDbContext _context; // Add this line

        // The Constructor now requires a Customer object
        public UserControlDebtList(Customer customer)
        {
            InitializeComponent();
            _customer = customer;
            _context = new AppDbContext(); // Initialize it here

            // Display the name in your label (ensure the label name matches your designer)
            lblDlCustomerName.Text = _customer.Name;
            LoadDebtHistory();

            LoadDebtHistory(); // We will write this next to fill the grid
        }




        private void buttonReturn_Click(object sender, EventArgs e)
        {
            Form parentForm = this.FindForm();
            if (parentForm != null)
            {
                parentForm.Close();
            }
        }

        private void LoadDebtHistory()
        {
            // Logic to load specific debts for _customer.Id will go here
        }

        private void btnDlAddItem_Click(object sender, EventArgs e)
        {
            // 1. Validation: Make sure fields aren't empty
            if (string.IsNullOrWhiteSpace(txtDlItemName.Text) || string.IsNullOrWhiteSpace(txtDlAmount.Text))
            {
                MessageBox.Show("Please fill in the Item Name and Amount.");
                return;
            }

            // 2. Parse the amount
            if (!decimal.TryParse(txtDlAmount.Text, out decimal amount))
            {
                MessageBox.Show("Please enter a valid number for the Amount.");
                return;
            }

            // 3. Create the Record
            // We calculate the new running balance: Old Balance + Current Amount
            decimal newRunningBalance = _customer.ActiveBalance + amount;

            var newDebt = new DebtRecord
            {
                CustomerId = _customer.Id,
                DateAndTime = DateTime.Now,
                Item = txtDlItemName.Text.Trim(),
                Category = cmbDlCategory.Text,
                Type = "Debt", // Defaulting to debt for this button
                Amount = amount,
                RunningBalance = newRunningBalance
            };

            // 4. Update the Customer's overall balance in the database
            _customer.ActiveBalance = newRunningBalance;
            _customer.DebtStatus = "Unpaid"; // Automatically flag them

            // 5. Save everything to the database
            _context.DebtRecords.Add(newDebt);
            _context.SaveChanges();

            // 6. Refresh UI
            LoadDebtHistory(); // Refresh the grid
            ClearInputFields();
        }

        private void ClearInputFields()
        {
            txtDlItemName.Clear();
            txtDlAmount.Clear();
            cmbDlCategory.SelectedIndex = -1;
        }

        private void dgvDebtList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the user clicked a valid row (not the header)
            if (e.RowIndex >= 0)
            {
                // Get the specific DebtRecord from the clicked row
                if (dgvDebtList.Rows[e.RowIndex].DataBoundItem is DebtRecord selectedDebt)
                {
                    // Fill your input fields with the row's data
                    txtDlItemName.Text = selectedDebt.Item;
                    cmbDlCategory.Text = selectedDebt.Category;
                    txtDlAmount.Text = selectedDebt.Amount.ToString("0.00");

                    // Pro-tip: Store the ID in the 'Tag' property of the Save button 
                    // so the Save button knows WHICH record to update later.
                    buttonSave.Tag = selectedDebt.Id;
                }
            }
        }

        private void btnDlDeleteDebt_Click(object sender, EventArgs e)
        {
            if (dgvDebtList.CurrentRow?.DataBoundItem is DebtRecord selectedDebt)
            {
                var confirm = MessageBox.Show($"Delete {selectedDebt.Item}? This will reduce the customer's debt by {selectedDebt.Amount:C}.",
                                              "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    // 1. Subtract the amount from the customer's total balance
                    _customer.ActiveBalance -= selectedDebt.Amount;

                    // 2. Remove the record from the database
                    _context.DebtRecords.Remove(selectedDebt);

                    // 3. Save changes to both the Debt record and the Customer balance
                    _context.SaveChanges();

                    // 4. Refresh the grid
                    LoadDebtHistory();
                    ClearInputFields();
                }
            }
        }
    }
}