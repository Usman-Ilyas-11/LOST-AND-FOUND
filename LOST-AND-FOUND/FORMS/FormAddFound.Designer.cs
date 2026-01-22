using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;

namespace LOST_AND_FOUND
{
    partial class FormAddFound
    {
        private Guna2TextBox txtName;
        private Guna2TextBox txtDesc;
        private Guna2TextBox txtLoc;
        private Guna2TextBox txtDate;
        private Guna2TextBox txtPerson;
        private Guna2TextBox txtContact;

        private PictureBox pic;
        private Guna2Button btnBrowse;
        private Guna2Button btnSave;

        private void InitializeComponent()
        {
            // Color palette
            Color LightBlueNavy = ColorTranslator.FromHtml("#2A3B5D"); // lighter navy-blue background
            Color DeepBlue = ColorTranslator.FromHtml("#1F3C58");
            Color GoldAccent = ColorTranslator.FromHtml("#C5A059");
            Color SapphireHover = ColorTranslator.FromHtml("#3A5F7A");
            Color SoftIvory = ColorTranslator.FromHtml("#F5F3E9");

            // FORM SETTINGS - larger, elegant
            this.Text = "Add Found Item";
            this.ClientSize = new Size(750, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = LightBlueNavy;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // TextBoxes
            txtName = CreateTextBox("Item Name", 30, 30, SoftIvory);
            txtDesc = CreateTextBox("Description", 30, 90, SoftIvory, true, 90);
            txtLoc = CreateTextBox("Location Found", 30, 200, SoftIvory);
            txtDate = CreateTextBox("Date (YYYY-MM-DD)", 30, 250, SoftIvory);
            txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtPerson = CreateTextBox("Finder Name", 30, 300, SoftIvory);
            txtContact = CreateTextBox("Contact", 30, 350, SoftIvory);

            // PictureBox
            pic = new PictureBox();
            pic.BorderStyle = BorderStyle.FixedSingle;
            pic.Location = new Point(450, 50);
            pic.Size = new Size(250, 250);
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            pic.BackColor = SoftIvory;

            // Browse Button
            btnBrowse = new Guna2Button();
            btnBrowse.Text = "Browse Image";
            btnBrowse.Location = new Point(450, 320);
            btnBrowse.Size = new Size(250, 45);
            btnBrowse.FillColor = GoldAccent;
            btnBrowse.ForeColor = Color.White;
            btnBrowse.HoverState.FillColor = SapphireHover;
            btnBrowse.BorderRadius = 10;
            btnBrowse.Click += btnBrowse_Click;

            // Save Button
            btnSave = new Guna2Button();
            btnSave.Text = "Save";
            btnSave.Location = new Point(30, 420);
            btnSave.Size = new Size(180, 50);
            btnSave.FillColor = GoldAccent;
            btnSave.ForeColor = Color.White;
            btnSave.HoverState.FillColor = SapphireHover;
            btnSave.BorderRadius = 12;
            btnSave.Click += btnSave_Click;

            // Add Controls
            this.Controls.AddRange(new Control[] {
                txtName, txtDesc, txtLoc, txtDate, txtPerson, txtContact,
                pic, btnBrowse, btnSave
            });
        }

        // Helper for elegant Guna2TextBox
        private Guna2TextBox CreateTextBox(string placeholder, int x, int y, Color fillColor, bool multiline = false, int height = 45)
        {
            var txt = new Guna2TextBox();
            txt.PlaceholderText = placeholder;
            txt.Location = new Point(x, y);
            txt.Size = new Size(380, height);
            txt.FillColor = fillColor;
            txt.BorderRadius = 10;
            txt.ForeColor = Color.Black;
            txt.Multiline = multiline;
            txt.Padding = new Padding(10, 5, 10, 5);
            return txt;
        }
    }
}
