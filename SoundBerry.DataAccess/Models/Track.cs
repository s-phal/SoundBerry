namespace SoundBerry.DataAccess.Models
{
    public partial class Track
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; } = new TimeSpan(0, 0, 0);
    }
}
