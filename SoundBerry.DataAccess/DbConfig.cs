using Dapper;
using Microsoft.Data.Sqlite;
using SoundBerry.DataAccess.Models;
using SoundBerry.DataAccess.TypeHandlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoundBerry.DataAccess
{
    public class DbConfig
    {
        //private static readonly string _connectionString = $"Data Source= {Path.Combine(AppContext.BaseDirectory, "SoundBerry.db")}";
        private static readonly string _connectionString = $"Data Source=C:\\Users\\sam\\source\\repos\\s-phal\\SoundBerry\\SoundBerry.DataAccess\\SoundBerry.db";
        public static SqliteConnection OpenConnection()
        {
            var connection = new SqliteConnection(_connectionString);
            connection.Open();

            return connection;
        }


        public static void Initialize()
        {
            CreateTableIfNotExists();
        }


        private static void CreateTableIfNotExists()
        {
            using var connection = OpenConnection();

            var sql = """
            CREATE TABLE IF NOT EXISTS track (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    title TEXT NOT NULL DEFAULT '',
                    artist TEXT NOT NULL DEFAULT '',
                    url TEXT NOT NULL DEFAULT '',
                    file_path TEXT NOT NULL DEFAULT '',
                    duration_seconds INTEGER NOT NULL DEFAULT 0,
                    is_downloaded INTEGER NOT NULL DEFAULT 0
                    );
        """;
            connection.Execute(sql);

            var count = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM track");
            if (count == 0)
            {
                //SeedSampleData();
            }

        }


    //    private static void SeedSampleData()
    //    {
    //        var sampleTracks = new List<Track>
    //        {
    //            new Track
    //            {
    //                Title = "Victory",
    //                Artist = "Two Steps From Hell",
    //                Url = "https://www.youtube.com/watch?v=hKRUPYrAQoE",
    //                FilePath = "",
    //                DurationSeconds = 328,
    //                IsDownloaded = false
    //            },
    //            new Track
    //            {
    //                Title = "Star Sky",
    //        Author = "Two Steps From Hell",
    //        Url = "https://www.youtube.com/watch?v=pICAha0nsb0",
    //        FilePath = "",
    //        DurationSeconds = 334,
    //        IsDownloaded = false
    //    },
    //    new Track
    //    {
    //        Title = "Strength Of A Thousand Men [Instrumental Core Remix]",
    //        Author = "Two Steps From Hell",
    //        Url = "https://www.youtube.com/watch?v=1wtxnk5KE8s",
    //        FilePath = "",
    //        DurationSeconds = 213,
    //        IsDownloaded = false
    //    }
    //};

    //        foreach (var track in sampleTracks)
    //        {
    //            track.Save();
    //        }
    //    }

    }

}