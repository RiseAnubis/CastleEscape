using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleEscape
{
    class Inventory
    {
        List<Item> items;
         
        public int Size { get; }
        public bool IsFull { get; }

        public void Add(Item Item)
        {
            
        }

        public void Remove(Item Item) => items.Remove(Item);
    }
}
