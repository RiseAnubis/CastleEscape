using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleEscape
{
    class Item
    {
        public string ID { get; }
        public string Name { get; }
        public string HelpDescription { get; }

        public Item(string ID, string Name, string Description)
        {
            this.ID = ID;
            this.Name = Name;
            HelpDescription = Description;
        }

        public void ShowInfo(Item Item)
        {
            Console.WriteLine(Item.HelpDescription);
            CommandManager.ReadCommand();
        }
    }
}
