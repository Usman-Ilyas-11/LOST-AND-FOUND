using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LostAndFound;

namespace LOST_AND_FOUND
{
    public partial class FormViewFound : Form
    {
        private List<FoundItem> items;

        public FormViewFound()
        {
            InitializeComponent();
            LoadItems();
        }

        private void LoadItems()
        {
            items = FoundItemRepository.GetAll();
            list.Items.Clear();
            foreach (var it in items)
            {
                var li = new ListViewItem(it.Id.ToString());
                li.SubItems.Add(it.ItemName);
                li.SubItems.Add(it.LocationFound);
                li.SubItems.Add(it.FoundDate);
                li.SubItems.Add(it.PersonName);
                list.Items.Add(li);
            }
        }
    }
}
