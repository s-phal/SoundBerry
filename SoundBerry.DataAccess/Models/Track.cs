namespace SoundBerry.DataAccess.Models
{
    public partial class Track
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public int DurationSeconds { get; set; } = 0;
        public TimeSpan Duration => TimeSpan.FromSeconds(DurationSeconds);
        public bool IsDownloaded { get; set; } = false;
    }
}
