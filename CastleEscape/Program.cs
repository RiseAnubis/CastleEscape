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
            Console.WriteLine("Willkommen zu Castle Escape! Drücke eine beliebige Taste zum Fortsetzen.");
            Console.WriteLine("Hinweis: Du kannst jederzeit \"Hilfe\" eingeben, um eine Befehlsliste zu erhalten.");
            Console.ReadKey();
            Console.Clear();
            GameManager.GoToScreen(GameScreens.StartRoom);
            Console.CursorVisible = true;

            do
            {
                CommandManager.ReadCommand();
            }
            while (!GameManager.CanQuit);
        }
    }
}
