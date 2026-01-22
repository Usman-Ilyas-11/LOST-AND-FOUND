using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace LostAndFound
{
    public static class FoundItemRepository
    {
        public static int Add(FoundItem item)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(@"INSERT INTO FoundItems 
                    (ItemName, Description, LocationFound, FoundDate, PersonName, Contact, Image)
                    VALUES (@name,@desc,@loc,@date,@person,@contact,@img);
                    SELECT last_insert_rowid();", conn))
                {
                    cmd.Parameters.AddWithValue("@name", item.ItemName);
                    cmd.Parameters.AddWithValue("@desc", item.Description);
                    cmd.Parameters.AddWithValue("@loc", item.LocationFound);
                    cmd.Parameters.AddWithValue("@date", item.FoundDate);
                    cmd.Parameters.AddWithValue("@person", item.PersonName);
                    cmd.Parameters.AddWithValue("@contact", item.Contact);
                    cmd.Parameters.AddWithValue("@img", (object)item.Image ?? DBNull.Value);

                    long id = (long)cmd.ExecuteScalar();
                    return Convert.ToInt32(id);
                }
            }
        }

        public static List<FoundItem> GetAll()
        {
            var list = new List<FoundItem>();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT * FROM FoundItems;", conn))
                {
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new FoundItem
                            {
                                Id = Convert.ToInt32(r["Id"]),
                                ItemName = r["ItemName"].ToString(),
                                Description = r["Description"].ToString(),
                                LocationFound = r["LocationFound"].ToString(),
                                FoundDate = r["FoundDate"].ToString(),
                                PersonName = r["PersonName"].ToString(),
                                Contact = r["Contact"].ToString(),
                                Image = r["Image"] != DBNull.Value ? (byte[])r["Image"] : null
                            });
                        }
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
                using (var cmd = new SQLiteCommand("DELETE FROM FoundItems WHERE Id = @id;", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
