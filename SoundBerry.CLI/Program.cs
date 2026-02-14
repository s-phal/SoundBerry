using Dapper;
using NAudio.Wave;
using SoundBerry.DataAccess;
using SoundBerry.DataAccess.Models;
using SoundBerry.DataAccess.TypeHandlers;
using SoundBerry.Playback;
using System.Numerics;

namespace SoundBerry.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {

            DbConfig.Initialize();

            var audioPlayer = new AudioPlayer();


            var selectedIndex = 0;
            var trackList = Track.GetAll();
            var playerController = new PlayerController(audioPlayer, trackList);

           


            while (true)
            {
                Console.Clear();

                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;

                for (int i = 0; i < trackList.Count; i++)
                {
                    if (i == playerController.SelectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.ResetColor();
                    }

                    Console.WriteLine(trackList[i].Title);
                }

                Console.ResetColor();

                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }


                playerController.HandleKeyInput(key);

   



            }

        }
    }
}
