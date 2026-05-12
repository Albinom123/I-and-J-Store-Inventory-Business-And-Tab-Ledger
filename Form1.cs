using MaterialSkin;
using MaterialSkin.Controls;
using System.Drawing;
using System.Windows.Forms;

namespace I_and_J_Store_Inventory__Business_And_Tab_Ledger
{
    public partial class Form1 : MaterialForm
    {
        bool sidebarExpand = false;

        public Form1()
        {
            InitializeComponent();

            // Setup the MaterialSkinManager
            var msm = MaterialSkinManager.Instance;
            msm.AddFormToManage(this);
            msm.Theme = MaterialSkinManager.Themes.LIGHT;

            // Define your Gold ColorScheme
            msm.ColorScheme = new ColorScheme(
                Color.FromArgb(184, 134, 11), // Primary: Dark Goldenrod
                Color.FromArgb(153, 101, 21), // Primary Dark: Golden Brown
                Color.FromArgb(218, 165, 32), // Primary Light: Goldenrod
                Color.FromArgb(255, 215, 0),  // Accent: Pure Gold
                TextShade.WHITE               // White text for readability
            );

            // Force the rendering to be consistent
            labelSL.UseCompatibleTextRendering = true;

            // Re-assert the specific font and weight

            labelSL.Font = new Font("Montserrat", 30f, FontStyle.Bold | FontStyle.Underline);
            labelSL.ForeColor = Color.FromArgb(153, 101, 21);

            labelInv.Font = new Font("Montserrat", 30f, FontStyle.Bold | FontStyle.Underline);
            labelInv.ForeColor = Color.FromArgb(153, 101, 21);

            labelTL.Font = new Font("Montserrat", 30f, FontStyle.Bold | FontStyle.Underline);
            labelTL.ForeColor = Color.FromArgb(153, 101, 21);

        }

        private void loadForm(Form contentForm)
        {
            if (this.MainInvPanel.Controls.Count > 0)
                this.MainInvPanel.Controls.RemoveAt(0);

            contentForm.TopLevel = false;
            contentForm.FormBorderStyle = FormBorderStyle.None;
            contentForm.Dock = DockStyle.Fill;

            this.MainInvPanel.Controls.Add(contentForm);
            this.MainInvPanel.Tag = contentForm;
            contentForm.Show();

            // Re-apply colors if the controls are part of the main form being refreshed

        }

        private void BtnVCDL_Click(object sender, EventArgs e)
        {
            // 1. Create the floating "Ghost" frame
            FloatingHostForm ghostFrame = new FloatingHostForm();

            // 2. Create your debt list content
            UserControlDebtList debtContent = new UserControlDebtList();
            debtContent.Dock = DockStyle.Fill;

            // 3. Put the content inside the frame
            ghostFrame.Controls.Add(debtContent);

            // 4. Pop it up!
            // ShowDialog makes it float on top and stay there until closed
            ghostFrame.ShowDialog();
        }
    }
}