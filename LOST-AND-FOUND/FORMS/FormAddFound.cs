using System;
using System.Drawing;
using System.Windows.Forms;
using LostAndFound;
using Guna.UI2.WinForms;
using System.Xml.Linq;

namespace LOST_AND_FOUND
{
    public partial class FormAddFound : Form
    {
        private User CurrentUser;

        public FormAddFound(User user)
        {
            CurrentUser = user;
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Images|*.png;*.jpg;*.jpeg;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pic.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var item = new FoundItem
            {
                ItemName = txtName.Text.Trim(),
                Description = txtDesc.Text.Trim(),
                LocationFound = txtLoc.Text.Trim(),
                FoundDate = txtDate.Text.Trim(),
                PersonName = txtPerson.Text.Trim(),
                Contact = txtContact.Text.Trim(),
                Image = ImageHelper.ImageToBytes(pic.Image)
            };

            int id = FoundItemRepository.Add(item);
            MessageBox.Show("Saved Found Item. ID: " + id);
            this.Close();
        }
    }
}
