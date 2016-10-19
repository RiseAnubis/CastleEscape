using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleEscape
{
    static class Level
    {
        public static Room[,] Rooms { get; private set; }

        public static void Initialize()
        {
            Rooms = new Room[2, 2];
            Room room = new Room();
            Rooms[0, 0] = room;
            room.Text = "Dies ist der blaue Raum";
            room.AddExit(Exits.East);
            room.AddExit(Exits.North);
            room.AddItem(new Item("Schlüssel", "Test"));
            Player.PositionX = 0;
            Player.PositionY = 0;

            room = new Room();
            Rooms[1, 0] = room;
            room.Text = "Dies ist der rote Raum";
            room.AddExit(Exits.North);
            room.AddItem(new Item("Schwert", "Eine Waffe, was denn sonst?"));
        }
    }
}
