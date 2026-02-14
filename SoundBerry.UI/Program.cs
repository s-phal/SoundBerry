using Dapper;
using SoundBerry.DataAccess;
using SoundBerry.DataAccess.Models;
using SoundBerry.DataAccess.TypeHandlers;
using NAudio.Wave;

namespace SoundBerry.UI;

class Program
{

    static void Main(string[] args)
    {
        SqlMapper.AddTypeHandler(new TimeSpanHandler());

        DbConfig.CreateTableIfNotExists();

        var player = new Player();

        var selectedIndex = 0;
        var trackList = Track.GetAll();




        while (true)
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;

            for (int i = 0; i < trackList.Count; i++)
            {
                if (i == selectedIndex)
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

            switch (key)
            {
                case ConsoleKey.Escape:
                    return;

                case ConsoleKey.UpArrow:
                    if (selectedIndex > 0)
                    {
                        selectedIndex--;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (selectedIndex < trackList.Count - 1)
                    {
                        selectedIndex++;
                    }
                    break;

                case ConsoleKey.Enter:
                        player.Play(trackList[selectedIndex]);
                    break;

                case ConsoleKey.Spacebar:
                    if (player.PlaybackState == PlaybackState.Playing)
                    {
                        player.Pause();
                    }
                    else if(player.PlaybackState == PlaybackState.Paused)
                    {
                        player.Resume();
                    }

                    break;
            }




        }

    }
}
