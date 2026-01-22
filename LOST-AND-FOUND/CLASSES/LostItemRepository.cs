using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace LostAndFound
{
    public static class LostItemRepository
    {
        public static int Add(LostItem item)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(@"INSERT INTO LostItems 
            (ItemName, Description, LocationLost, LostDate, PersonName, Contact, Image, StudentId)
            VALUES (@name,@desc,@loc,@date,@person,@contact,@img,@sid);
            SELECT last_insert_rowid();", conn))
                {
                    cmd.Parameters.AddWithValue("@name", item.ItemName);
                    cmd.Parameters.AddWithValue("@desc", item.Description);
                    cmd.Parameters.AddWithValue("@loc", item.LocationLost);
                    cmd.Parameters.AddWithValue("@date", item.LostDate);
                    cmd.Parameters.AddWithValue("@person", item.PersonName);
                    cmd.Parameters.AddWithValue("@contact", item.Contact);
                    cmd.Parameters.AddWithValue("@img", (object)item.Image ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@sid", (object)item.StudentId ?? DBNull.Value); // <- updated

                    long id = (long)cmd.ExecuteScalar();
                    return Convert.ToInt32(id);
                }
            }
        }

        public static List<LostItem> GetAll()
        {
            var list = new List<LostItem>();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT * FROM LostItems;", conn))
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new LostItem
                        {
                            Id = Convert.ToInt32(r["Id"]),
                            ItemName = r["ItemName"].ToString(),
                            Description = r["Description"].ToString(),
                            LocationLost = r["LocationLost"].ToString(),
                            LostDate = r["LostDate"].ToString(),
                            PersonName = r["PersonName"].ToString(),
                            Contact = r["Contact"].ToString(),
                            Image = r["Image"] != DBNull.Value ? (byte[])r["Image"] : null,
                            StudentId = r["StudentId"] != DBNull.Value ? r["StudentId"].ToString() : null // <- updated
                        });
                    }
                }
            }
            return list;
        }


        public static void Delete(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("DELETE FROM LostItems WHERE Id = @id;", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
