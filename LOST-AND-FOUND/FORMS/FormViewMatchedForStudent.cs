using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using LostAndFound;

namespace LOST_AND_FOUND
{
    public partial class FormViewMatchedForStudent : Form
    {
        private string StudentId;
        private List<MatchedItem> matchedItems = new List<MatchedItem>();

        public FormViewMatchedForStudent(string studentId)
        {
            StudentId = studentId;
            InitializeComponent();
            LoadMatchedItems();
        }

        private void LoadMatchedItems()
        {
            matchedItems.Clear();
            lvMatched.Items.Clear();

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "SELECT * FROM MatchedItems WHERE StudentId=@sid;", conn))
                {
                    cmd.Parameters.AddWithValue("@sid", StudentId);

                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            var item = new MatchedItem
                            {
                                Id = Convert.ToInt32(r["Id"]),
                                ItemName = r["ItemName"].ToString(),
                                Description = r["Description"].ToString(),
                                MatchedDate = r["MatchedDate"].ToString(),
                                Returned = r["Returned"] != DBNull.Value ? Convert.ToBoolean(r["Returned"]) : false
                            };
                            matchedItems.Add(item);

                            var li = new ListViewItem(item.Id.ToString());
                            li.SubItems.Add(item.ItemName);
                            li.SubItems.Add(item.Description);
                            li.SubItems.Add(item.MatchedDate);
                            li.SubItems.Add(item.Returned ? "Yes" : "No");

                            lvMatched.Items.Add(li);
                        }
                    }
                }
            }
        }

    }
}
