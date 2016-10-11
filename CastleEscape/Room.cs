using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleEscape
{
    enum Directions { North, South, East, West }

    class Room
    {
        public string Text { get; }
        public Directions Direction { get; }
        public List<Item> Items { get; } 
    }
}
