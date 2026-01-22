using System.Windows.Forms;
using System.Drawing;

namespace LOST_AND_FOUND
{
    partial class FormShowItem
    {
        private Label lblItemDetails;
        private PictureBox picItemImage;

        private void InitializeComponent()
        {
            // Color palette
            Color LightBlueNavy = ColorTranslator.FromHtml("#2A3B5D"); // background
            Color SoftIvory = ColorTranslator.FromHtml("#F5F3E9");      // card
            Color GoldAccent = ColorTranslator.FromHtml("#C5A059");     // accents
            Color DeepBlue = ColorTranslator.FromHtml("#1F3C58");       // text

            // Form properties
            this.ClientSize = new Size(700, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Item Details";
            this.BackColor = LightBlueNavy;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // PictureBox for image
            picItemImage = new PictureBox
            {
                Left = 40,
                Top = 30,
                Width = 250,
                Height = 250,
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = SoftIvory
            };

            // Label for item details (styled as card)
            lblItemDetails = new Label
            {
                Left = 320,
                Top = 30,
                Width = 340,
                Height = 250,
                BackColor = SoftIvory,
                ForeColor = DeepBlue,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(15),
                AutoSize = false
            };

            // Optional: add a subtle title label above details
            Label lblTitle = new Label
            {
                Text = "Item Information",
                Left = 320,
                Top = 0,
                Width = 340,
                Height = 30,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = GoldAccent,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = LightBlueNavy
            };

            this.Controls.Add(picItemImage);
            this.Controls.Add(lblItemDetails);
            this.Controls.Add(lblTitle);
        }
    }
}
