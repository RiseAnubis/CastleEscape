using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleEscape
{
    static class Level
    {
        static Room[,] rooms;

        public static void Initialize()
        {
            rooms = new Room[2, 1];
            rooms[0, 0] = new Room { Text = "Dies ist der blaue Raum" };
            rooms[0, 0].AddExit(Exits.East);
            rooms[0, 0].AddExit(Exits.North);
            rooms[0, 0].AddItem(new Item("Schlüssel", "Test"));
            Player.CurrentRoom = rooms[0, 0];

            rooms[1, 0] = new Room { Text = "Dies ist der rote Raum" };
            rooms[1, 0].AddExit(Exits.North);
            rooms[1, 0].AddItem(new Item("Schwert", "Eine Waffe, was denn sonst?"));
        }
    }
}
