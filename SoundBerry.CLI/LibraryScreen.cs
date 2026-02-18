using SoundBerry.CLI.CommonLib;
using SoundBerry.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using YoutubeExplode;

namespace SoundBerry.CLI
{
    public class LibraryScreen
    {
        public static async Task AddTrackAsync()
        {
            Console.Clear();
            Console.WriteLine();
            string input = string.Empty;

            using var youtube = new YoutubeClient();
            var track = new Track();

            Console.WriteLine("    Enter YouTube Url:");
            track.Url = Console.ReadLine();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("    Fetching video details...");
            var video = await youtube.Videos.GetAsync(track.Url);
            Console.WriteLine("Done");
            Console.ResetColor();


            Console.Write($"    Enter track title (default:{video.Title}): ");
            input = Console.ReadLine();
            track.Title = string.IsNullOrWhiteSpace(input) ? video.Title.Trim() : input;


            Console.Write($"    Enter track artist (default: {video.Author}): ");
            input = Console.ReadLine();
            track.Artist = string.IsNullOrWhiteSpace(input) ? video.Author.ToString().Trim() : input;


            Console.Write("    Download local copy? [Y/N] (default: N): ");
            input = Console.ReadLine();
            track.IsDownloaded = string.IsNullOrWhiteSpace(input) || !input.ToLower().Equals("y") ? false : true;


            track.FilePath = "";
            track.DurationSeconds = (int)video.Duration.Value.TotalSeconds;



            track.Save();
        }

    }
}
