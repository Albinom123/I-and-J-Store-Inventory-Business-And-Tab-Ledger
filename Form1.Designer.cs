namespace I_and_J_Store_Inventory__Business_And_Tab_Ledger
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel1 = new Panel();
            SideBarContainer = new FlowLayoutPanel();
            panel2 = new Panel();
            label1 = new Label();
            MenuButton = new PictureBox();
            panel3 = new Panel();
            button1 = new Button();
            panel4 = new Panel();
            button2 = new Button();
            panel5 = new Panel();
            button3 = new Button();
            panel6 = new Panel();
            button4 = new Button();
            panel7 = new Panel();
            button5 = new Button();
            SideBarTimer = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            SideBarContainer.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MenuButton).BeginInit();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
            panel7.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonHighlight;
            panel1.Controls.Add(SideBarContainer);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(810, 506);
            panel1.TabIndex = 0;
            // 
            // SideBarContainer
            // 
            SideBarContainer.BackColor = Color.DarkOrange;
            SideBarContainer.Controls.Add(panel2);
            SideBarContainer.Controls.Add(panel3);
            SideBarContainer.Controls.Add(panel4);
            SideBarContainer.Controls.Add(panel5);
            SideBarContainer.Controls.Add(panel6);
            SideBarContainer.Controls.Add(panel7);
            SideBarContainer.Dock = DockStyle.Left;
            SideBarContainer.Location = new Point(0, 0);
            SideBarContainer.MaximumSize = new Size(214, 506);
            SideBarContainer.MinimumSize = new Size(55, 506);
            SideBarContainer.Name = "SideBarContainer";
            SideBarContainer.Size = new Size(214, 506);
            SideBarContainer.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(label1);
            panel2.Controls.Add(MenuButton);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(207, 59);
            panel2.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Showcard Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(81, 18);
            label1.Name = "label1";
            label1.Size = new Size(59, 20);
            label1.TabIndex = 1;
            label1.Text = "Menu";
            // 
            // MenuButton
            // 
            MenuButton.Cursor = Cursors.Hand;
            MenuButton.Image = (Image)resources.GetObject("MenuButton.Image");
            MenuButton.Location = new Point(3, 9);
            MenuButton.Name = "MenuButton";
            MenuButton.Size = new Size(44, 38);
            MenuButton.SizeMode = PictureBoxSizeMode.StretchImage;
            MenuButton.TabIndex = 0;
            MenuButton.TabStop = false;
            MenuButton.Click += MenuButton_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(button1);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(3, 68);
            panel3.Name = "panel3";
            panel3.Size = new Size(210, 52);
            panel3.TabIndex = 1;
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Showcard Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = SystemColors.ActiveCaptionText;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Location = new Point(-11, -12);
            button1.Name = "button1";
            button1.Padding = new Padding(15, 0, 0, 0);
            button1.Size = new Size(235, 78);
            button1.TabIndex = 0;
            button1.Text = "Home";
            button1.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            panel4.Controls.Add(button2);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(3, 126);
            panel4.Name = "panel4";
            panel4.Size = new Size(210, 52);
            panel4.TabIndex = 2;
            // 
            // button2
            // 
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Showcard Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = SystemColors.ActiveCaptionText;
            button2.Image = (Image)resources.GetObject("button2.Image");
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.Location = new Point(-11, -12);
            button2.Name = "button2";
            button2.Padding = new Padding(15, 0, 0, 0);
            button2.Size = new Size(227, 78);
            button2.TabIndex = 0;
            button2.Text = "   Inventory   ";
            button2.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            panel5.Controls.Add(button3);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(3, 184);
            panel5.Name = "panel5";
            panel5.Size = new Size(210, 52);
            panel5.TabIndex = 3;
            // 
            // button3
            // 
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Showcard Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.ForeColor = SystemColors.ActiveCaptionText;
            button3.Image = (Image)resources.GetObject("button3.Image");
            button3.ImageAlign = ContentAlignment.MiddleLeft;
            button3.Location = new Point(-11, -12);
            button3.Name = "button3";
            button3.Padding = new Padding(15, 0, 0, 0);
            button3.Size = new Size(227, 78);
            button3.TabIndex = 0;
            button3.Text = "  Sales Ledger";
            button3.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            panel6.Controls.Add(button4);
            panel6.Dock = DockStyle.Top;
            panel6.Location = new Point(3, 242);
            panel6.Name = "panel6";
            panel6.Size = new Size(210, 52);
            panel6.TabIndex = 4;
            // 
            // button4
            // 
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("Showcard Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button4.ForeColor = SystemColors.ActiveCaptionText;
            button4.Image = (Image)resources.GetObject("button4.Image");
            button4.ImageAlign = ContentAlignment.MiddleLeft;
            button4.Location = new Point(-11, -12);
            button4.Name = "button4";
            button4.Padding = new Padding(15, 0, 0, 0);
            button4.Size = new Size(227, 78);
            button4.TabIndex = 0;
            button4.Text = "Tab Ledger";
            button4.UseVisualStyleBackColor = true;
            // 
            // panel7
            // 
            panel7.Controls.Add(button5);
            panel7.Dock = DockStyle.Top;
            panel7.Location = new Point(3, 300);
            panel7.Name = "panel7";
            panel7.Size = new Size(210, 52);
            panel7.TabIndex = 5;
            // 
            // button5
            // 
            button5.FlatStyle = FlatStyle.Flat;
            button5.Font = new Font("Showcard Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button5.ForeColor = SystemColors.ActiveCaptionText;
            button5.Image = (Image)resources.GetObject("button5.Image");
            button5.ImageAlign = ContentAlignment.MiddleLeft;
            button5.Location = new Point(-11, -12);
            button5.Name = "button5";
            button5.Padding = new Padding(15, 0, 0, 0);
            button5.Size = new Size(227, 78);
            button5.TabIndex = 0;
            button5.Text = "Settings";
            button5.UseVisualStyleBackColor = true;
            // 
            // SideBarTimer
            // 
            SideBarTimer.Interval = 5;
            SideBarTimer.Tick += SideBarTimer_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(810, 506);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            panel1.ResumeLayout(false);
            SideBarContainer.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)MenuButton).EndInit();
            panel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel7.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private FlowLayoutPanel SideBarContainer;
        private Panel panel3;
        private Button button1;
        private Panel panel2;
        private Panel panel4;
        private Button button2;
        private Panel panel5;
        private Button button3;
        private Panel panel6;
        private Button button4;
        private Panel panel7;
        private Button button5;
        private Label label1;
        private PictureBox MenuButton;
        private System.Windows.Forms.Timer SideBarTimer;
    }
}
