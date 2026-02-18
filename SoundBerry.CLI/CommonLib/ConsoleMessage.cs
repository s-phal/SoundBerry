using System;
using System.Collections.Generic;
using System.Text;

namespace SoundBerry.CLI.CommonLib
{
    public static class ConsoleMessage
    {

        public static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(message);
            Console.WriteLine();
            Console.ResetColor();
        }

        public static void PrintSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.WriteLine();
            Console.ResetColor();
        }

        public static void PrintInformational(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.WriteLine();
            Console.ResetColor();
        }

        public static void PrintRuler(int count = 0)
        {
            Console.Write("   ");
            Console.WriteLine(new string('-', count));
        }

        public static void PrintRow(string title, string artist, string time, int paddingCountTitle, int paddingCountArtist)
        {
            if (!TimeSpan.TryParse(time, out var duration))
            {
                duration = TimeSpan.Zero;
            }

            // header
            if (time.Equals("Time"))
            {
                Console.WriteLine("    " + title.PadLeft(5).PadRight(paddingCountTitle) + artist.PadRight(paddingCountArtist) + "Time".PadRight(10));
            }
            else
            {
                Console.WriteLine("    " + title.PadLeft(5).PadRight(paddingCountTitle) + artist.PadRight(paddingCountArtist) + duration.ToString(@"mm\:ss").PadRight(10));

            }

        }
        
    }
}
