using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleEscape
{
    enum Directions { North, South, East, West }

    struct Exits
    {
        public const string North = "nord";
        public const string East = "ost";
        public const string South = "süd";
        public const string West = "west";
    }

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

        public void AddExit(string Exit)
        {
            if (exits.Contains(Exit))
                Console.WriteLine("Der Ausgang ist bereits vorhanden!");
            else
                exits.Add(Exit);
        }

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

        public bool CanExit(string Direction) => exits.Contains(Direction);

        public void AddItem(Item Item) => items.Add(Item);

        public void RemoveItem(Item Item) => items.Remove(Item);

        public Item GetItem(string ItemName) => items.Find(x => string.Equals(x.Name, ItemName, StringComparison.CurrentCultureIgnoreCase));
    }
}
