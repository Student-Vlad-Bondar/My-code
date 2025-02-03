using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace SQLite
{
    public class Database
    {
        private string connectionString;

        public Database(string dbPath)
        {
            connectionString = $"Data Source={dbPath};Version=3;";
        }
        public void CreateDatabase()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string createUserTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Username TEXT NOT NULL UNIQUE,
                        Password TEXT NOT NULL
                    );";
                string createGameStatisticsTableQuery = @"
                    CREATE TABLE IF NOT EXISTS GameStatistics (
                        ProgressID INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserID INTEGER NOT NULL UNIQUE,
                        BossesDefeated INTEGER,
                        EnemiesDefeated INTEGER DEFAULT 0,
                        FOREIGN KEY(UserID) REFERENCES Users(UserID)
                    );";

                using (SQLiteCommand cmd = new SQLiteCommand(createUserTableQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                using (SQLiteCommand cmd = new SQLiteCommand(createGameStatisticsTableQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void InsertUser(string username, string password)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public int VerifyUser(string username, string password)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT UserID FROM Users WHERE Username = @Username AND Password = @Password";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }
        public void SaveGameStatistics(int userId, int bossesDefeated, int enemiesDefeated)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = @"
            INSERT INTO GameStatistics (UserID, BossesDefeated, EnemiesDefeated)
            VALUES (@UserID, @BossesDefeated, @EnemiesDefeated)
            ON CONFLICT(UserID) DO UPDATE SET
                BossesDefeated = excluded.BossesDefeated,
                EnemiesDefeated = excluded.EnemiesDefeated;";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@BossesDefeated", bossesDefeated);
                    cmd.Parameters.AddWithValue("@EnemiesDefeated", enemiesDefeated);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public (int BossesDefeated, int EnemiesDefeated)? LoadGameStatistics(int userId)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT BossesDefeated, EnemiesDefeated FROM GameStatistics WHERE UserID = @UserID";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int bossesDefeated = reader.GetInt32(0);
                            int enemiesDefeated = reader.GetInt32(1);
                            return (bossesDefeated, enemiesDefeated);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        public string GetUsername(int userId)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Username FROM Users WHERE UserID = @UserID";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return result.ToString();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}