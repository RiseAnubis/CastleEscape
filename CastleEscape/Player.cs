using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleEscape
{
    static class Player
    {
        public static string Name { get; set; }
        public static Inventory Inventory { get; set; }

        public static void TakeItem(Item Item)
        {
            if (!Inventory.IsFull)
                Inventory.Add(Item);
        }
    }
}
