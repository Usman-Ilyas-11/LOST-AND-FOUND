using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace LOST_AND_FOUND
{
    partial class FormViewMatched
    {
        private Guna2Panel pnlLost;
        private Guna2Panel pnlFound;
        private Guna2Panel pnlMatched;

        private ListView lvLost;
        private ListView lvFound;
        private ListView lvMatched;

        private Guna2Button BtnApproveMatch;
        private Guna2Button BtnReturnItem;

        private void InitializeComponent()
        {
            // Color Palette
            Color LightBlueNavy = ColorTranslator.FromHtml("#2A3B5D");
            Color DeepBlue = ColorTranslator.FromHtml("#1F3C58");
            Color GoldAccent = ColorTranslator.FromHtml("#C5A059");
            Color SoftIvory = ColorTranslator.FromHtml("#F5F3E9");
            Color SapphireHover = ColorTranslator.FromHtml("#3A5F7A");

            // ==================== FORM SETTINGS ====================
            this.ClientSize = new Size(1050, 700); // increased width for buttons
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Matched Items Management";
            this.BackColor = LightBlueNavy;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // ==================== LOST ITEMS PANEL ====================
            pnlLost = new Guna2Panel
            {
                Size = new Size(400, 250),
                Location = new Point(10, 10),
                FillColor = SoftIvory,
                BorderRadius = 12,
                ShadowDecoration = { Enabled = true }
            };

            lvLost = new ListView
            {
                View = View.Details,
                Dock = DockStyle.Fill,
                FullRowSelect = true,
                GridLines = true,
                BackColor = Color.WhiteSmoke,
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 10)
            };
            lvLost.Columns.Add("ID", 50);
            lvLost.Columns.Add("Item Name", 120);
            lvLost.Columns.Add("Description", 200);

            pnlLost.Controls.Add(lvLost);
            this.Controls.Add(pnlLost);

            // ==================== FOUND ITEMS PANEL ====================
            pnlFound = new Guna2Panel
            {
                Size = new Size(400, 250),
                Location = new Point(420, 10),
                FillColor = SoftIvory,
                BorderRadius = 12,
                ShadowDecoration = { Enabled = true }
            };

            lvFound = new ListView
            {
                View = View.Details,
                Dock = DockStyle.Fill,
                FullRowSelect = true,
                GridLines = true,
                BackColor = Color.WhiteSmoke,
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 10)
            };
            lvFound.Columns.Add("ID", 50);
            lvFound.Columns.Add("Item Name", 120);
            lvFound.Columns.Add("Description", 200);

            pnlFound.Controls.Add(lvFound);
            this.Controls.Add(pnlFound);

            // ==================== MATCHED ITEMS PANEL ====================
            pnlMatched = new Guna2Panel
            {
                Size = new Size(830, 300),
                Location = new Point(10, 270),
                FillColor = SoftIvory,
                BorderRadius = 12,
                ShadowDecoration = { Enabled = true }
            };

            lvMatched = new ListView
            {
                View = View.Details,
                Dock = DockStyle.Fill,
                FullRowSelect = true,
                GridLines = true,
                BackColor = Color.WhiteSmoke,
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 10)
            };
            lvMatched.Columns.Add("ID", 50);
            lvMatched.Columns.Add("Item Name", 120);
            lvMatched.Columns.Add("Description", 150);
            lvMatched.Columns.Add("Matched Date", 120);
            lvMatched.Columns.Add("Admin", 80);
            lvMatched.Columns.Add("Returned", 80);

            pnlMatched.Controls.Add(lvMatched);
            this.Controls.Add(pnlMatched);

            // ==================== BUTTONS ====================
            BtnApproveMatch = new Guna2Button
            {
                Text = "Approve Match",
                Location = new Point(880, 280), // right side
                Size = new Size(140, 50),
                FillColor = GoldAccent,
                ForeColor = Color.White,
                BorderRadius = 15,
                HoverState = { FillColor = SapphireHover }
            };
            BtnApproveMatch.Click += BtnApproveMatch_Click;
            this.Controls.Add(BtnApproveMatch);

            BtnReturnItem = new Guna2Button
            {
                Text = "Mark as Returned",
                Location = new Point(880, 350), // below Approve button
                Size = new Size(140, 50),       // same size
                FillColor = GoldAccent,         // same fill as Approve button
                ForeColor = Color.White,
                BorderRadius = 15,              // same rounded corners
                HoverState = { FillColor = SapphireHover } // same hover effect
            };
            BtnReturnItem.Click += BtnReturnItem_Click;
            this.Controls.Add(BtnReturnItem);

        }
    }
}
