using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace LOST_AND_FOUND
{
    partial class FormStudentDashboard
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelHeader;
        private Panel panelNav;
        private Label lblTitle;
        private PictureBox pbBackground;

        private Guna2Button btnAddLost;
        private Guna2Button btnAddFound;
        private Guna2Button btnViewMatches;
        private Guna2Button btnLogout;

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

            // Form settings
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = MidnightBlue;

            // Background
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
            lblTitle.Text = "STUDENT DASHBOARD";
            lblTitle.Font = new Font("Segoe UI Semibold", 22F);
            lblTitle.ForeColor = GoldAccent;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Dock = DockStyle.Fill;
            panelHeader.Controls.Add(lblTitle);

            // Navbar Panel
            panelNav = new Panel();
            panelNav.Width = 200;
            panelNav.Dock = DockStyle.Left;
            panelNav.BackColor = DeepBlue;
            pbBackground.Controls.Add(panelNav);

            // Buttons
            btnAddLost = CreateNavButton("Add Lost Item", 10, 100, GoldAccent, SapphireHover);
            btnAddFound = CreateNavButton("Add Found Item", 10, 170, GoldAccent, SapphireHover);
            btnViewMatches = CreateNavButton("View My Matches", 10, 240, GoldAccent, SapphireHover);
            btnLogout = CreateNavButton("Logout", 10, 310, Color.IndianRed, Color.Red);

            panelNav.Controls.AddRange(new Control[] { btnAddLost, btnAddFound, btnViewMatches, btnLogout });

            // Attach event handlers
            btnAddLost.Click += new EventHandler(BtnAddLost_Click);
            btnAddFound.Click += new EventHandler(BtnAddFound_Click);
            btnViewMatches.Click += new EventHandler(BtnViewMatches_Click);
            btnLogout.Click += new EventHandler(BtnLogout_Click);
        }

        private Guna2Button CreateNavButton(string text, int x, int y, Color fill, Color hover)
        {
            var btn = new Guna2Button();
            btn.Text = text;
            btn.Location = new Point(x, y);
            btn.Size = new Size(180, 50);
            btn.FillColor = fill;
            btn.ForeColor = Color.White;
            btn.HoverState.FillColor = hover;
            btn.BorderRadius = 8;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            return btn;
        }
    }
}
