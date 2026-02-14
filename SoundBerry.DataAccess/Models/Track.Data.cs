using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoundBerry.DataAccess.Models
{
    public partial class Track
    {
        public void Save()
        {
            using var connection = DbConfig.OpenConnection();

            if (Id == 0)
            {
                var parameters = new
                {
                    Title,
                    Author,
                    FilePath,
                    Duration = Duration.ToString()
                };
                var sql = """                    
                    INSERT INTO track (title, author, file_path, duration)                    
                        VALUES (@Title, @Author, @FilePath, @Duration);                    
                    """;

                connection.ExecuteScalar(sql, parameters);
            }
            else
            {
                var parameters = new
                {
                    Title,
                    Author,
                    FilePath,
                    Duration = Duration.ToString()
                };
                var sql = @"UPDATE track
                            SET title = @Title,
                                author = @Author,
                                file_path = @FilePath,
                                duration = @Duration
                            WHERE Id = @Id;";
                connection.Execute(sql, parameters);
            }

        }

        public void Delete()
        {
            using var connection = DbConfig.OpenConnection();

            var parameters = new
            {
                Id
            };
            var sql = """
                        DELETE FROM track                 
                        WHERE Id = @Id;
                     """;
            connection.Execute(sql, parameters);
        }

        public static List<Track> GetAll()
        {
            using var connection = DbConfig.OpenConnection();
            var sql = "SELECT Id, Title, Author, file_path as FilePath, Duration FROM track;";

            var tracks = connection.Query<Track>(sql).ToList();

            return tracks;
        }

        public static Track? FindById(int id)
        {
            using var connection = DbConfig.OpenConnection();
            var sql = "SELECT Id, Title, Author, file_path as FilePath, Duration FROM Tracks WHERE Id = @Id;";
            var track = connection.QuerySingleOrDefault<Track>(sql, new { Id = id });

            return track;
        }
    }
}
