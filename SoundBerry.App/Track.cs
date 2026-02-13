using Microsoft.Data.Sqlite;

namespace SoundBerry.App;

public class Track
{
    private static readonly string _connectionString = "Data Source=soundberry.db;";

    public int Id { get; set; } = 0;
    public string? Title { get; set; } = string.Empty;
    public string? Artist { get; set; } = string.Empty;
    public TimeSpan? Duration { get; set; } = TimeSpan.Zero;

    private static SqliteConnection OpenConnection()
    {
        var connection = new SqliteConnection(_connectionString);
        connection.Open();

        return connection;
    }

    private static Track LoadReader(SqliteDataReader reader)
    {
        return new Track
        {
            Id = Convert.ToInt32(reader["Id"]),
            Artist = reader.IsDBNull(reader.GetOrdinal("artist")) ? null : reader["artist"].ToString(),
            Title = reader.IsDBNull(reader.GetOrdinal("title")) ? string.Empty : reader["title"].ToString(),
            Duration = reader.IsDBNull(reader.GetOrdinal("duration"))
                ? TimeSpan.Zero
                : TimeSpan.FromSeconds(reader.GetInt32(reader.GetOrdinal("duration")))
        };
    }
}