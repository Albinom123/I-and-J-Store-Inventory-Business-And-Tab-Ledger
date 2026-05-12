using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace I_and_J_Store_Inventory__Business_And_Tab_Ledger
{
    public partial class UserControlDebtList : UserControl
    {
        public UserControlDebtList()
        {
            InitializeComponent();
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            // Find the Form that this UserControl is currently sitting in
            Form parentForm = this.FindForm();

            if (parentForm != null)
            {
                // Close the floating host form
                parentForm.Close();
            }
        }
    }
}
