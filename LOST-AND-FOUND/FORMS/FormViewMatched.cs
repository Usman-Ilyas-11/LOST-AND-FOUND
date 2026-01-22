using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using LostAndFound;

namespace LOST_AND_FOUND
{
    public partial class FormViewMatched : Form
    {
        private List<LostItem> lostItems = new List<LostItem>();
        private List<FoundItem> foundItems = new List<FoundItem>();

        public FormViewMatched()
        {
            InitializeComponent();
            LoadLostItems();
            LoadFoundItems();
            LoadMatchedItems(); // load the third view
        }

        private void LoadLostItems()
        {
            lostItems.Clear();
            lvLost.Items.Clear();

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT * FROM LostItems;", conn))
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var item = new LostItem
                        {
                            Id = Convert.ToInt32(r["Id"]),
                            ItemName = r["ItemName"].ToString(),
                            Description = r["Description"].ToString(),
                            LocationLost = r["LocationLost"].ToString(),
                            Image = r["Image"] != DBNull.Value ? (byte[])r["Image"] : null
                        };
                        lostItems.Add(item);
                        var li = new ListViewItem(item.Id.ToString());
                        li.SubItems.Add(item.ItemName);
                        li.SubItems.Add(item.Description);
                        lvLost.Items.Add(li);
                    }
                }
            }
        }

        private void LoadFoundItems()
        {
            foundItems.Clear();
            lvFound.Items.Clear();

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "SELECT * FROM FoundItems WHERE Id NOT IN (SELECT FoundItemId FROM MatchedItems);", conn))
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var item = new FoundItem
                        {
                            Id = Convert.ToInt32(r["Id"]),
                            ItemName = r["ItemName"].ToString(),
                            Description = r["Description"].ToString(),
                            LocationFound = r["LocationFound"].ToString(),
                            Image = r["Image"] != DBNull.Value ? (byte[])r["Image"] : null
                        };
                        foundItems.Add(item);
                        var li = new ListViewItem(item.Id.ToString());
                        li.SubItems.Add(item.ItemName);
                        li.SubItems.Add(item.Description);
                        lvFound.Items.Add(li);
                    }
                }
            }
        }

        private void LoadMatchedItems()
        {
            lvMatched.Items.Clear();

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "SELECT Id, ItemName, Description, MatchedDate, AdminUsername, Returned FROM MatchedItems;", conn))
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var li = new ListViewItem(r["Id"].ToString());
                        li.SubItems.Add(r["ItemName"].ToString());
                        li.SubItems.Add(r["Description"].ToString());
                        li.SubItems.Add(r["MatchedDate"].ToString());
                        li.SubItems.Add(r["AdminUsername"].ToString());
                        li.SubItems.Add(Convert.ToInt32(r["Returned"]) == 1 ? "Yes" : "No");
                        lvMatched.Items.Add(li);
                    }
                }
            }
        }

        private void BtnApproveMatch_Click(object sender, EventArgs e)
        {
            if (lvLost.SelectedItems.Count == 0 || lvFound.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select one Lost and one Found item to approve match.");
                return;
            }

            var lostId = int.Parse(lvLost.SelectedItems[0].Text);
            var foundId = int.Parse(lvFound.SelectedItems[0].Text);

            using (var conn = Database.GetConnection())
            {
                conn.Open();

                // Insert into MatchedItems including StudentId from LostItems
                using (var cmd = new SQLiteCommand(
                    "INSERT INTO MatchedItems (LostItemId, FoundItemId, ItemName, Description, MatchedDate, StudentId, AdminUsername) " +
                    "SELECT l.Id, f.Id, l.ItemName, l.Description, @date, l.StudentId, @admin " +
                    "FROM LostItems l, FoundItems f WHERE l.Id=@lid AND f.Id=@fid;", conn))
                {
                    cmd.Parameters.AddWithValue("@lid", lostId);
                    cmd.Parameters.AddWithValue("@fid", foundId);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                    cmd.Parameters.AddWithValue("@admin", "Admin"); // replace with actual admin username
                    cmd.ExecuteNonQuery();
                }

                // Delete from LostItems and FoundItems
                using (var del = new SQLiteCommand(
                    "DELETE FROM LostItems WHERE Id=@lid; DELETE FROM FoundItems WHERE Id=@fid;", conn))
                {
                    del.Parameters.AddWithValue("@lid", lostId);
                    del.Parameters.AddWithValue("@fid", foundId);
                    del.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Items matched and moved to MatchedItems.");
            LoadLostItems();
            LoadFoundItems();
            LoadMatchedItems(); // refresh the third view
        }


        private void BtnReturnItem_Click(object sender, EventArgs e)
        {
            if (lvMatched.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select a matched item to mark as returned.");
                return;
            }

            var matchId = int.Parse(lvMatched.SelectedItems[0].Text);

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "UPDATE MatchedItems SET Returned=1 WHERE Id=@id;", conn))
                {
                    cmd.Parameters.AddWithValue("@id", matchId);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Item marked as returned.");

            // Refresh all views so student sees it immediately
            LoadLostItems();
            LoadFoundItems();
            LoadMatchedItems(); // student will see their matched item with Returned = Yes
        }

    }
}
