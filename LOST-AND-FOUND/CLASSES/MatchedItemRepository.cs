using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace LostAndFound
{
    public static class MatchedItemRepository
    {
        public static List<MatchedItem> GetAll()
        {
            var list = new List<MatchedItem>();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT * FROM MatchedItems;", conn))
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var item = new MatchedItem
                        {
                            Id = Convert.ToInt32(r["Id"]),
                            ItemName = r["ItemName"].ToString(),
                            Description = r["Description"].ToString(),
                            Location = r["Location"].ToString(),
                            MatchedDate = r["MatchedDate"].ToString(),
                            StudentId = r["StudentId"] != DBNull.Value ? r["StudentId"].ToString() : null,
                            AdminUsername = r["AdminUsername"].ToString(),
                            Returned = r["Returned"] != DBNull.Value ? Convert.ToBoolean(r["Returned"]) : false,
                            Image = r["Image"] != DBNull.Value ? (byte[])r["Image"] : null
                        };
                        list.Add(item);
                    }
                }
            }
            return list;
        }
    }
}
