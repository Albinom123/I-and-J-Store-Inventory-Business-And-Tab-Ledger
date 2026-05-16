namespace I_and_J_Store_Inventory__Business_And_Tab_Ledger
{
    partial class UserControlDebtList
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel6 = new Panel();
            panel10 = new Panel();
            panel18 = new Panel();
            materialCard1 = new MaterialSkin.Controls.MaterialCard();
            lblDlCustomerName = new Label();
            labelTL = new Label();
            panel16 = new Panel();
            buttonReturn = new Button();
            buttonSave = new Button();
            materialCard3 = new MaterialSkin.Controls.MaterialCard();
            btnPaydebt = new Button();
            label1 = new Label();
            textPaidAmount = new TextBox();
            label7 = new Label();
            cmbDlCategory = new ComboBox();
            label8 = new Label();
            txtDlAmount = new TextBox();
            label9 = new Label();
            txtDlItemName = new TextBox();
            btnDlAddItem = new Button();
            btnDlEditDebt = new Button();
            btnDlDeleteDebt = new Button();
            dgvDebtList = new DataGridView();
            colDlDate = new DataGridViewTextBoxColumn();
            colDlItem = new DataGridViewTextBoxColumn();
            colDlCategory = new DataGridViewTextBoxColumn();
            colDlAmount = new DataGridViewTextBoxColumn();
            colDlRunningBalance = new DataGridViewTextBoxColumn();
            panel18.SuspendLayout();
            materialCard1.SuspendLayout();
            panel16.SuspendLayout();
            materialCard3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDebtList).BeginInit();
            SuspendLayout();
            // 
            // panel6
            // 
            panel6.BackColor = Color.DarkGoldenrod;
            panel6.Dock = DockStyle.Left;
            panel6.Location = new Point(0, 0);
            panel6.Name = "panel6";
            panel6.Size = new Size(82, 810);
            panel6.TabIndex = 14;
            // 
            // panel10
            // 
            panel10.BackColor = Color.DarkGoldenrod;
            panel10.Dock = DockStyle.Right;
            panel10.Location = new Point(1323, 0);
            panel10.Name = "panel10";
            panel10.Size = new Size(38, 810);
            panel10.TabIndex = 15;
            // 
            // panel18
            // 
            panel18.BackColor = SystemColors.ControlLightLight;
            panel18.BorderStyle = BorderStyle.Fixed3D;
            panel18.Controls.Add(materialCard1);
            panel18.Controls.Add(labelTL);
            panel18.Dock = DockStyle.Top;
            panel18.Location = new Point(82, 0);
            panel18.Name = "panel18";
            panel18.Size = new Size(1241, 121);
            panel18.TabIndex = 17;
            // 
            // materialCard1
            // 
            materialCard1.BackColor = Color.FromArgb(255, 255, 255);
            materialCard1.Controls.Add(lblDlCustomerName);
            materialCard1.Depth = 0;
            materialCard1.Dock = DockStyle.Right;
            materialCard1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard1.Location = new Point(766, 0);
            materialCard1.Margin = new Padding(14);
            materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard1.Name = "materialCard1";
            materialCard1.Padding = new Padding(14);
            materialCard1.Size = new Size(471, 117);
            materialCard1.TabIndex = 15;
            // 
            // lblDlCustomerName
            // 
            lblDlCustomerName.AutoSize = true;
            lblDlCustomerName.Location = new Point(38, 24);
            lblDlCustomerName.Name = "lblDlCustomerName";
            lblDlCustomerName.Size = new Size(119, 20);
            lblDlCustomerName.TabIndex = 15;
            lblDlCustomerName.Text = "Customer Name:";
            // 
            // labelTL
            // 
            labelTL.AutoSize = true;
            labelTL.BackColor = SystemColors.ControlLightLight;
            labelTL.Font = new Font("Segoe UI", 24F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            labelTL.ForeColor = Color.Goldenrod;
            labelTL.Location = new Point(17, 24);
            labelTL.Name = "labelTL";
            labelTL.Size = new Size(413, 54);
            labelTL.TabIndex = 14;
            labelTL.Text = "Costumer's Debt List";
            // 
            // panel16
            // 
            panel16.BackColor = SystemColors.ControlLightLight;
            panel16.BorderStyle = BorderStyle.Fixed3D;
            panel16.Controls.Add(buttonReturn);
            panel16.Controls.Add(buttonSave);
            panel16.Controls.Add(materialCard3);
            panel16.Controls.Add(btnDlEditDebt);
            panel16.Controls.Add(btnDlDeleteDebt);
            panel16.Dock = DockStyle.Left;
            panel16.Location = new Point(82, 121);
            panel16.Name = "panel16";
            panel16.Size = new Size(248, 689);
            panel16.TabIndex = 18;
            // 
            // buttonReturn
            // 
            buttonReturn.Location = new Point(17, 623);
            buttonReturn.Name = "buttonReturn";
            buttonReturn.Size = new Size(208, 29);
            buttonReturn.TabIndex = 7;
            buttonReturn.Text = "Return";
            buttonReturn.UseVisualStyleBackColor = true;
            buttonReturn.Click += buttonReturn_Click;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(17, 575);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(208, 29);
            buttonSave.TabIndex = 6;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            // 
            // materialCard3
            // 
            materialCard3.BackColor = Color.FromArgb(255, 255, 255);
            materialCard3.Controls.Add(btnPaydebt);
            materialCard3.Controls.Add(label1);
            materialCard3.Controls.Add(textPaidAmount);
            materialCard3.Controls.Add(label7);
            materialCard3.Controls.Add(cmbDlCategory);
            materialCard3.Controls.Add(label8);
            materialCard3.Controls.Add(txtDlAmount);
            materialCard3.Controls.Add(label9);
            materialCard3.Controls.Add(txtDlItemName);
            materialCard3.Controls.Add(btnDlAddItem);
            materialCard3.Depth = 0;
            materialCard3.Dock = DockStyle.Top;
            materialCard3.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard3.Location = new Point(0, 0);
            materialCard3.Margin = new Padding(14);
            materialCard3.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard3.Name = "materialCard3";
            materialCard3.Padding = new Padding(14);
            materialCard3.Size = new Size(244, 420);
            materialCard3.TabIndex = 5;
            // 
            // btnPaydebt
            // 
            btnPaydebt.Location = new Point(58, 336);
            btnPaydebt.Name = "btnPaydebt";
            btnPaydebt.Size = new Size(120, 29);
            btnPaydebt.TabIndex = 15;
            btnPaydebt.Text = "Pay Debt";
            btnPaydebt.UseVisualStyleBackColor = true;
            btnPaydebt.Click += btnPaydebt_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 280);
            label1.Name = "label1";
            label1.Size = new Size(98, 20);
            label1.TabIndex = 14;
            label1.Text = "Paid Amount ";
            // 
            // textPaidAmount
            // 
            textPaidAmount.Location = new Point(19, 303);
            textPaidAmount.Name = "textPaidAmount";
            textPaidAmount.Size = new Size(208, 27);
            textPaidAmount.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(15, 95);
            label7.Name = "label7";
            label7.Size = new Size(69, 20);
            label7.TabIndex = 12;
            label7.Text = "Category";
            // 
            // cmbDlCategory
            // 
            cmbDlCategory.FormattingEnabled = true;
            cmbDlCategory.Items.AddRange(new object[] { "Rice", "Cooking Essentials", "Drinks", "Snacks/Foods", "Cleaning Essentials", "Packaging & Disposables" });
            cmbDlCategory.Location = new Point(17, 118);
            cmbDlCategory.Name = "cmbDlCategory";
            cmbDlCategory.Size = new Size(208, 28);
            cmbDlCategory.TabIndex = 11;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(17, 159);
            label8.Name = "label8";
            label8.Size = new Size(102, 20);
            label8.TabIndex = 10;
            label8.Text = "Price Amount ";
            // 
            // txtDlAmount
            // 
            txtDlAmount.Location = new Point(17, 182);
            txtDlAmount.Name = "txtDlAmount";
            txtDlAmount.Size = new Size(208, 27);
            txtDlAmount.TabIndex = 9;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(15, 31);
            label9.Name = "label9";
            label9.Size = new Size(83, 20);
            label9.TabIndex = 8;
            label9.Text = "Item Name";
            // 
            // txtDlItemName
            // 
            txtDlItemName.Location = new Point(17, 54);
            txtDlItemName.Name = "txtDlItemName";
            txtDlItemName.Size = new Size(208, 27);
            txtDlItemName.TabIndex = 7;
            // 
            // btnDlAddItem
            // 
            btnDlAddItem.Location = new Point(58, 236);
            btnDlAddItem.Name = "btnDlAddItem";
            btnDlAddItem.Size = new Size(120, 29);
            btnDlAddItem.TabIndex = 1;
            btnDlAddItem.Text = "Add Item +";
            btnDlAddItem.UseVisualStyleBackColor = true;
            btnDlAddItem.Click += btnDlAddItem_Click;
            // 
            // btnDlEditDebt
            // 
            btnDlEditDebt.Location = new Point(17, 519);
            btnDlEditDebt.Name = "btnDlEditDebt";
            btnDlEditDebt.Size = new Size(208, 29);
            btnDlEditDebt.TabIndex = 4;
            btnDlEditDebt.Text = "Edit Costumer Debt";
            btnDlEditDebt.UseVisualStyleBackColor = true;
            // 
            // btnDlDeleteDebt
            // 
            btnDlDeleteDebt.Location = new Point(17, 484);
            btnDlDeleteDebt.Name = "btnDlDeleteDebt";
            btnDlDeleteDebt.Size = new Size(208, 29);
            btnDlDeleteDebt.TabIndex = 3;
            btnDlDeleteDebt.Text = "Delete Costumer Debt";
            btnDlDeleteDebt.UseVisualStyleBackColor = true;
            btnDlDeleteDebt.Click += btnDlDeleteDebt_Click;
            // 
            // dgvDebtList
            // 
            dgvDebtList.AllowUserToOrderColumns = true;
            dgvDebtList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDebtList.BackgroundColor = SystemColors.ControlLightLight;
            dgvDebtList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDebtList.Columns.AddRange(new DataGridViewColumn[] { colDlDate, colDlItem, colDlCategory, colDlAmount, colDlRunningBalance });
            dgvDebtList.Dock = DockStyle.Fill;
            dgvDebtList.GridColor = SystemColors.MenuText;
            dgvDebtList.Location = new Point(330, 121);
            dgvDebtList.Name = "dgvDebtList";
            dgvDebtList.RowHeadersWidth = 51;
            dgvDebtList.Size = new Size(993, 689);
            dgvDebtList.TabIndex = 19;
            // 
            // colDlDate
            // 
            colDlDate.FillWeight = 99.77925F;
            colDlDate.HeaderText = "Date and Time ";
            colDlDate.MinimumWidth = 6;
            colDlDate.Name = "colDlDate";
            // 
            // colDlItem
            // 
            colDlItem.HeaderText = "Item ";
            colDlItem.MinimumWidth = 6;
            colDlItem.Name = "colDlItem";
            // 
            // colDlCategory
            // 
            colDlCategory.HeaderText = "Category";
            colDlCategory.MinimumWidth = 6;
            colDlCategory.Name = "colDlCategory";
            colDlCategory.Resizable = DataGridViewTriState.True;
            // 
            // colDlAmount
            // 
            colDlAmount.FillWeight = 101.558464F;
            colDlAmount.HeaderText = "Amount ";
            colDlAmount.MinimumWidth = 6;
            colDlAmount.Name = "colDlAmount";
            // 
            // colDlRunningBalance
            // 
            colDlRunningBalance.FillWeight = 104.423553F;
            colDlRunningBalance.HeaderText = "Runing Balance";
            colDlRunningBalance.MinimumWidth = 6;
            colDlRunningBalance.Name = "colDlRunningBalance";
            // 
            // UserControlDebtList
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvDebtList);
            Controls.Add(panel16);
            Controls.Add(panel18);
            Controls.Add(panel10);
            Controls.Add(panel6);
            Name = "UserControlDebtList";
            Size = new Size(1361, 810);
            panel18.ResumeLayout(false);
            panel18.PerformLayout();
            materialCard1.ResumeLayout(false);
            materialCard1.PerformLayout();
            panel16.ResumeLayout(false);
            materialCard3.ResumeLayout(false);
            materialCard3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDebtList).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel6;
        private Panel panel10;
        private Panel panel18;
        private MaterialSkin.Controls.MaterialCard materialCard1;
        private Label lblDlCustomerName;
        private Label labelTL;
        private Panel panel16;
        private MaterialSkin.Controls.MaterialCard materialCard3;
        private Button btnDlAddItem;
        private Button btnDlEditDebt;
        private Button btnDlDeleteDebt;
        private DataGridView dgvDebtList;
        private Button buttonReturn;
        private Button buttonSave;
        private Label label7;
        private ComboBox cmbDlCategory;
        private Label label8;
        private TextBox txtDlAmount;
        private Label label9;
        private TextBox txtDlItemName;
        private DataGridViewTextBoxColumn colDlDate;
        private DataGridViewTextBoxColumn colDlItem;
        private DataGridViewTextBoxColumn colDlCategory;
        private DataGridViewTextBoxColumn colDlAmount;
        private DataGridViewTextBoxColumn colDlRunningBalance;
        private Button btnPaydebt;
        private Label label1;
        private TextBox textPaidAmount;
    }
}
