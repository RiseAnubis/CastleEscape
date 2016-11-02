using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CastleEscape
{
    static class Level
    {
        public static Room[,] Rooms { get; private set; }

        public static void Initialize()
        {
            Rooms = new Room[2, 2];
            LoadGameFile();
            /*
            // Startraum
            Room room = new Room { Text = "Dies ist der blaue Raum" };
            room.AddExits(new[] { Exits.North, Exits.East });
            room.AddItem(new Item("Sword", "Schwert", "Eine Waffe, was denn sonst?"));
            room.LockExit(Exits.North, "KeyGreen");
            Rooms[0, 0] = room;
            Player.PositionX = 0;
            Player.PositionY = 0;

            // Östlicher Raum
            room = new Room { Text = "Dies ist der rote Raum" };
            room.AddExits(new[] { Exits.North, Exits.West });
            room.AddItem(GameManager.GameItems.Find(x => x.ID == "keyGreen"));
            Rooms[1, 0] = room;

            // Nörldicher Raum
            room = new Room { Text = "Dies ist der grüne Raum" };
            room.AddExits(new[] { Exits.South, Exits.East });
            room.AddItem(new Item("Treasure", "Schatzkiste", "Eine Kiste gefüllt mit begehrten Sachen"));
            Rooms[0, 1] = room;*/
        }

        public static void LoadGameFile()
        {
            XElement levels = XElement.Load("Game.xml").Element("Levels");
            string[] layout, roomPosition;

            foreach (XElement level in levels.Elements()) // Auslesen der Levels
            {
                layout = level.Attribute("Layout").Value.Split(',');
                Rooms = new Room[Convert.ToInt32(layout[0]), Convert.ToInt32(layout[1])];
                XElement rooms = level.Element("Rooms");

                foreach (XElement room in rooms.Elements()) // Auslesen der Räume
                {
                    roomPosition = room.Attribute("Position").Value.Split(',');
                    Room r = new Room { Name = room.Attribute("Name").Value, Text = room.Element("Text").Value };
                    XElement exits = room.Element("Exits");
                    
                    foreach (XElement exit in exits.Elements())
                    {
                        string e = exit.Attribute("Direction").Value; // TODO Möglichkeit, den String aus dem Struct mit dem Value zu verknüpfen?
                        bool isLocked = exit.Attribute("IsLocked").Value == "true";
                        r.AddExits(new[] { e });

                        if (isLocked)
                            r.LockExit(e, exit.Attribute("NecessaryItem").Value);
                    }

                    XElement items = room.Element("Items");

                    foreach (XElement item in items.Elements())
                    {
                        Item i = GameManager.GetGameItem(item.Attribute("ID").Value);

                        if (i != null)
                            r.AddItem(i);
                    }

                    Rooms[Convert.ToInt32(roomPosition[0]), Convert.ToInt32(roomPosition[1])] = r;
                }
            }
        }
    }
}
