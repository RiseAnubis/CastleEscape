using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleEscape
{
    struct Exits
    {
        public const string North = "nord";
        public const string East = "ost";
        public const string South = "süd";
        public const string West = "west";
    }

    /// <summary>
    /// Stellt einen Raum dar
    /// </summary>
    class Room
    {
        List<string> exits;
        List<Item> items;

        public string Name { get; set; }
        public string Text { get; set; }

        public Room()
        {
            exits = new List<string>();
            items = new List<Item>();
        }

        /// <summary>
        /// Fügt dem Raum einen Ausgang hinzu
        /// </summary>
        /// <param name="Exit">Der hinzuzufügende Ausgang</param>
        public void AddExit(string Exit)
        {
            if (!exits.Contains(Exit))
                exits.Add(Exit);
        }

        public void AddExits(string[] Exits)
        {
            foreach (string exit in Exits.Where(exit => !exits.Contains(exit)))
                exits.Add(exit);
        }

        /// <summary>
        /// Zeigt eine Beschreibung des Raumes an mit den verfügbaren Items und Ausgängen
        /// </summary>
        public void ShowDescription()
        {
            Console.Clear();
            TextBuffer.WriteLine(Text + "\n");
            TextBuffer.WriteLine("Verfügbare Items:");

            foreach (Item item in items)
                TextBuffer.WriteLine(item.Name);

            TextBuffer.WriteLine("\nVerfügbare Ausgänge:");

            foreach (string exit in exits)
                TextBuffer.WriteLine(exit);

            TextBuffer.ShowBuffer();
        }

        /// <summary>
        /// Gibt an, ob der Raum durch den angegebenen Ausgang verlassen werden kann
        /// </summary>
        /// <param name="Direction">Der Ausgang, durch den der Raum verlassen werden soll</param>
        /// <returns></returns>
        public bool CanExit(string Direction) => exits.Contains(Direction);

        /// <summary>
        /// Fügt dem Raum ein Item hinzu
        /// </summary>
        /// <param name="Item"></param>
        public void AddItem(Item Item) => items.Add(Item);

        /// <summary>
        /// Entfernt ein Item aus dem Raum
        /// </summary>
        /// <param name="Item"></param>
        public void RemoveItem(Item Item) => items.Remove(Item);

        /// <summary>
        /// Gibt ein im Raum befindliches Item zurück
        /// </summary>
        /// <param name="ItemName"></param>
        /// <returns></returns>
        public Item GetItem(string ItemName) => items.Find(x => string.Equals(x.Name, ItemName, StringComparison.CurrentCultureIgnoreCase));
    }
}
