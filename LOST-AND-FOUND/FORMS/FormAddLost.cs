using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using Guna.UI2.WinForms;
using LostAndFound;

namespace LOST_AND_FOUND
{
    public partial class FormAddLost : Form
    {
        private User CurrentUser;

        public FormAddLost(User user)
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
            var item = new LostItem
            {
                ItemName = txtName.Text.Trim(),
                Description = txtDesc.Text.Trim(),
                LocationLost = txtLoc.Text.Trim(),
                LostDate = txtDate.Text.Trim(),
                PersonName = txtPerson.Text.Trim(),
                Contact = txtContact.Text.Trim(),
                Image = ImageHelper.ImageToBytes(pic.Image),
                StudentId = CurrentUser?.StudentId // <- updated here
            };

            int id = LostItemRepository.Add(item);
            MessageBox.Show("Saved Lost Item. ID: " + id);
            this.Close();
        }

    }
}
