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
                    Artist,
                    FilePath,
                    Url,
                    DurationSeconds,
                    IsDownloaded
                };
                var sql = """                    
                    INSERT INTO track (title, artist, file_path, url, duration_seconds, is_downloaded)                    
                        VALUES (@Title, @Artist, @FilePath, @Url, @DurationSeconds, @IsDownloaded);                    
                    """;

                connection.ExecuteScalar(sql, parameters);
            }
            else
            {
                var parameters = new
                {
                    Title,
                    Artist,
                    FilePath,
                    Url,
                    DurationSeconds,
                    IsDownloaded
                };
                var sql = @"UPDATE track
                            SET title = @Title,
                                artist = @Artist,
                                file_path = @FilePath,
                                url = @Url,
                                duration_seconds = @DurationSeconds,
                                is_downloaded = @IsDownloaded
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

            var sql = @"SELECT id as Id, 
                               title as Title, 
                               artist as Artist, 
                               file_path as FilePath,
                               url as Url, 
                               duration_seconds as DurationSeconds,
                               is_downloaded as IsDownloaded 
                        FROM track";

            var tracks = connection.Query<Track>(sql).ToList();

            return tracks;
        }

        public static Track? FindById(int id)
        {
            using var connection = DbConfig.OpenConnection();
            var sql = @"SELECT id as Id, 
                               title as Title, 
                               artist as Artist, 
                               file_path as FilePath,
                               url as Url, 
                               duration_seconds as DurationSeconds,
                               is_downloaded as IsDownloaded 
                        FROM track
                        WHERE id = @Id;";
            var track = connection.QuerySingleOrDefault<Track>(sql, new { Id = id });

            return track;
        }
    }
}
