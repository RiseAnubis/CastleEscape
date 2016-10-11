using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleEscape
{
    class Item
    {
        public string Name { get; }
        public string HelpDescription { get; }

        public Item(string Name, string Description)
        {
            this.Name = Name;
            HelpDescription = Description;
        }

        public static void ShowItemInfo(Item Item)
        {
            Console.WriteLine(Item.HelpDescription);
            CommandManager.ReadCommand();
        }
    }
}
