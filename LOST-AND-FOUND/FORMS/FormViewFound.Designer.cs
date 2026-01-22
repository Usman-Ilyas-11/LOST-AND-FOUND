using System.Windows.Forms;
using System.Drawing;
using Guna.UI2.WinForms;

namespace LOST_AND_FOUND
{
    partial class FormViewFound
    {
        private Guna2Panel mainPanel;
        private ListView list;

        private void InitializeComponent()
        {
            // Color palette
            Color LightBlueNavy = ColorTranslator.FromHtml("#2A3B5D"); // lightest navy for background
            Color DeepBlue = ColorTranslator.FromHtml("#1F3C58");
            Color GoldAccent = ColorTranslator.FromHtml("#C5A059");
            Color SoftIvory = ColorTranslator.FromHtml("#F5F3E9");
            Color SapphireHover = ColorTranslator.FromHtml("#3A5F7A");

            // Form properties
            this.ClientSize = new Size(950, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Found Items";
            this.BackColor = LightBlueNavy;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Main panel to hold ListView
            mainPanel = new Guna2Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.FillColor = SoftIvory;
            mainPanel.Padding = new Padding(10);
            mainPanel.BorderRadius = 12;

            // ListView setup
            list = new ListView
            {
                View = View.Details,
                Dock = DockStyle.Fill,
                FullRowSelect = true,
                GridLines = true,
                BackColor = Color.WhiteSmoke,
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };

            // Add columns
            list.Columns.Add("Id", 50, HorizontalAlignment.Center);
            list.Columns.Add("Item", 200, HorizontalAlignment.Left);
            list.Columns.Add("Location", 200, HorizontalAlignment.Left);
            list.Columns.Add("Date", 120, HorizontalAlignment.Center);
            list.Columns.Add("Finder", 150, HorizontalAlignment.Left);

            list.DoubleClick += new System.EventHandler(this.List_DoubleClick);

            // Add ListView to panel and panel to form
            mainPanel.Controls.Add(list);
            this.Controls.Add(mainPanel);
        }

        // Optional event handler for designer
        private void List_DoubleClick(object sender, System.EventArgs e)
        {
            if (list.SelectedItems.Count == 0) return;
            int idx = int.Parse(list.SelectedItems[0].Text);
            var it = items.Find(x => x.Id == idx);
            if (it != null)
            {
                var dlg = new FormShowItem(it.ItemName, it.Description, it.LocationFound, it.FoundDate, it.PersonName, it.Contact, it.Image);
                dlg.ShowDialog();
            }
        }
    }
}
