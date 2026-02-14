using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoundBerry.DataAccess
{
    public class DbConfig
    {
        private static readonly string _connectionString = "Data Source= C:\\Users\\sam\\source\\repos\\s-phal\\SoundBerry\\SoundBerry.UI\\SoundBerry.db";


        public static SqliteConnection OpenConnection()
        {
            var connection = new SqliteConnection(_connectionString);
            connection.Open();

            return connection;
        }



        public static void CreateTableIfNotExists()
        {
            using var connection = OpenConnection();

            var sql = """
            CREATE TABLE IF NOT EXISTS track (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    title TEXT NOT NULL,
                    author TEXT DEFAULT '',
                    file_path TEXT DEFAULT '',
                    duration INTEGER DEFAULT 0
                    );
        """;
            connection.Execute(sql);

        }


    }
}
