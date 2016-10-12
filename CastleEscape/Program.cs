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
            int choice = 0;

            Console.WriteLine("Willkommen zu Castle Escape!\nWas möchtest du tun?\n");
            Console.WriteLine("1 - Neues Spiel starten");
            Console.WriteLine("2 - Spiel laden\n");

            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Die Eingabe ist nicht korrekt!");
            }

            switch (choice)
            {
                case 1:
                    GameManager.GoToScreen(GameScreens.CharacterCreation);
                    break;
                case 2:
                    break;
            }

            /*Player.TakeItem(new Item("test", "Beispiel"));
            Player.TakeItem(new Item("wewe", "Beispiel"));
            Player.TakeItem(new Item("bnbn", "Beispiel"));
            Player.ShowInventory();*/
            Console.ReadLine();
        }
    }
}
