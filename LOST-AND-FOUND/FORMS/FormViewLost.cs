using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LostAndFound;

namespace LOST_AND_FOUND
{
    public partial class FormViewLost : Form
    {
        private List<LostItem> items;

        public FormViewLost()
        {
            InitializeComponent();
            LoadItems();
        }

        private void LoadItems()
        {
            items = LostItemRepository.GetAll();
            list.Items.Clear();
            foreach (var it in items)
            {
                var li = new ListViewItem(it.Id.ToString());
                li.SubItems.Add(it.ItemName);
                li.SubItems.Add(it.LocationLost);
                li.SubItems.Add(it.LostDate);
                li.SubItems.Add(it.PersonName);
                list.Items.Add(li);
            }
        }
    }
}
