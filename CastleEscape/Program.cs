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
            int choice;

            Console.WriteLine("Willkommen zu Castle Escape!\nWas möchtest du tun?\n");
            Console.WriteLine("1 - Neues Spiel starten");
            Console.WriteLine("2 - Spiel laden\n");
            choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    GameManager.GoToScreen(GameScreens.CharacterCreation);
                    break;
                case 2:
                    break;
            }
        }
    }
}
