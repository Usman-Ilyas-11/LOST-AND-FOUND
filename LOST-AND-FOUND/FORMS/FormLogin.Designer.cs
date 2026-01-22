using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace LOST_AND_FOUND
{
    partial class FormLogin
    {
        private Label lblTitle;
        private Guna2TextBox txtUser;
        private Guna2TextBox txtPass;
        private Guna2Button btnLogin;
        private Panel panelHeader;

        private void InitializeComponent()
        {
            // Palette Colors
            Color MidnightBlue = ColorTranslator.FromHtml("#0F2233");   // Background
            Color DeepBlue = ColorTranslator.FromHtml("#1F3C58");       // Secondary Panel
            Color GoldAccent = ColorTranslator.FromHtml("#C5A059");     // Buttons / Accent
            Color SapphireHover = ColorTranslator.FromHtml("#3A5F7A");  // Hover Highlight
            Color SoftIvory = ColorTranslator.FromHtml("#F5F3E9");      // Text

            // Form settings
            this.ClientSize = new Size(420, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Text = "Login - Lost & Found";
            this.BackColor = MidnightBlue;

            // Header Panel
            panelHeader = new Panel();
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 85;
            panelHeader.BackColor = DeepBlue;
            this.Controls.Add(panelHeader);

            // Title Label
            lblTitle = new Label();
            lblTitle.Text = "LOST & FOUND LOGIN";
            lblTitle.Font = new Font("Segoe UI Semibold", 18F);
            lblTitle.ForeColor = GoldAccent;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Dock = DockStyle.Fill;
            panelHeader.Controls.Add(lblTitle);

            // Username TextBox
            txtUser = new Guna2TextBox();
            txtUser.PlaceholderText = "Enter username";
            txtUser.Font = new Font("Segoe UI", 10F);
            txtUser.FillColor = DeepBlue;
            txtUser.ForeColor = SoftIvory;
            txtUser.BorderColor = GoldAccent;
            txtUser.Width = 280;
            txtUser.Location = new Point(70, 120);
            txtUser.BorderRadius = 6;
            txtUser.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            this.Controls.Add(txtUser);

            // Password TextBox
            txtPass = new Guna2TextBox();
            txtPass.PlaceholderText = "Enter password";
            txtPass.PasswordChar = '●';
            txtPass.Font = new Font("Segoe UI", 10F);
            txtPass.FillColor = DeepBlue;
            txtPass.ForeColor = SoftIvory;
            txtPass.BorderColor = GoldAccent;
            txtPass.Width = 280;
            txtPass.Location = new Point(70, 175);
            txtPass.BorderRadius = 6;
            txtPass.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            this.Controls.Add(txtPass);

            // Login Button
            btnLogin = new Guna2Button();
            btnLogin.Text = "LOGIN";
            btnLogin.Font = new Font("Segoe UI Semibold", 11F);
            btnLogin.Width = 150;
            btnLogin.Height = 42;
            btnLogin.Location = new Point(135, 230);

            btnLogin.FillColor = GoldAccent;
            btnLogin.HoverState.FillColor = SapphireHover;
            btnLogin.PressedColor = DeepBlue;
            btnLogin.ForeColor = SoftIvory;
            btnLogin.BorderRadius = 8;
            btnLogin.BorderColor = GoldAccent;
            btnLogin.BorderThickness = 1;

            btnLogin.Click += new EventHandler(this.BtnLogin_Click);
            this.Controls.Add(btnLogin);
        }
    }
}
