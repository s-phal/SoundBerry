using Dapper;
using NAudio.Wave;
using SoundBerry.DataAccess;
using SoundBerry.DataAccess.Models;
using SoundBerry.Playback;
using System.Numerics;
using System.Text;

namespace SoundBerry.CLI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            DbConfig.Initialize();

            await PlaybackScreen.Run();

        }
    }
}
