using NAudio.Wave;

namespace SoundBerry.App;

internal static class Program
{
    private static WaveOutEvent _player;
    private static AudioFileReader _audioFile;

    private static void Main(string[] args)
    {
        Console.Clear();

        var selectedIndex = 0;
        var key = ConsoleKey.None;

        var items = new List<string>
        {
            "sample1.mp3",
            "sample2.mp3",
            "sample3.mp3"
        };


        while (true)
        {
            Console.Clear();

            for (int i = 0; i < items.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"{items[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{items[i]}");
                }
            }

            key = Console.ReadKey().Key;

            if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                selectedIndex--;

            if (key == ConsoleKey.DownArrow && selectedIndex < items.Count - 1)
                selectedIndex++;

            if (key == ConsoleKey.Enter)
            {
                PlayFile(items[selectedIndex]);
            }

            if (key == ConsoleKey.Escape)
            {
                StopPlayer();
            }
        }
    }

    private static void StopPlayer()
    {
        _player?.Stop();
        _player?.Dispose();
    }

    private static void PlayFile(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath)) return;

        _player?.Stop();
        _player?.Dispose();

        _audioFile?.Dispose();

        _audioFile = new AudioFileReader(filePath);
        _player = new WaveOutEvent();

        _player.Init(_audioFile);
        _player.Play();
    }
}