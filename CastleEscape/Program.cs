using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleEscape
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Title = "Castle Escape";
            Console.WriteLine("Willkommen zu Castle Escape! Drücke eine beliebige Taste zum Fortsetzen.");
            Console.WriteLine("Hinweis: Du kannst jederzeit \"Hilfe\" eingeben, um eine Befehlsliste zu erhalten.");
            Console.ReadKey();
            Console.Clear();
            Console.CursorVisible = true;
            Level.Initialize();
            Player.CurrentRoom.ShowDescription();

            while (!GameManager.CanQuit)
                CommandManager.ReadCommand();
        }
    }
}
