using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace LostAndFound
{
    public static class UserRepository
    {
        public static bool ValidateLogin(string username, string password, out User user)
        {
            user = null;
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT Id, Username, Password, Role, StudentId FROM Users WHERE Username = @u AND Password = @p;", conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", password);
                    using (var r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            user = new User
                            {
                                Id = Convert.ToInt32(r["Id"]),
                                Username = r["Username"].ToString(),
                                Password = r["Password"].ToString(),
                                Role = r["Role"].ToString(),
                                StudentId = r["StudentId"] != DBNull.Value ? r["StudentId"].ToString() : null
                            };
                            return true;
                        }
                    }
                }
                conn.Close();
            }
            return false;
        }

        public static bool AddStudent(string username, string password, string studentId)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("INSERT INTO Users (Username, Password, Role, StudentId) VALUES (@u,@p,'Student',@sid);", conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", password);
                    cmd.Parameters.AddWithValue("@sid", studentId);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (SQLiteException)
                    {
                        return false;
                    }
                }
            }
        }

        public static List<User> GetAllStudents()
        {
            var list = new List<User>();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT Id, Username, Role, StudentId FROM Users WHERE Role='Student';", conn))
                {
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new User
                            {
                                Id = Convert.ToInt32(r["Id"]),
                                Username = r["Username"].ToString(),
                                Role = r["Role"].ToString(),
                                StudentId = r["StudentId"]?.ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
