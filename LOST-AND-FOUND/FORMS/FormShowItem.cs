using System;
using System.Drawing;
using System.Windows.Forms;
using LostAndFound;

namespace LOST_AND_FOUND
{
    public partial class FormShowItem : Form
    {
        public FormShowItem(string name, string desc, string loc, string date, string person, string contact, byte[] image)
        {
            InitializeComponent();

            this.Text = name;

            lblItemDetails.Text = $"Name: {name}\n\nDesc: {desc}\n\nLoc: {loc}\n\nDate: {date}\n\nPerson: {person}\n\nContact: {contact}";

            if (image != null)
            {
                picItemImage.Image = ImageHelper.BytesToImage(image);
            }
        }
    }
}
