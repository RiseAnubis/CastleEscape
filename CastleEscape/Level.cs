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

            // Startraum
            Room room = new Room { Text = "Dies ist der blaue Raum" };
            room.AddExits(new[] { Exits.North, Exits.East });
            room.AddItem(new Item("Schwert", "Eine Waffe, was denn sonst?"));
            //room.LockExits(new[] { Exits.North });
            room.LockExit(Exits.North, GameManager.KeyGreenRoom);
            Rooms[0, 0] = room;
            Player.PositionX = 0;
            Player.PositionY = 0;

            // Östlicher Raum
            room = new Room { Text = "Dies ist der rote Raum" };
            room.AddExits(new[] { Exits.North, Exits.West });
            room.AddItem(GameManager.KeyGreenRoom);
            Rooms[1, 0] = room;

            // Nörldicher Raum
            room = new Room { Text = "Dies ist der grüne Raum" };
            room.AddExits(new[] { Exits.South, Exits.East });
            room.AddItem(new Item("Schatzkiste", "Eine Kiste gefüllt mit begehrten Sachen"));
            Rooms[0, 1] = room;
        }
    }
}
