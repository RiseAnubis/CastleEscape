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

        /// <summary>
        /// Gibt den Raum zurück, in dem sich der Spieler derzeit befindet
        /// </summary>
        public static Room CurrentRoom => Level.Rooms[PositionX, PositionY];

        /// <summary>
        /// Die X-Position im Level
        /// </summary>
        public static int PositionX { get; set; }

        /// <summary>
        /// Die Y-Position im Level
        /// </summary>
        public static int PositionY { get; set; }

        /// <summary>
        /// Ruft den Namen des Spielers ab oder setzt ihn
        /// </summary>
        public static string Name { get; set; }

        /// <summary>
        /// Nimmt ein Item auf, wenn das Inventar noch Platz hat
        /// </summary>
        /// <param name="ItemName">Das aufzunehmende Item</param>
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
                    CurrentRoom.ShowDescription();
                }
            }
            else
                Console.WriteLine("Das Item existiert nicht in diesem Raum!");
        }

        /// <summary>
        /// Legt ein Item wieder ab, welches sich dann im derzeitigen Raum befindet
        /// </summary>
        /// <param name="ItemName">Das abzulegende Item</param>
        public static void DropItem(string ItemName)
        {
            Item item = null;

            foreach (Item i in from Item i in inventory where string.Equals(i.Name, ItemName, StringComparison.CurrentCultureIgnoreCase) select i)
                item = i;

            inventory.Remove(item);
            CurrentRoom.AddItem(item);
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

        public static void OpenExit(string Exit)
        {
            if (CurrentRoom.IsExitLocked(Exit))
            {
                Item necessaryItem = CurrentRoom.GetItemToOpenExit(Exit);

                if (necessaryItem != null && inventory.Contains(necessaryItem))
                {
                    CurrentRoom.OpenExit(Exit);
                    TextBuffer.WriteLine("Der Ausgang wurde geöffnet!");
                    TextBuffer.ShowBuffer();
                }
                else
                {
                    TextBuffer.WriteLine("Du besitzt nicht das richtige Item, um den Ausgang zu öffnen!");
                    TextBuffer.ShowBuffer();
                }
            }
            else
            {
                TextBuffer.WriteLine("Der Ausgang war nicht verschlossen!");
                TextBuffer.ShowBuffer();
            }
        }

        /// <summary>
        /// Bewegt den Spieler in den nächsten Raum, wenn die Richtung gültig ist
        /// </summary>
        /// <param name="Direction">Die Richtung, in die sich der Spieler bewegen soll</param>
        public static void MoveToDirection(string Direction)
        {
            if (!CurrentRoom.CanExit(Direction))
            {
                Console.WriteLine("Die Richtung existiert nicht oder der Ausgang ist verschlossen!");
                return;
            }

            switch (Direction)
            {
                case Exits.North:
                    PositionY++;
                    break;
                case Exits.South:
                    PositionY--;
                    break;
                case Exits.West:
                    PositionX--;
                    break;
                case Exits.East:
                    PositionX++;
                    break;
            }

            CurrentRoom.ShowDescription();
        }
    }
}
