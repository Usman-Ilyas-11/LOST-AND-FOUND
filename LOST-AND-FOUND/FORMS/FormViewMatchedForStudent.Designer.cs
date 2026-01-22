using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace LOST_AND_FOUND
{
    partial class FormViewMatchedForStudent
    {
        private Guna2Panel pnlMatched;
        private ListView lvMatched;

        private void InitializeComponent()
        {
            // Colors
            Color LightBlueNavy = ColorTranslator.FromHtml("#2A3B5D");
            Color DeepBlue = ColorTranslator.FromHtml("#1F3C58");
            Color GoldAccent = ColorTranslator.FromHtml("#C5A059");
            Color SoftIvory = ColorTranslator.FromHtml("#F5F3E9");
            Color SapphireHover = ColorTranslator.FromHtml("#3A5F7A");

            // ==================== FORM SETTINGS ====================
            this.ClientSize = new Size(720, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "My Matched Items";
            this.BackColor = LightBlueNavy;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // ==================== MATCHED PANEL ====================
            pnlMatched = new Guna2Panel
            {
                Size = new Size(700, 400),
                Location = new Point(10, 10),
                FillColor = SoftIvory,
                BorderRadius = 12,
                ShadowDecoration = { Enabled = true }
            };

            // ==================== LISTVIEW ====================
            lvMatched = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                BackColor = Color.WhiteSmoke,
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 10)
            };

            lvMatched.Columns.Add("ID", 50);
            lvMatched.Columns.Add("Item Name", 150);
            lvMatched.Columns.Add("Description", 250);
            lvMatched.Columns.Add("Matched Date", 120);
            lvMatched.Columns.Add("Returned", 80);

            pnlMatched.Controls.Add(lvMatched);
            this.Controls.Add(pnlMatched);
        }
    }
}
