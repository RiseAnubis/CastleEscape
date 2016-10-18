using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleEscape
{
    /// <summary>
    /// Stellt den Spieler dar
    /// </summary>
    static class Player
    {
        static Inventory inventory = new Inventory(10);
        static int availableSpace;
        static int posX, posY;

        public static Room CurrentRoom { get; set; }

        /// <summary>
        /// Ruft den Namen des Spielers ab oder setzt ihn
        /// </summary>
        public static string Name { get; set; }

        public static void TakeItem(string ItemName)
        {
            Item item = CurrentRoom?.GetItem(ItemName);

            if (item != null)
            {
                if (inventory.IsFull)
                    Console.WriteLine("Dein Inventar ist voll!");
                else
                {
                    inventory.Add(item);
                    CurrentRoom.RemoveItem(item);
                }
            }
            else
                Console.WriteLine("Das Item existiert nicht in diesem Raum!");
        }

        public static void DropItem(string ItemName)
        {
            foreach (Item i in inventory)
            {
                if (string.Equals(i.Name, ItemName, StringComparison.CurrentCultureIgnoreCase))
                {
                    inventory.Remove(i);
                    CurrentRoom.AddItem(i);
                }
            }
        }

        /// <summary>
        /// Zeigt alle Items im Inventar an
        /// </summary>
        public static void ShowInventory()
        {
            Console.Clear();

            if (inventory.IsEmpty)
                Console.WriteLine("Du hast derzeit keine Items im Inventar.");
            else
            {
                Console.WriteLine("Dein Inventar enthält folgende Items:\n");

                foreach (Item i in inventory)
                    Console.WriteLine(i.Name);

                Console.WriteLine();
            }

            Console.WriteLine("Verfügbarer Platz: " + inventory.AvailableSize);
        }

        public static void MoveToDirection(string Direction)
        {
            Directions dir;

            switch (Direction)
            {
                case "nord":
                    dir = Directions.North;
                    break;
                case "süd":
                    dir = Directions.South;
                    break;
                case "west":
                    dir = Directions.West;
                    break;
                case "ost":
                    dir = Directions.East;
                    break;
                default:
                    Console.WriteLine("Die angegebene Richtung existiert nicht!");
                    break;
            }
        }
    }
}
