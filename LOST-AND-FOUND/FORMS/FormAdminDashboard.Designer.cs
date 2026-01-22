using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace LOST_AND_FOUND
{
    partial class FormAdminDashboard
    {
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private Panel panelHeader;
        private Panel panelNav;
        private Label lblTitle;
        private PictureBox pbBackground;

        private Guna2Button btnAddStudent;
        private Guna2Button btnViewLost;
        private Guna2Button btnViewFound;
        private Guna2Button btnMatch;
        private Guna2Button btnLogout;

        private Label lblLostCount;
        private Label lblFoundCount;
        private Label lblMatchedCount;

        private Guna2Panel pnlAddStudent;
        private Guna2TextBox txtUsername;
        private Guna2TextBox txtPassword;
        private Guna2TextBox txtStudentID;
        private Guna2Button btnSubmitStudent;
        private Guna2Button btnCancelStudent;
        private Label lblAddStudentTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // Colors
            Color MidnightBlue = ColorTranslator.FromHtml("#0F2233");
            Color DeepBlue = ColorTranslator.FromHtml("#1F3C58");
            Color GoldAccent = ColorTranslator.FromHtml("#C5A059");
            Color SapphireHover = ColorTranslator.FromHtml("#3A5F7A");
            Color SoftIvory = ColorTranslator.FromHtml("#F5F3E9");

            // Form settings - Full screen
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = MidnightBlue;

            // Background Image
            pbBackground = new PictureBox();
            pbBackground.Image = Properties.Resources.LF;
            pbBackground.Dock = DockStyle.Fill;
            pbBackground.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(pbBackground);
            pbBackground.SendToBack();

            // Header Panel
            panelHeader = new Panel();
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 80;
            panelHeader.BackColor = DeepBlue;
            pbBackground.Controls.Add(panelHeader);

            lblTitle = new Label();
            lblTitle.Text = "ADMIN DASHBOARD";
            lblTitle.Font = new Font("Segoe UI Semibold", 22F);
            lblTitle.ForeColor = GoldAccent;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Dock = DockStyle.Fill;
            panelHeader.Controls.Add(lblTitle);

            // Left Navbar Panel
            panelNav = new Panel();
            panelNav.Width = 200;
            panelNav.Dock = DockStyle.Left;
            panelNav.BackColor = DeepBlue;
            pbBackground.Controls.Add(panelNav);

            // Buttons aligned vertically in navbar
            btnAddStudent = CreateButton("Add Student", 10, 100, GoldAccent, SapphireHover);
            btnViewLost = CreateButton("View Lost Items", 10, 170, GoldAccent, SapphireHover);
            btnViewFound = CreateButton("View Found Items", 10, 240, GoldAccent, SapphireHover);
            btnMatch = CreateButton("Manual Match", 10, 310, GoldAccent, SapphireHover);
            btnLogout = CreateButton("Logout", 10, 380, Color.IndianRed, Color.Red);

            panelNav.Controls.AddRange(new Control[] { btnAddStudent, btnViewLost, btnViewFound, btnMatch, btnLogout });

            // Stats Labels - descriptive text + counts on the right
            lblLostCount = CreateStatLabel(0, 150, SoftIvory);
            lblFoundCount = CreateStatLabel(0, 220, SoftIvory);
            lblMatchedCount = CreateStatLabel(0, 290, SoftIvory);

            // Descriptive Labels
            Label lblLostText = new Label();
            lblLostText.Text = "Lost Items:";
            lblLostText.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblLostText.ForeColor = SoftIvory;
            lblLostText.Size = new Size(180, 40);
            lblLostText.TextAlign = ContentAlignment.MiddleRight;
            lblLostText.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            Label lblFoundText = new Label();
            lblFoundText.Text = "Found Items:";
            lblFoundText.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblFoundText.ForeColor = SoftIvory;
            lblFoundText.Size = new Size(180, 40);
            lblFoundText.TextAlign = ContentAlignment.MiddleRight;
            lblFoundText.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            Label lblMatchedText = new Label();
            lblMatchedText.Text = "Matched Items:";
            lblMatchedText.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblMatchedText.ForeColor = SoftIvory;
            lblMatchedText.Size = new Size(180, 40);
            lblMatchedText.TextAlign = ContentAlignment.MiddleRight;
            lblMatchedText.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // Position dynamically based on form width
            lblLostText.Location = new Point(this.ClientSize.Width - 420, 150);
            lblFoundText.Location = new Point(this.ClientSize.Width - 420, 220);
            lblMatchedText.Location = new Point(this.ClientSize.Width - 420, 290);

            lblLostCount.Location = new Point(this.ClientSize.Width - 220, 150);
            lblFoundCount.Location = new Point(this.ClientSize.Width - 220, 220);
            lblMatchedCount.Location = new Point(this.ClientSize.Width - 220, 290);

            pbBackground.Controls.AddRange(new Control[] { lblLostText, lblFoundText, lblMatchedText, lblLostCount, lblFoundCount, lblMatchedCount });
            lblLostText.BringToFront();
            lblFoundText.BringToFront();
            lblMatchedText.BringToFront();
            lblLostCount.BringToFront();
            lblFoundCount.BringToFront();
            lblMatchedCount.BringToFront();

            // Add Student Panel
            pnlAddStudent = new Guna2Panel();
            pnlAddStudent.Size = new Size(450, 320);
            pnlAddStudent.Location = new Point(220 + 50, 120); // right of navbar
            pnlAddStudent.FillColor = DeepBlue;
            pnlAddStudent.BorderRadius = 12;
            pnlAddStudent.Visible = false;
            pbBackground.Controls.Add(pnlAddStudent);

            lblAddStudentTitle = new Label();
            lblAddStudentTitle.Text = "Register New Student";
            lblAddStudentTitle.Font = new Font("Segoe UI Semibold", 16F);
            lblAddStudentTitle.ForeColor = GoldAccent;
            lblAddStudentTitle.Location = new Point(90, 20);
            lblAddStudentTitle.Size = new Size(300, 40);
            pnlAddStudent.Controls.Add(lblAddStudentTitle);

            txtUsername = CreateTextBox("Username", 50, 80);
            txtPassword = CreateTextBox("Password", 50, 140, true);
            txtStudentID = CreateTextBox("Student ID", 50, 200);
            pnlAddStudent.Controls.AddRange(new Control[] { txtUsername, txtPassword, txtStudentID });

            btnSubmitStudent = CreateSmallButton("Create Student", 50, 260, GoldAccent, SapphireHover);
            btnCancelStudent = CreateSmallButton("Cancel", 240, 260, Color.IndianRed, Color.Red);
            pnlAddStudent.Controls.AddRange(new Control[] { btnSubmitStudent, btnCancelStudent });
        }

        // Helper methods
        private Guna2Button CreateButton(string text, int x, int y, Color fill, Color hover)
        {
            var btn = new Guna2Button();
            btn.Text = text;
            btn.Location = new Point(x, y);
            btn.Size = new Size(180, 50);
            btn.FillColor = fill;
            btn.ForeColor = Color.White;
            btn.HoverState.FillColor = hover;
            btn.BorderRadius = 8;
            return btn;
        }

        private Guna2Button CreateSmallButton(string text, int x, int y, Color fill, Color hover)
        {
            var btn = new Guna2Button();
            btn.Text = text;
            btn.Location = new Point(x, y);
            btn.Size = new Size(160, 40);
            btn.FillColor = fill;
            btn.ForeColor = Color.White;
            btn.HoverState.FillColor = hover;
            btn.BorderRadius = 6;
            return btn;
        }

        private Guna2TextBox CreateTextBox(string placeholder, int x, int y, bool isPassword = false)
        {
            var txt = new Guna2TextBox();
            txt.PlaceholderText = placeholder;
            txt.Location = new Point(x, y);
            txt.Size = new Size(350, 35);
            txt.BorderRadius = 6;
            txt.FillColor = Color.White;
            txt.PasswordChar = isPassword ? '●' : '\0';
            return txt;
        }

        private Label CreateStatLabel(int x, int y, Color foreColor)
        {
            var lbl = new Label();
            lbl.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lbl.ForeColor = foreColor;
            lbl.Size = new Size(200, 40);
            lbl.Text = "0";
            lbl.TextAlign = ContentAlignment.MiddleRight;
            lbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            return lbl;
        }
    }
}
