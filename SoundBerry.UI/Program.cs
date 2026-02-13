using SoundBerry.DataAccess;
using SoundBerry.DataAccess.Models;

namespace SoundBerry.UI;

class Program
{
    static void Main(string[] args)
    {
        DbConfig.CreateTableIfNotExists();
    }
}
