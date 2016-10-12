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

        public static void TakeItem(string Name)
        {
            Item item = currentRoom.Items.Find(x => x.Name == Name);

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

        public static void RemoveItem(Item Item)
        {
            inventory.Remove(Item);
        }

        public static void ShowInventory()
        {
            foreach (Item i in inventory)
                Console.WriteLine(i.Name);

            CommandManager.ReadCommand();
        }
    }
}
