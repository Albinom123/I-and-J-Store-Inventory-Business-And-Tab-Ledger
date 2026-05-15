namespace I_and_J_Store_Inventory__Business_And_Tab_Ledger
{
    partial class Form1
    {
        
        private System.ComponentModel.IContainer components = null;

       
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code


        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            imageList1 = new ImageList(components);
            SideBarTimer = new System.Windows.Forms.Timer(components);
            tabPage5 = new TabPage();
            panel9 = new Panel();
            panel7 = new Panel();
            tabPage4 = new TabPage();
            panel17 = new Panel();
            dgvTabLedger = new DataGridView();
            panel16 = new Panel();
            BtnVCDL = new Button();
            materialCard3 = new MaterialSkin.Controls.MaterialCard();
            label2 = new Label();
            txtTabCustomerName = new TextBox();
            btnTabAddCustomer = new Button();
            btnTabEditCustomer = new Button();
            btnTabDeleteCustomer = new Button();
            panel18 = new Panel();
            btnTabSearch = new Button();
            cmbTabFilterStatus = new ComboBox();
            txtTabSearchName = new TextBox();
            materialCard4 = new MaterialSkin.Controls.MaterialCard();
            labelTL = new Label();
            panel10 = new Panel();
            panel6 = new Panel();
            tabPage3 = new TabPage();
            panel20 = new Panel();
            materialCard6 = new MaterialSkin.Controls.MaterialCard();
            label7 = new Label();
            cmbSlCategory = new ComboBox();
            label8 = new Label();
            txtSlItemPrice = new TextBox();
            label9 = new Label();
            txtSlItemName = new TextBox();
            btnSlAddItem = new Button();
            btnSlEditItem = new Button();
            btnSlDeleteItem = new Button();
            panel14 = new Panel();
            dgvSalesLedger = new DataGridView();
            colSLName = new DataGridViewTextBoxColumn();
            colSLItemsold = new DataGridViewTextBoxColumn();
            colSLCategory = new DataGridViewTextBoxColumn();
            colSlPrice = new DataGridViewTextBoxColumn();
            colSLDateTime = new DataGridViewTextBoxColumn();
            panel13 = new Panel();
            dtpSlFilterDate = new MaterialSkin.Controls.MaterialCard();
            labelSL = new Label();
            dateTimePicker1 = new DateTimePicker();
            btnSlShowDate = new Button();
            btnSlSearch = new Button();
            txtSlSearch = new TextBox();
            cmbSlFilterCategory = new ComboBox();
            btnSlFilterApply = new Button();
            panel11 = new Panel();
            panel5 = new Panel();
            tabPage2 = new TabPage();
            MainInvPanel = new Panel();
            panel19 = new Panel();
            materialCard5 = new MaterialSkin.Controls.MaterialCard();
            label5 = new Label();
            txtInvItemStocks = new TextBox();
            label4 = new Label();
            cmbInvCategory = new ComboBox();
            label3 = new Label();
            txtInvItemPrice = new TextBox();
            label1 = new Label();
            txtInvItemName = new TextBox();
            btnInvAddItem = new Button();
            btnInvEditItem = new Button();
            btnInvDeleteItem = new Button();
            panel4 = new Panel();
            dgvInventory = new DataGridView();
            colInvName = new DataGridViewTextBoxColumn();
            colInvCategory = new DataGridViewTextBoxColumn();
            colInvPrice = new DataGridViewTextBoxColumn();
            colInvStocks = new DataGridViewTextBoxColumn();
            panel3 = new Panel();
            materialCard1 = new MaterialSkin.Controls.MaterialCard();
            labelInv = new Label();
            btnInvSearch = new Button();
            txtInvSearch = new TextBox();
            cmbInvFilterCategory = new ComboBox();
            btnInvFilterApply = new Button();
            panel2 = new Panel();
            panel1 = new Panel();
            tabPage1 = new TabPage();
            panel15 = new Panel();
            panel12 = new Panel();
            panel8 = new Panel();
            materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            colTabName = new DataGridViewTextBoxColumn();
            colTabBalance = new DataGridViewTextBoxColumn();
            colTabStatus = new DataGridViewTextBoxColumn();
            colTabDate = new DataGridViewTextBoxColumn();
            tabPage5.SuspendLayout();
            tabPage4.SuspendLayout();
            panel17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTabLedger).BeginInit();
            panel16.SuspendLayout();
            materialCard3.SuspendLayout();
            panel18.SuspendLayout();
            materialCard4.SuspendLayout();
            tabPage3.SuspendLayout();
            panel20.SuspendLayout();
            materialCard6.SuspendLayout();
            panel14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSalesLedger).BeginInit();
            panel13.SuspendLayout();
            dtpSlFilterDate.SuspendLayout();
            tabPage2.SuspendLayout();
            MainInvPanel.SuspendLayout();
            panel19.SuspendLayout();
            materialCard5.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInventory).BeginInit();
            panel3.SuspendLayout();
            materialCard1.SuspendLayout();
            tabPage1.SuspendLayout();
            materialTabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "home.png");
            imageList1.Images.SetKeyName(1, "Inventory.png");
            imageList1.Images.SetKeyName(2, "sales.png");
            imageList1.Images.SetKeyName(3, "SalesTab.png");
            imageList1.Images.SetKeyName(4, "settings.png");
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(panel9);
            tabPage5.Controls.Add(panel7);
            tabPage5.ImageKey = "settings.png";
            tabPage5.Location = new Point(4, 34);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(1341, 700);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "Settings";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // panel9
            // 
            panel9.BackColor = Color.Black;
            panel9.Dock = DockStyle.Right;
            panel9.Location = new Point(1300, 3);
            panel9.Name = "panel9";
            panel9.Size = new Size(38, 694);
            panel9.TabIndex = 13;
            // 
            // panel7
            // 
            panel7.BackColor = Color.Black;
            panel7.Dock = DockStyle.Left;
            panel7.Location = new Point(3, 3);
            panel7.Name = "panel7";
            panel7.Size = new Size(82, 694);
            panel7.TabIndex = 12;
            // 
            // tabPage4
            // 
            tabPage4.BackColor = Color.White;
            tabPage4.Controls.Add(panel17);
            tabPage4.Controls.Add(panel16);
            tabPage4.Controls.Add(panel18);
            tabPage4.Controls.Add(panel10);
            tabPage4.Controls.Add(panel6);
            tabPage4.ImageKey = "SalesTab.png";
            tabPage4.Location = new Point(4, 34);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(1341, 700);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Tab Ledger";
            // 
            // panel17
            // 
            panel17.Controls.Add(dgvTabLedger);
            panel17.Dock = DockStyle.Fill;
            panel17.Location = new Point(341, 124);
            panel17.Name = "panel17";
            panel17.Size = new Size(959, 573);
            panel17.TabIndex = 17;
            // 
            // dgvTabLedger
            // 
            dgvTabLedger.AllowUserToOrderColumns = true;
            dgvTabLedger.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTabLedger.BackgroundColor = SystemColors.ControlLightLight;
            dgvTabLedger.BorderStyle = BorderStyle.Fixed3D;
            dgvTabLedger.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTabLedger.Columns.AddRange(new DataGridViewColumn[] { colTabName, colTabBalance, colTabStatus, colTabDate });
            dgvTabLedger.Dock = DockStyle.Fill;
            dgvTabLedger.GridColor = SystemColors.MenuText;
            dgvTabLedger.Location = new Point(0, 0);
            dgvTabLedger.Name = "dgvTabLedger";
            dgvTabLedger.RowHeadersWidth = 51;
            dgvTabLedger.Size = new Size(959, 573);
            dgvTabLedger.TabIndex = 0;
            // 
            // panel16
            // 
            panel16.BorderStyle = BorderStyle.Fixed3D;
            panel16.Controls.Add(BtnVCDL);
            panel16.Controls.Add(materialCard3);
            panel16.Controls.Add(btnTabEditCustomer);
            panel16.Controls.Add(btnTabDeleteCustomer);
            panel16.Dock = DockStyle.Left;
            panel16.Location = new Point(85, 124);
            panel16.Name = "panel16";
            panel16.Size = new Size(256, 573);
            panel16.TabIndex = 16;
            // 
            // BtnVCDL
            // 
            BtnVCDL.Location = new Point(17, 289);
            BtnVCDL.Name = "BtnVCDL";
            BtnVCDL.Size = new Size(208, 29);
            BtnVCDL.TabIndex = 6;
            BtnVCDL.Text = "View Customer Debt List";
            BtnVCDL.UseVisualStyleBackColor = true;
            BtnVCDL.Click += BtnVCDL_Click;
            // 
            // materialCard3
            // 
            materialCard3.BackColor = Color.FromArgb(255, 255, 255);
            materialCard3.BorderStyle = BorderStyle.Fixed3D;
            materialCard3.Controls.Add(label2);
            materialCard3.Controls.Add(txtTabCustomerName);
            materialCard3.Controls.Add(btnTabAddCustomer);
            materialCard3.Depth = 0;
            materialCard3.Dock = DockStyle.Top;
            materialCard3.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard3.Location = new Point(0, 0);
            materialCard3.Margin = new Padding(14);
            materialCard3.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard3.Name = "materialCard3";
            materialCard3.Padding = new Padding(14);
            materialCard3.Size = new Size(252, 142);
            materialCard3.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 14);
            label2.Name = "label2";
            label2.Size = new Size(183, 20);
            label2.TabIndex = 2;
            label2.Text = "Register Customer's Name";
            // 
            // txtTabCustomerName
            // 
            txtTabCustomerName.Location = new Point(17, 46);
            txtTabCustomerName.Name = "txtTabCustomerName";
            txtTabCustomerName.Size = new Size(208, 27);
            txtTabCustomerName.TabIndex = 0;
            // 
            // btnTabAddCustomer
            // 
            btnTabAddCustomer.Location = new Point(17, 93);
            btnTabAddCustomer.Name = "btnTabAddCustomer";
            btnTabAddCustomer.Size = new Size(94, 29);
            btnTabAddCustomer.TabIndex = 1;
            btnTabAddCustomer.Text = "Add +";
            btnTabAddCustomer.UseVisualStyleBackColor = true;
            btnTabAddCustomer.Click += btnTabAddCustomer_Click;
            // 
            // btnTabEditCustomer
            // 
            btnTabEditCustomer.Location = new Point(17, 234);
            btnTabEditCustomer.Name = "btnTabEditCustomer";
            btnTabEditCustomer.Size = new Size(208, 29);
            btnTabEditCustomer.TabIndex = 4;
            btnTabEditCustomer.Text = "Edit Customer Info";
            btnTabEditCustomer.UseVisualStyleBackColor = true;
            // 
            // btnTabDeleteCustomer
            // 
            btnTabDeleteCustomer.Location = new Point(17, 183);
            btnTabDeleteCustomer.Name = "btnTabDeleteCustomer";
            btnTabDeleteCustomer.Size = new Size(208, 29);
            btnTabDeleteCustomer.TabIndex = 3;
            btnTabDeleteCustomer.Text = "Delete Customer Info";
            btnTabDeleteCustomer.UseVisualStyleBackColor = true;
            // 
            // panel18
            // 
            panel18.Controls.Add(btnTabSearch);
            panel18.Controls.Add(cmbTabFilterStatus);
            panel18.Controls.Add(txtTabSearchName);
            panel18.Controls.Add(materialCard4);
            panel18.Dock = DockStyle.Top;
            panel18.Location = new Point(85, 3);
            panel18.Name = "panel18";
            panel18.Size = new Size(1215, 121);
            panel18.TabIndex = 15;
            // 
            // btnTabSearch
            // 
            btnTabSearch.Location = new Point(547, 80);
            btnTabSearch.Name = "btnTabSearch";
            btnTabSearch.Size = new Size(94, 29);
            btnTabSearch.TabIndex = 18;
            btnTabSearch.Text = "Search";
            btnTabSearch.UseVisualStyleBackColor = true;
            // 
            // cmbTabFilterStatus
            // 
            cmbTabFilterStatus.FormattingEnabled = true;
            cmbTabFilterStatus.Items.AddRange(new object[] { "All", "Active", "Settled" });
            cmbTabFilterStatus.Location = new Point(751, 80);
            cmbTabFilterStatus.Name = "cmbTabFilterStatus";
            cmbTabFilterStatus.Size = new Size(234, 28);
            cmbTabFilterStatus.TabIndex = 17;
            cmbTabFilterStatus.Text = "Filter by Status:";
            // 
            // txtTabSearchName
            // 
            txtTabSearchName.Location = new Point(309, 81);
            txtTabSearchName.Name = "txtTabSearchName";
            txtTabSearchName.Size = new Size(214, 27);
            txtTabSearchName.TabIndex = 15;
            txtTabSearchName.Text = "Search Costumer Name:";
            // 
            // materialCard4
            // 
            materialCard4.BackColor = Color.FromArgb(255, 255, 255);
            materialCard4.BorderStyle = BorderStyle.Fixed3D;
            materialCard4.Controls.Add(labelTL);
            materialCard4.Depth = 0;
            materialCard4.Dock = DockStyle.Fill;
            materialCard4.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard4.Location = new Point(0, 0);
            materialCard4.Margin = new Padding(14);
            materialCard4.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard4.Name = "materialCard4";
            materialCard4.Padding = new Padding(14);
            materialCard4.Size = new Size(1215, 121);
            materialCard4.TabIndex = 19;
            // 
            // labelTL
            // 
            labelTL.AutoSize = true;
            labelTL.Font = new Font("Segoe UI", 24F, FontStyle.Bold | FontStyle.Underline);
            labelTL.ForeColor = Color.Goldenrod;
            labelTL.Location = new Point(17, 12);
            labelTL.Name = "labelTL";
            labelTL.Size = new Size(231, 54);
            labelTL.TabIndex = 0;
            labelTL.Text = "Tab Ledger";
            // 
            // panel10
            // 
            panel10.BackColor = SystemColors.Control;
            panel10.Dock = DockStyle.Right;
            panel10.Location = new Point(1300, 3);
            panel10.Name = "panel10";
            panel10.Size = new Size(38, 694);
            panel10.TabIndex = 13;
            // 
            // panel6
            // 
            panel6.BackColor = SystemColors.Control;
            panel6.Dock = DockStyle.Left;
            panel6.Location = new Point(3, 3);
            panel6.Name = "panel6";
            panel6.Size = new Size(82, 694);
            panel6.TabIndex = 12;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(panel20);
            tabPage3.Controls.Add(panel14);
            tabPage3.Controls.Add(panel13);
            tabPage3.Controls.Add(panel11);
            tabPage3.Controls.Add(panel5);
            tabPage3.ImageKey = "sales.png";
            tabPage3.Location = new Point(4, 34);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1341, 700);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Sales Ledger";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel20
            // 
            panel20.BorderStyle = BorderStyle.Fixed3D;
            panel20.Controls.Add(materialCard6);
            panel20.Controls.Add(btnSlEditItem);
            panel20.Controls.Add(btnSlDeleteItem);
            panel20.Dock = DockStyle.Left;
            panel20.Location = new Point(85, 128);
            panel20.Name = "panel20";
            panel20.Size = new Size(242, 569);
            panel20.TabIndex = 18;
            // 
            // materialCard6
            // 
            materialCard6.BackColor = Color.FromArgb(255, 255, 255);
            materialCard6.BorderStyle = BorderStyle.Fixed3D;
            materialCard6.Controls.Add(label7);
            materialCard6.Controls.Add(cmbSlCategory);
            materialCard6.Controls.Add(label8);
            materialCard6.Controls.Add(txtSlItemPrice);
            materialCard6.Controls.Add(label9);
            materialCard6.Controls.Add(txtSlItemName);
            materialCard6.Controls.Add(btnSlAddItem);
            materialCard6.Depth = 0;
            materialCard6.Dock = DockStyle.Top;
            materialCard6.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard6.Location = new Point(0, 0);
            materialCard6.Margin = new Padding(14);
            materialCard6.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard6.Name = "materialCard6";
            materialCard6.Padding = new Padding(14);
            materialCard6.Size = new Size(238, 385);
            materialCard6.TabIndex = 5;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(15, 87);
            label7.Name = "label7";
            label7.Size = new Size(69, 20);
            label7.TabIndex = 6;
            label7.Text = "Category";
            // 
            // cmbSlCategory
            // 
            cmbSlCategory.FormattingEnabled = true;
            cmbSlCategory.Items.AddRange(new object[] { "Rice", "Cooking Essentials", "Drinks", "Snacks/Foods", "Cleaning Essentials", "Packaging & Disposables" });
            cmbSlCategory.Location = new Point(17, 110);
            cmbSlCategory.Name = "cmbSlCategory";
            cmbSlCategory.Size = new Size(208, 28);
            cmbSlCategory.TabIndex = 5;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(17, 151);
            label8.Name = "label8";
            label8.Size = new Size(75, 20);
            label8.TabIndex = 4;
            label8.Text = "Item Price";
            // 
            // txtSlItemPrice
            // 
            txtSlItemPrice.Location = new Point(17, 174);
            txtSlItemPrice.Name = "txtSlItemPrice";
            txtSlItemPrice.Size = new Size(208, 27);
            txtSlItemPrice.TabIndex = 3;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(15, 23);
            label9.Name = "label9";
            label9.Size = new Size(83, 20);
            label9.TabIndex = 2;
            label9.Text = "Item Name";
            // 
            // txtSlItemName
            // 
            txtSlItemName.Location = new Point(17, 46);
            txtSlItemName.Name = "txtSlItemName";
            txtSlItemName.Size = new Size(208, 27);
            txtSlItemName.TabIndex = 0;
            // 
            // btnSlAddItem
            // 
            btnSlAddItem.Location = new Point(15, 335);
            btnSlAddItem.Name = "btnSlAddItem";
            btnSlAddItem.Size = new Size(94, 29);
            btnSlAddItem.TabIndex = 1;
            btnSlAddItem.Text = "Add +";
            btnSlAddItem.UseVisualStyleBackColor = true;
            btnSlAddItem.Click += btnSlAddItem_Click;
            // 
            // btnSlEditItem
            // 
            btnSlEditItem.Location = new Point(17, 453);
            btnSlEditItem.Name = "btnSlEditItem";
            btnSlEditItem.Size = new Size(208, 29);
            btnSlEditItem.TabIndex = 4;
            btnSlEditItem.Text = "Edit Item";
            btnSlEditItem.UseVisualStyleBackColor = true;
            btnSlEditItem.Click += btnSlEditItem_Click;
            // 
            // btnSlDeleteItem
            // 
            btnSlDeleteItem.Location = new Point(17, 418);
            btnSlDeleteItem.Name = "btnSlDeleteItem";
            btnSlDeleteItem.Size = new Size(208, 29);
            btnSlDeleteItem.TabIndex = 3;
            btnSlDeleteItem.Text = "Delete Item ";
            btnSlDeleteItem.UseVisualStyleBackColor = true;
            btnSlDeleteItem.Click += btnSlDeleteItem_Click;
            // 
            // panel14
            // 
            panel14.Controls.Add(dgvSalesLedger);
            panel14.Dock = DockStyle.Right;
            panel14.Location = new Point(333, 128);
            panel14.Name = "panel14";
            panel14.Size = new Size(967, 569);
            panel14.TabIndex = 15;
            // 
            // dgvSalesLedger
            // 
            dgvSalesLedger.AllowUserToOrderColumns = true;
            dgvSalesLedger.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSalesLedger.BackgroundColor = SystemColors.ButtonFace;
            dgvSalesLedger.BorderStyle = BorderStyle.Fixed3D;
            dgvSalesLedger.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSalesLedger.Columns.AddRange(new DataGridViewColumn[] { colSLName, colSLItemsold, colSLCategory, colSlPrice, colSLDateTime });
            dgvSalesLedger.Dock = DockStyle.Fill;
            dgvSalesLedger.GridColor = SystemColors.InactiveCaptionText;
            dgvSalesLedger.Location = new Point(0, 0);
            dgvSalesLedger.Name = "dgvSalesLedger";
            dgvSalesLedger.RowHeadersWidth = 51;
            dgvSalesLedger.Size = new Size(967, 569);
            dgvSalesLedger.TabIndex = 11;
            // 
            // colSLName
            // 
            dataGridViewCellStyle1.Format = "₱ #,##0.00";
            dataGridViewCellStyle1.NullValue = null;
            colSLName.DefaultCellStyle = dataGridViewCellStyle1;
            colSLName.HeaderText = "Item Name ";
            colSLName.MinimumWidth = 6;
            colSLName.Name = "colSLName";
            // 
            // colSLItemsold
            // 
            colSLItemsold.HeaderText = "Item Sold";
            colSLItemsold.MinimumWidth = 6;
            colSLItemsold.Name = "colSLItemsold";
            // 
            // colSLCategory
            // 
            colSLCategory.HeaderText = "Category";
            colSLCategory.MinimumWidth = 6;
            colSLCategory.Name = "colSLCategory";
            colSLCategory.Resizable = DataGridViewTriState.True;
            // 
            // colSlPrice
            // 
            colSlPrice.HeaderText = "Total Amount (₱)";
            colSlPrice.MinimumWidth = 6;
            colSlPrice.Name = "colSlPrice";
            // 
            // colSLDateTime
            // 
            colSLDateTime.HeaderText = "Time And Date:";
            colSLDateTime.MinimumWidth = 6;
            colSLDateTime.Name = "colSLDateTime";
            // 
            // panel13
            // 
            panel13.BackColor = Color.White;
            panel13.Controls.Add(dtpSlFilterDate);
            panel13.Dock = DockStyle.Top;
            panel13.Location = new Point(85, 3);
            panel13.Name = "panel13";
            panel13.Size = new Size(1215, 125);
            panel13.TabIndex = 14;
            // 
            // dtpSlFilterDate
            // 
            dtpSlFilterDate.BackColor = Color.FromArgb(255, 255, 255);
            dtpSlFilterDate.BorderStyle = BorderStyle.Fixed3D;
            dtpSlFilterDate.Controls.Add(labelSL);
            dtpSlFilterDate.Controls.Add(dateTimePicker1);
            dtpSlFilterDate.Controls.Add(btnSlShowDate);
            dtpSlFilterDate.Controls.Add(btnSlSearch);
            dtpSlFilterDate.Controls.Add(txtSlSearch);
            dtpSlFilterDate.Controls.Add(cmbSlFilterCategory);
            dtpSlFilterDate.Controls.Add(btnSlFilterApply);
            dtpSlFilterDate.Depth = 0;
            dtpSlFilterDate.Dock = DockStyle.Fill;
            dtpSlFilterDate.ForeColor = Color.FromArgb(222, 0, 0, 0);
            dtpSlFilterDate.Location = new Point(0, 0);
            dtpSlFilterDate.Margin = new Padding(14);
            dtpSlFilterDate.MouseState = MaterialSkin.MouseState.HOVER;
            dtpSlFilterDate.Name = "dtpSlFilterDate";
            dtpSlFilterDate.Padding = new Padding(14);
            dtpSlFilterDate.Size = new Size(1215, 125);
            dtpSlFilterDate.TabIndex = 10;
            // 
            // labelSL
            // 
            labelSL.AutoSize = true;
            labelSL.Font = new Font("Segoe UI", 24F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            labelSL.ForeColor = Color.Goldenrod;
            labelSL.Location = new Point(17, 12);
            labelSL.Name = "labelSL";
            labelSL.Size = new Size(259, 54);
            labelSL.TabIndex = 13;
            labelSL.Text = "Sales Ledger";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(188, 85);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(266, 27);
            dateTimePicker1.TabIndex = 12;
            // 
            // btnSlShowDate
            // 
            btnSlShowDate.BackColor = SystemColors.ButtonFace;
            btnSlShowDate.Location = new Point(17, 84);
            btnSlShowDate.Name = "btnSlShowDate";
            btnSlShowDate.Size = new Size(165, 29);
            btnSlShowDate.TabIndex = 11;
            btnSlShowDate.Text = "Show Specific Date:";
            btnSlShowDate.UseVisualStyleBackColor = false;
            btnSlShowDate.Click += btnSlShowDate_Click;
            // 
            // btnSlSearch
            // 
            btnSlSearch.BackColor = SystemColors.ButtonHighlight;
            btnSlSearch.Location = new Point(1073, 37);
            btnSlSearch.Name = "btnSlSearch";
            btnSlSearch.Size = new Size(113, 29);
            btnSlSearch.TabIndex = 9;
            btnSlSearch.Text = "Search";
            btnSlSearch.UseVisualStyleBackColor = false;
            btnSlSearch.Click += btnSlSearch_Click;
            // 
            // txtSlSearch
            // 
            txtSlSearch.Location = new Point(782, 39);
            txtSlSearch.Name = "txtSlSearch";
            txtSlSearch.Size = new Size(285, 27);
            txtSlSearch.TabIndex = 8;
            txtSlSearch.Text = "Enter item name...";
            // 
            // cmbSlFilterCategory
            // 
            cmbSlFilterCategory.FormattingEnabled = true;
            cmbSlFilterCategory.Items.AddRange(new object[] { "All", "Rice", "Cooking Essentials", "Drinks", "Snacks/Foods", "Cleaning Essentials", "Packaging & Disposables" });
            cmbSlFilterCategory.Location = new Point(782, 84);
            cmbSlFilterCategory.Name = "cmbSlFilterCategory";
            cmbSlFilterCategory.Size = new Size(285, 28);
            cmbSlFilterCategory.TabIndex = 4;
            cmbSlFilterCategory.Text = "Categories";
            // 
            // btnSlFilterApply
            // 
            btnSlFilterApply.BackColor = SystemColors.ButtonFace;
            btnSlFilterApply.Location = new Point(1073, 83);
            btnSlFilterApply.Name = "btnSlFilterApply";
            btnSlFilterApply.Size = new Size(113, 29);
            btnSlFilterApply.TabIndex = 2;
            btnSlFilterApply.Text = "Show Only";
            btnSlFilterApply.UseVisualStyleBackColor = false;
            btnSlFilterApply.Click += btnSlFilterApply_Click;
            // 
            // panel11
            // 
            panel11.BackColor = SystemColors.Control;
            panel11.Dock = DockStyle.Right;
            panel11.Location = new Point(1300, 3);
            panel11.Name = "panel11";
            panel11.Size = new Size(38, 694);
            panel11.TabIndex = 13;
            // 
            // panel5
            // 
            panel5.BackColor = SystemColors.Control;
            panel5.Dock = DockStyle.Left;
            panel5.Location = new Point(3, 3);
            panel5.Name = "panel5";
            panel5.Size = new Size(82, 694);
            panel5.TabIndex = 12;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(MainInvPanel);
            tabPage2.ImageKey = "Inventory.png";
            tabPage2.Location = new Point(4, 34);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1341, 700);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Inventory";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // MainInvPanel
            // 
            MainInvPanel.Controls.Add(panel19);
            MainInvPanel.Controls.Add(panel4);
            MainInvPanel.Controls.Add(panel3);
            MainInvPanel.Controls.Add(panel2);
            MainInvPanel.Controls.Add(panel1);
            MainInvPanel.Dock = DockStyle.Fill;
            MainInvPanel.Location = new Point(3, 3);
            MainInvPanel.Name = "MainInvPanel";
            MainInvPanel.Size = new Size(1335, 694);
            MainInvPanel.TabIndex = 0;
            // 
            // panel19
            // 
            panel19.BorderStyle = BorderStyle.Fixed3D;
            panel19.Controls.Add(materialCard5);
            panel19.Controls.Add(btnInvEditItem);
            panel19.Controls.Add(btnInvDeleteItem);
            panel19.Dock = DockStyle.Left;
            panel19.Location = new Point(82, 125);
            panel19.Name = "panel19";
            panel19.Size = new Size(242, 569);
            panel19.TabIndex = 17;
            // 
            // materialCard5
            // 
            materialCard5.BackColor = Color.FromArgb(255, 255, 255);
            materialCard5.BorderStyle = BorderStyle.Fixed3D;
            materialCard5.Controls.Add(label5);
            materialCard5.Controls.Add(txtInvItemStocks);
            materialCard5.Controls.Add(label4);
            materialCard5.Controls.Add(cmbInvCategory);
            materialCard5.Controls.Add(label3);
            materialCard5.Controls.Add(txtInvItemPrice);
            materialCard5.Controls.Add(label1);
            materialCard5.Controls.Add(txtInvItemName);
            materialCard5.Controls.Add(btnInvAddItem);
            materialCard5.Depth = 0;
            materialCard5.Dock = DockStyle.Top;
            materialCard5.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard5.Location = new Point(0, 0);
            materialCard5.Margin = new Padding(14);
            materialCard5.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard5.Name = "materialCard5";
            materialCard5.Padding = new Padding(14);
            materialCard5.Size = new Size(238, 385);
            materialCard5.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 219);
            label5.Name = "label5";
            label5.Size = new Size(85, 20);
            label5.TabIndex = 8;
            label5.Text = "Item Stocks";
            // 
            // txtInvItemStocks
            // 
            txtInvItemStocks.Location = new Point(17, 242);
            txtInvItemStocks.Name = "txtInvItemStocks";
            txtInvItemStocks.Size = new Size(208, 27);
            txtInvItemStocks.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 87);
            label4.Name = "label4";
            label4.Size = new Size(69, 20);
            label4.TabIndex = 6;
            label4.Text = "Category";
            // 
            // cmbInvCategory
            // 
            cmbInvCategory.FormattingEnabled = true;
            cmbInvCategory.Items.AddRange(new object[] { "Rice", "Cooking Essentials", "Drinks", "Snacks/Foods", "Cleaning Essentials", "Packaging & Disposables" });
            cmbInvCategory.Location = new Point(17, 110);
            cmbInvCategory.Name = "cmbInvCategory";
            cmbInvCategory.Size = new Size(208, 28);
            cmbInvCategory.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(17, 151);
            label3.Name = "label3";
            label3.Size = new Size(75, 20);
            label3.TabIndex = 4;
            label3.Text = "Item Price";
            // 
            // txtInvItemPrice
            // 
            txtInvItemPrice.Location = new Point(17, 174);
            txtInvItemPrice.Name = "txtInvItemPrice";
            txtInvItemPrice.Size = new Size(208, 27);
            txtInvItemPrice.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 23);
            label1.Name = "label1";
            label1.Size = new Size(83, 20);
            label1.TabIndex = 2;
            label1.Text = "Item Name";
            // 
            // txtInvItemName
            // 
            txtInvItemName.Location = new Point(17, 46);
            txtInvItemName.Name = "txtInvItemName";
            txtInvItemName.Size = new Size(208, 27);
            txtInvItemName.TabIndex = 0;
            // 
            // btnInvAddItem
            // 
            btnInvAddItem.Location = new Point(15, 335);
            btnInvAddItem.Name = "btnInvAddItem";
            btnInvAddItem.Size = new Size(94, 29);
            btnInvAddItem.TabIndex = 1;
            btnInvAddItem.Text = "Add +";
            btnInvAddItem.UseVisualStyleBackColor = true;
            btnInvAddItem.Click += btnInvAddItem_Click;
            // 
            // btnInvEditItem
            // 
            btnInvEditItem.Location = new Point(17, 453);
            btnInvEditItem.Name = "btnInvEditItem";
            btnInvEditItem.Size = new Size(208, 29);
            btnInvEditItem.TabIndex = 4;
            btnInvEditItem.Text = "Edit Item";
            btnInvEditItem.UseVisualStyleBackColor = true;
            btnInvEditItem.Click += btnInvEditItem_Click;
            // 
            // btnInvDeleteItem
            // 
            btnInvDeleteItem.Location = new Point(17, 418);
            btnInvDeleteItem.Name = "btnInvDeleteItem";
            btnInvDeleteItem.Size = new Size(208, 29);
            btnInvDeleteItem.TabIndex = 3;
            btnInvDeleteItem.Text = "Delete Item ";
            btnInvDeleteItem.UseVisualStyleBackColor = true;
            btnInvDeleteItem.Click += btnInvDeleteItem_Click;
            // 
            // panel4
            // 
            panel4.Controls.Add(dgvInventory);
            panel4.Dock = DockStyle.Right;
            panel4.Location = new Point(330, 125);
            panel4.Name = "panel4";
            panel4.Size = new Size(967, 569);
            panel4.TabIndex = 14;
            // 
            // dgvInventory
            // 
            dgvInventory.AllowUserToOrderColumns = true;
            dgvInventory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInventory.BackgroundColor = SystemColors.ButtonFace;
            dgvInventory.BorderStyle = BorderStyle.Fixed3D;
            dgvInventory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInventory.Columns.AddRange(new DataGridViewColumn[] { colInvName, colInvCategory, colInvPrice, colInvStocks });
            dgvInventory.Dock = DockStyle.Fill;
            dgvInventory.GridColor = SystemColors.InactiveCaptionText;
            dgvInventory.Location = new Point(0, 0);
            dgvInventory.Name = "dgvInventory";
            dgvInventory.RowHeadersWidth = 51;
            dgvInventory.Size = new Size(967, 569);
            dgvInventory.TabIndex = 10;
            dgvInventory.CellContentClick += dgvInventory_CellContentClick;
            // 
            // colInvName
            // 
            dataGridViewCellStyle2.Format = "₱ #,##0.00";
            dataGridViewCellStyle2.NullValue = null;
            colInvName.DefaultCellStyle = dataGridViewCellStyle2;
            colInvName.HeaderText = "Item Name ";
            colInvName.MinimumWidth = 6;
            colInvName.Name = "colInvName";
            // 
            // colInvCategory
            // 
            colInvCategory.HeaderText = "Category";
            colInvCategory.MinimumWidth = 6;
            colInvCategory.Name = "colInvCategory";
            colInvCategory.Resizable = DataGridViewTriState.True;
            // 
            // colInvPrice
            // 
            colInvPrice.HeaderText = "Prices (₱)";
            colInvPrice.MinimumWidth = 6;
            colInvPrice.Name = "colInvPrice";
            // 
            // colInvStocks
            // 
            colInvStocks.HeaderText = "Stocks";
            colInvStocks.MinimumWidth = 6;
            colInvStocks.Name = "colInvStocks";
            // 
            // panel3
            // 
            panel3.Controls.Add(materialCard1);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(82, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(1215, 125);
            panel3.TabIndex = 13;
            // 
            // materialCard1
            // 
            materialCard1.BackColor = Color.FromArgb(255, 255, 255);
            materialCard1.BorderStyle = BorderStyle.Fixed3D;
            materialCard1.Controls.Add(labelInv);
            materialCard1.Controls.Add(btnInvSearch);
            materialCard1.Controls.Add(txtInvSearch);
            materialCard1.Controls.Add(cmbInvFilterCategory);
            materialCard1.Controls.Add(btnInvFilterApply);
            materialCard1.Depth = 0;
            materialCard1.Dock = DockStyle.Fill;
            materialCard1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard1.Location = new Point(0, 0);
            materialCard1.Margin = new Padding(14);
            materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard1.Name = "materialCard1";
            materialCard1.Padding = new Padding(14);
            materialCard1.Size = new Size(1215, 125);
            materialCard1.TabIndex = 9;
            // 
            // labelInv
            // 
            labelInv.AutoSize = true;
            labelInv.Font = new Font("Segoe UI", 24F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            labelInv.ForeColor = Color.Goldenrod;
            labelInv.Location = new Point(17, 12);
            labelInv.Name = "labelInv";
            labelInv.Size = new Size(208, 54);
            labelInv.TabIndex = 14;
            labelInv.Text = "Inventory";
            // 
            // btnInvSearch
            // 
            btnInvSearch.BackColor = SystemColors.ButtonHighlight;
            btnInvSearch.Location = new Point(1073, 37);
            btnInvSearch.Name = "btnInvSearch";
            btnInvSearch.Size = new Size(113, 29);
            btnInvSearch.TabIndex = 9;
            btnInvSearch.Text = "Search";
            btnInvSearch.UseVisualStyleBackColor = false;
            btnInvSearch.Click += btnInvSearch_Click;
            // 
            // txtInvSearch
            // 
            txtInvSearch.Location = new Point(782, 39);
            txtInvSearch.Name = "txtInvSearch";
            txtInvSearch.Size = new Size(285, 27);
            txtInvSearch.TabIndex = 8;
            txtInvSearch.Text = "Enter item name...";
            // 
            // cmbInvFilterCategory
            // 
            cmbInvFilterCategory.FormattingEnabled = true;
            cmbInvFilterCategory.Items.AddRange(new object[] { "All", "Rice", "Cooking Essentials", "Drinks", "Snacks/Foods", "Cleaning Essentials", "Packaging & Disposables" });
            cmbInvFilterCategory.Location = new Point(782, 84);
            cmbInvFilterCategory.Name = "cmbInvFilterCategory";
            cmbInvFilterCategory.Size = new Size(285, 28);
            cmbInvFilterCategory.TabIndex = 4;
            cmbInvFilterCategory.Text = "Categories";
            // 
            // btnInvFilterApply
            // 
            btnInvFilterApply.BackColor = SystemColors.ButtonFace;
            btnInvFilterApply.Location = new Point(1073, 83);
            btnInvFilterApply.Name = "btnInvFilterApply";
            btnInvFilterApply.Size = new Size(113, 29);
            btnInvFilterApply.TabIndex = 2;
            btnInvFilterApply.Text = "Show Only";
            btnInvFilterApply.UseVisualStyleBackColor = false;
            btnInvFilterApply.Click += btnInvFilterApply_Click;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Control;
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(1297, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(38, 694);
            panel2.TabIndex = 12;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Control;
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(82, 694);
            panel1.TabIndex = 11;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(panel15);
            tabPage1.Controls.Add(panel12);
            tabPage1.Controls.Add(panel8);
            tabPage1.ImageKey = "home.png";
            tabPage1.Location = new Point(4, 34);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1341, 700);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Home";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel15
            // 
            panel15.Dock = DockStyle.Top;
            panel15.Location = new Point(85, 3);
            panel15.Name = "panel15";
            panel15.Size = new Size(1215, 121);
            panel15.TabIndex = 14;
            // 
            // panel12
            // 
            panel12.BackColor = Color.Black;
            panel12.Dock = DockStyle.Right;
            panel12.Location = new Point(1300, 3);
            panel12.Name = "panel12";
            panel12.Size = new Size(38, 694);
            panel12.TabIndex = 13;
            // 
            // panel8
            // 
            panel8.BackColor = Color.Black;
            panel8.Dock = DockStyle.Left;
            panel8.Location = new Point(3, 3);
            panel8.Name = "panel8";
            panel8.Size = new Size(82, 694);
            panel8.TabIndex = 12;
            // 
            // materialTabControl1
            // 
            materialTabControl1.Controls.Add(tabPage1);
            materialTabControl1.Controls.Add(tabPage2);
            materialTabControl1.Controls.Add(tabPage3);
            materialTabControl1.Controls.Add(tabPage4);
            materialTabControl1.Controls.Add(tabPage5);
            materialTabControl1.Depth = 0;
            materialTabControl1.ImageList = imageList1;
            materialTabControl1.ItemSize = new Size(84, 30);
            materialTabControl1.Location = new Point(6, 67);
            materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            materialTabControl1.Multiline = true;
            materialTabControl1.Name = "materialTabControl1";
            materialTabControl1.SelectedIndex = 0;
            materialTabControl1.Size = new Size(1349, 738);
            materialTabControl1.TabIndex = 0;
            // 
            // colTabName
            // 
            colTabName.FillWeight = 94.23874F;
            colTabName.HeaderText = "Costumer Name";
            colTabName.MinimumWidth = 6;
            colTabName.Name = "colTabName";
            // 
            // colTabBalance
            // 
            colTabBalance.FillWeight = 101.558464F;
            colTabBalance.HeaderText = "Active Balance";
            colTabBalance.MinimumWidth = 6;
            colTabBalance.Name = "colTabBalance";
            // 
            // colTabStatus
            // 
            colTabStatus.FillWeight = 104.423553F;
            colTabStatus.HeaderText = "Debt Status";
            colTabStatus.MinimumWidth = 6;
            colTabStatus.Name = "colTabStatus";
            // 
            // colTabDate
            // 
            colTabDate.FillWeight = 99.77925F;
            colTabDate.HeaderText = "Debt Date and Time Started ";
            colTabDate.MinimumWidth = 6;
            colTabDate.Name = "colTabDate";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1361, 810);
            Controls.Add(materialTabControl1);
            DrawerShowIconsWhenHidden = true;
            DrawerTabControl = materialTabControl1;
            DrawerUseColors = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Padding = new Padding(60, 20, 20, 20);
            StartPosition = FormStartPosition.CenterScreen;
            Text = " ";
            tabPage5.ResumeLayout(false);
            tabPage4.ResumeLayout(false);
            panel17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTabLedger).EndInit();
            panel16.ResumeLayout(false);
            materialCard3.ResumeLayout(false);
            materialCard3.PerformLayout();
            panel18.ResumeLayout(false);
            panel18.PerformLayout();
            materialCard4.ResumeLayout(false);
            materialCard4.PerformLayout();
            tabPage3.ResumeLayout(false);
            panel20.ResumeLayout(false);
            materialCard6.ResumeLayout(false);
            materialCard6.PerformLayout();
            panel14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSalesLedger).EndInit();
            panel13.ResumeLayout(false);
            dtpSlFilterDate.ResumeLayout(false);
            dtpSlFilterDate.PerformLayout();
            tabPage2.ResumeLayout(false);
            MainInvPanel.ResumeLayout(false);
            panel19.ResumeLayout(false);
            materialCard5.ResumeLayout(false);
            materialCard5.PerformLayout();
            panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvInventory).EndInit();
            panel3.ResumeLayout(false);
            materialCard1.ResumeLayout(false);
            materialCard1.PerformLayout();
            tabPage1.ResumeLayout(false);
            materialTabControl1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer SideBarTimer;
        private ImageList imageList1;
        private TabPage tabPage5;
        private TabPage tabPage4;
        private TabPage tabPage3;
        private TabPage tabPage2;
        private Panel MainInvPanel;
        private Panel panel2;
        private MaterialSkin.Controls.MaterialCard materialCard1;
        private Button btnInvSearch;
        private TextBox txtInvSearch;
        private ComboBox cmbInvFilterCategory;
        private Button btnInvFilterApply;
        private Panel panel1;
        private TabPage tabPage1;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private Panel panel3;
        private Panel panel4;
        private Panel panel9;
        private Panel panel7;
        private Panel panel10;
        private Panel panel6;
        private Panel panel11;
        private Panel panel5;
        private Panel panel12;
        private Panel panel8;
        private DataGridView dgvInventory;
        private Panel panel13;
        private Panel panel14;
        private DataGridView dgvSalesLedger;
        private MaterialSkin.Controls.MaterialCard dtpSlFilterDate;
        private Button btnSlSearch;
        private TextBox txtSlSearch;
        private ComboBox cmbSlFilterCategory;
        private Button btnSlFilterApply;
        private DateTimePicker dateTimePicker1;
        private Button btnSlShowDate;
        private Label labelSL;
        private Label labelInv;
        private Panel panel15;
        private Panel panel17;
        private Panel panel16;
        private Panel panel18;
        private DataGridView dgvTabLedger;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private TextBox txtTabCustomerName;
        private ComboBox cmbTabFilterStatus;
        private TextBox txtTabSearchName;
        private Label label2;
        private Button btnTabAddCustomer;
        private Button btnTabSearch;
        private MaterialSkin.Controls.MaterialCard materialCard3;
        private Button btnTabEditCustomer;
        private Button btnTabDeleteCustomer;
        private Button BtnVCDL;
        private MaterialSkin.Controls.MaterialCard materialCard4;
        private Label labelTL;
        private Panel panel19;
        private MaterialSkin.Controls.MaterialCard materialCard5;
        private Label label1;
        private TextBox txtInvItemName;
        private Button btnInvAddItem;
        private Button btnInvEditItem;
        private Button btnInvDeleteItem;
        private Label label3;
        private TextBox txtInvItemPrice;
        private Label label5;
        private TextBox txtInvItemStocks;
        private Label label4;
        private ComboBox cmbInvCategory;
        private Panel panel20;
        private MaterialSkin.Controls.MaterialCard materialCard6;
        private Label label7;
        private ComboBox cmbSlCategory;
        private Label label8;
        private TextBox txtSlItemPrice;
        private Label label9;
        private TextBox txtSlItemName;
        private Button btnSlAddItem;
        private Button btnSlEditItem;
        private Button btnSlDeleteItem;
        private DataGridViewTextBoxColumn colInvName;
        private DataGridViewTextBoxColumn colInvCategory;
        private DataGridViewTextBoxColumn colInvPrice;
        private DataGridViewTextBoxColumn colInvStocks;
        private DataGridViewTextBoxColumn colSLName;
        private DataGridViewTextBoxColumn colSLItemsold;
        private DataGridViewTextBoxColumn colSLCategory;
        private DataGridViewTextBoxColumn colSlPrice;
        private DataGridViewTextBoxColumn colSLDateTime;
        private DataGridViewTextBoxColumn colTabName;
        private DataGridViewTextBoxColumn colTabBalance;
        private DataGridViewTextBoxColumn colTabStatus;
        private DataGridViewTextBoxColumn colTabDate;
    }
}
