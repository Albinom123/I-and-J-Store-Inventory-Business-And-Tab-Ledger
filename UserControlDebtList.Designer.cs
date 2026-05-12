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
            label1 = new Label();
            labelTL = new Label();
            panel16 = new Panel();
            buttonReturn = new Button();
            buttonSave = new Button();
            materialCard3 = new MaterialSkin.Controls.MaterialCard();
            textBox3 = new TextBox();
            button7 = new Button();
            button9 = new Button();
            button8 = new Button();
            dataGridView3 = new DataGridView();
            Column5 = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewComboBoxColumn();
            Column2 = new DataGridViewComboBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            panel18.SuspendLayout();
            materialCard1.SuspendLayout();
            panel16.SuspendLayout();
            materialCard3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).BeginInit();
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
            panel18.BackColor = Color.Goldenrod;
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
            materialCard1.Controls.Add(label1);
            materialCard1.Depth = 0;
            materialCard1.Dock = DockStyle.Right;
            materialCard1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard1.Location = new Point(749, 0);
            materialCard1.Margin = new Padding(14);
            materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard1.Name = "materialCard1";
            materialCard1.Padding = new Padding(14);
            materialCard1.Size = new Size(488, 117);
            materialCard1.TabIndex = 15;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(38, 24);
            label1.Name = "label1";
            label1.Size = new Size(119, 20);
            label1.TabIndex = 15;
            label1.Text = "Customer Name:";
            // 
            // labelTL
            // 
            labelTL.AutoSize = true;
            labelTL.Font = new Font("Segoe UI", 24F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            labelTL.ForeColor = Color.OldLace;
            labelTL.Location = new Point(17, 24);
            labelTL.Name = "labelTL";
            labelTL.Size = new Size(413, 54);
            labelTL.TabIndex = 14;
            labelTL.Text = "Costumer's Debt List";
            // 
            // panel16
            // 
            panel16.BackColor = Color.Goldenrod;
            panel16.BorderStyle = BorderStyle.Fixed3D;
            panel16.Controls.Add(buttonReturn);
            panel16.Controls.Add(buttonSave);
            panel16.Controls.Add(materialCard3);
            panel16.Controls.Add(button9);
            panel16.Controls.Add(button8);
            panel16.Dock = DockStyle.Left;
            panel16.Location = new Point(82, 121);
            panel16.Name = "panel16";
            panel16.Size = new Size(248, 689);
            panel16.TabIndex = 18;
            // 
            // buttonReturn
            // 
            buttonReturn.Location = new Point(17, 533);
            buttonReturn.Name = "buttonReturn";
            buttonReturn.Size = new Size(208, 29);
            buttonReturn.TabIndex = 7;
            buttonReturn.Text = "Return";
            buttonReturn.UseVisualStyleBackColor = true;
            buttonReturn.Click += buttonReturn_Click;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(17, 479);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(208, 29);
            buttonSave.TabIndex = 6;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            // 
            // materialCard3
            // 
            materialCard3.BackColor = Color.FromArgb(255, 255, 255);
            materialCard3.Controls.Add(textBox3);
            materialCard3.Controls.Add(button7);
            materialCard3.Depth = 0;
            materialCard3.Dock = DockStyle.Top;
            materialCard3.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard3.Location = new Point(0, 0);
            materialCard3.Margin = new Padding(14);
            materialCard3.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard3.Name = "materialCard3";
            materialCard3.Padding = new Padding(14);
            materialCard3.Size = new Size(244, 142);
            materialCard3.TabIndex = 5;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(17, 46);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(208, 27);
            textBox3.TabIndex = 0;
            // 
            // button7
            // 
            button7.Location = new Point(58, 96);
            button7.Name = "button7";
            button7.Size = new Size(120, 29);
            button7.TabIndex = 1;
            button7.Text = "Add Item +";
            button7.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            button9.Location = new Point(17, 300);
            button9.Name = "button9";
            button9.Size = new Size(208, 29);
            button9.TabIndex = 4;
            button9.Text = "Edit Costumer Debt";
            button9.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            button8.Location = new Point(17, 265);
            button8.Name = "button8";
            button8.Size = new Size(208, 29);
            button8.TabIndex = 3;
            button8.Text = "Delete Costumer Debt";
            button8.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            dataGridView3.AllowUserToOrderColumns = true;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView3.BackgroundColor = SystemColors.ControlLightLight;
            dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView3.Columns.AddRange(new DataGridViewColumn[] { Column5, Column1, Column6, Column2, Column3, Column4 });
            dataGridView3.Dock = DockStyle.Fill;
            dataGridView3.GridColor = SystemColors.MenuText;
            dataGridView3.Location = new Point(330, 121);
            dataGridView3.Name = "dataGridView3";
            dataGridView3.RowHeadersWidth = 51;
            dataGridView3.Size = new Size(993, 689);
            dataGridView3.TabIndex = 19;
            // 
            // Column5
            // 
            Column5.FillWeight = 99.77925F;
            Column5.HeaderText = "Date and Time ";
            Column5.MinimumWidth = 6;
            Column5.Name = "Column5";
            // 
            // Column1
            // 
            Column1.HeaderText = "Item ";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            // 
            // Column6
            // 
            Column6.HeaderText = "Category";
            Column6.MinimumWidth = 6;
            Column6.Name = "Column6";
            Column6.Resizable = DataGridViewTriState.True;
            Column6.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // Column2
            // 
            Column2.FillWeight = 94.23874F;
            Column2.HeaderText = "Type";
            Column2.Items.AddRange(new object[] { "Debt", "Payment" });
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.Resizable = DataGridViewTriState.True;
            Column2.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // Column3
            // 
            Column3.FillWeight = 101.558464F;
            Column3.HeaderText = "Amount ";
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            // 
            // Column4
            // 
            Column4.FillWeight = 104.423553F;
            Column4.HeaderText = "Runing Balance";
            Column4.MinimumWidth = 6;
            Column4.Name = "Column4";
            // 
            // UserControlDebtList
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dataGridView3);
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
            ((System.ComponentModel.ISupportInitialize)dataGridView3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel6;
        private Panel panel10;
        private Panel panel18;
        private MaterialSkin.Controls.MaterialCard materialCard1;
        private Label label1;
        private Label labelTL;
        private Panel panel16;
        private MaterialSkin.Controls.MaterialCard materialCard3;
        private TextBox textBox3;
        private Button button7;
        private Button button9;
        private Button button8;
        private DataGridView dataGridView3;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewComboBoxColumn Column6;
        private DataGridViewComboBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private Button buttonReturn;
        private Button buttonSave;
    }
}
