using System;
using System.Data.SQLite;
using System.IO;

namespace LostAndFound
{
    public static class Database
    {
        private static readonly string DbFileName = "database.db";
        private static readonly string DbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DbFileName);
        private static readonly string ConnectionString = $"Data Source={DbPath};Version=3;";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(ConnectionString);
        }

        public static void Initialize()
        {
            bool isNewDb = !File.Exists(DbPath);

            if (isNewDb)
            {
                SQLiteConnection.CreateFile(DbPath);
            }

            using (var conn = GetConnection())
            {
                conn.Open();

                // --- Create tables if they do not exist ---
                ExecuteNonQuery(conn, @"
                    CREATE TABLE IF NOT EXISTS LostItems (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        ItemName TEXT,
                        Description TEXT,
                        LocationLost TEXT,
                        LostDate TEXT,
                        PersonName TEXT,
                        Contact TEXT,
                        StudentId TEXT,
                        Image BLOB
                    );
                ");

                ExecuteNonQuery(conn, @"
                    CREATE TABLE IF NOT EXISTS FoundItems (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        ItemName TEXT,
                        Description TEXT,
                        LocationFound TEXT,
                        FoundDate TEXT,
                        PersonName TEXT,
                        Contact TEXT,
                        Image BLOB
                    );
                ");

                ExecuteNonQuery(conn, @"
                    CREATE TABLE IF NOT EXISTS Users (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Username TEXT UNIQUE,
                        Password TEXT,
                        Role TEXT,
                        StudentId TEXT
                    );
                ");

                ExecuteNonQuery(conn, @"
                    CREATE TABLE IF NOT EXISTS MatchedItems (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        LostItemId INTEGER,
                        FoundItemId INTEGER,
                        ItemName TEXT,
                        Description TEXT,
                        MatchedDate TEXT,
                        StudentId TEXT,
                        AdminUsername TEXT,
                        Returned INTEGER DEFAULT 0,
                        Image BLOB
                    );
                ");

                // --- Ensure missing columns exist for older DBs ---
                AddColumnIfNotExists(conn, "LostItems", "StudentId", "TEXT");
                AddColumnIfNotExists(conn, "MatchedItems", "LostItemId", "INTEGER");
                AddColumnIfNotExists(conn, "MatchedItems", "FoundItemId", "INTEGER");
                AddColumnIfNotExists(conn, "MatchedItems", "Returned", "INTEGER DEFAULT 0");
                AddColumnIfNotExists(conn, "MatchedItems", "StudentId", "TEXT");

                // --- Ensure default admin exists ---
                using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Users WHERE Username='ADMIN';", conn))
                {
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count == 0)
                    {
                        using (var ins = new SQLiteCommand(
                            "INSERT INTO Users (Username, Password, Role) VALUES (@u,@p,'Admin');", conn))
                        {
                            ins.Parameters.AddWithValue("@u", "ADMIN");
                            ins.Parameters.AddWithValue("@p", "12345");
                            ins.ExecuteNonQuery();
                        }
                    }
                }

                conn.Close();
            }
        }

        private static void ExecuteNonQuery(SQLiteConnection conn, string sql)
        {
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private static void AddColumnIfNotExists(SQLiteConnection conn, string tableName, string columnName, string columnType)
        {
            using (var cmd = new SQLiteCommand($"PRAGMA table_info({tableName});", conn))
            using (var reader = cmd.ExecuteReader())
            {
                bool exists = false;
                while (reader.Read())
                {
                    if (reader["name"].ToString().Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    using (var add = new SQLiteCommand($"ALTER TABLE {tableName} ADD COLUMN {columnName} {columnType};", conn))
                    {
                        add.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
