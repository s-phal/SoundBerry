using SoundBerry.CLI.CommonLib;
using SoundBerry.DataAccess.Models;
using SoundBerry.Playback;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace SoundBerry.CLI
{
    public class PlaybackScreen
    {
        public static async Task Run()
        {

            var trackList = Track.GetAll();
            var audioPlayer = new AudioPlayer();
            var selectedConsoleKey = ConsoleKey.None;
            var selectedPlaylistIndex = 0;

            while (true)
            {
                Console.Clear();

                ShowPlaylist(trackList, selectedPlaylistIndex);

                if (trackList.Count > 0)
                {
                    Console.WriteLine("    [A]dd   [D]elete   [U]pdate");
                }
                Console.WriteLine();

                selectedConsoleKey = Console.ReadKey(true).Key;

                switch (selectedConsoleKey)
                {
                    case ConsoleKey.None:
                        break;

                    case ConsoleKey.UpArrow:
                        selectedPlaylistIndex = (selectedPlaylistIndex - 1 + trackList.Count) % trackList.Count;
                        break;

                    case ConsoleKey.DownArrow:
                        selectedPlaylistIndex = (selectedPlaylistIndex + 1) % trackList.Count;
                        break;

                    case ConsoleKey.A:
                        await LibraryScreen.AddTrackAsync();
                        trackList = Track.GetAll(); 
                        break;

                    case ConsoleKey.D:
                        break;


                    case ConsoleKey.Enter:
                        audioPlayer.Play(trackList[selectedPlaylistIndex]);
                        break;
                }


            }

        }

        private static void ShowPlaylist(List<Track> trackList, int selectedPlaylistIndex)
        {
            if (trackList.Count == 0)
            {
                Console.WriteLine();
                ConsoleMessage.PrintError("    No tracks in your playlist yet.\n    Press [A] to start adding new tracks and build your collection.");
                return;
            }


            var paddingCountTitle = GetMaxTitleLength(trackList) + 20;
            var paddingCountArtist = GetMaxArtistLength(trackList) + 5;

            ConsoleMessage.PrintRow("Title", "Artist", "Time", paddingCountTitle, paddingCountArtist);
            ConsoleMessage.PrintRuler(10 + paddingCountTitle + paddingCountArtist);

            for (int i = 0; i < trackList.Count; i++)
            {
                if (i == selectedPlaylistIndex)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                ConsoleMessage.PrintRow(trackList[i].Title, trackList[i].Artist, trackList[i].Duration.ToString(), paddingCountTitle, paddingCountArtist);
            }

            Console.ResetColor();
            Console.WriteLine();
            ConsoleMessage.PrintRuler(10 + paddingCountTitle + paddingCountArtist);

        }

        private static int GetMaxTitleLength(List<Track> trackList)
        {
            var maxLength = 0;

            foreach (var item in trackList)
            {
                maxLength = item.Title.Length > maxLength ? item.Title.Length : maxLength;
            }

            return maxLength;
        }

        private static int GetMaxArtistLength(List<Track> trackList)
        {
            var maxLength = 0;

            foreach (var item in trackList)
            {
                maxLength = item.Artist.Length > maxLength ? item.Artist.Length : maxLength;
            }

            return maxLength;
        }




    }
}
