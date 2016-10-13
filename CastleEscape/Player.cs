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
        static Room currentRoom;

        /// <summary>
        /// Ruft den Namen des Spielers ab oder setzt ihn
        /// </summary>
        public static string Name { get; set; }

        public static void TakeItem(string ItemName)
        {
            Item item = currentRoom.Items.Find(x => x.Name == ItemName);

            if (item != null)
            {
                if (inventory.IsFull)
                    Console.WriteLine("Dein Inventar ist voll!");
                else
                    inventory.Add(item);
            }
            else
                Console.WriteLine("Das Item existiert nicht in diesem Raum!");

            CommandManager.ReadCommand();
        }

        public static void RemoveItem(string ItemName)
        {
            foreach (Item i in from Item i in inventory where i.Name == ItemName select i)
                inventory.Remove(i);
        }

        /// <summary>
        /// Zeigt alle Items im Inventar an
        /// </summary>
        public static void ShowInventory()
        {
            foreach (Item i in inventory)
                Console.WriteLine(i.Name);

            CommandManager.ReadCommand();
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
