using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace LostAndFound
{
    public static class MatchService
    {
        // Very simple matching: exact ItemName (ignoring case) and Location contains or vice versa.
        // Returns list of (lost, found) candidate pairs.
        public static bool ConfirmMatch(LostItem l, FoundItem f, string adminUsername)
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    // Insert matched record
                    using (var cmd = new SQLiteCommand(@"
                INSERT INTO MatchedItems 
                (LostItemId, FoundItemId, ItemName, Description, MatchedDate, StudentId, AdminUsername, Image, Returned)
                VALUES (@lid, @fid, @name, @desc, @date, @sid, @admin, @img, 0);", conn))
                    {
                        cmd.Parameters.AddWithValue("@lid", l.Id);
                        cmd.Parameters.AddWithValue("@fid", f.Id);
                        cmd.Parameters.AddWithValue("@name", l.ItemName);
                        cmd.Parameters.AddWithValue("@desc", l.Description ?? f.Description);
                        cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@sid", (object)l.StudentId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@admin", adminUsername);
                        cmd.Parameters.AddWithValue("@img", (object)(l.Image ?? f.Image) ?? DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }

                    // Delete originals
                    using (var d1 = new SQLiteCommand("DELETE FROM LostItems WHERE Id = @id;", conn))
                    {
                        d1.Parameters.AddWithValue("@id", l.Id);
                        d1.ExecuteNonQuery();
                    }
                    using (var d2 = new SQLiteCommand("DELETE FROM FoundItems WHERE Id = @id;", conn))
                    {
                        d2.Parameters.AddWithValue("@id", f.Id);
                        d2.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

    }

}
