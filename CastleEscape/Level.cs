using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CastleEscape
{
    /// <summary>
    /// Klasse zum Laden eines Levels
    /// </summary>
    static class Level
    {
        /// <summary>
        /// Das Array der vorhandenen Räume als Darstellung in Zeilen und Spalten
        /// </summary>
        public static Room[,] Rooms { get; private set; }

        /// <summary>
        /// Lädt das Level und erstellt die Räume
        /// </summary>
        public static void Initialize()
        {
            LoadGameFile();
        }

        public static void LoadGameFile()
        {
            string[] layout, roomPosition;
            XElement levels = XElement.Load("Game.xml").Element("Levels");

            if (levels != null)
            {
                foreach (XElement level in levels.Elements()) // Auslesen der Levels
                {
                    layout = level.Attribute("Layout").Value.Split(',');
                    Rooms = new Room[Convert.ToInt32(layout[0]), Convert.ToInt32(layout[1])];
                    XElement rooms = level.Element("Rooms");

                    if (rooms != null)
                    {
                        foreach (XElement room in rooms.Elements()) // Auslesen der Räume
                        {
                            roomPosition = room.Attribute("Position").Value.Split(',');
                            Room r = new Room { Name = room.Attribute("Name").Value, Text = room.Element("Text").Value };
                            XElement exits = room.Element("Exits");

                            if (exits != null)
                                foreach (XElement exit in exits.Elements())
                                {
                                    string e = exit.Attribute("Direction").Value; // TODO Möglichkeit, den String aus dem Struct mit dem Value zu verknüpfen?
                                    bool isLocked = exit.Attribute("IsLocked").Value == "true";
                                    r.AddExits(new[] { e });

                                    if (isLocked)
                                        r.LockExit(e, exit.Attribute("NecessaryItem").Value);
                                }
                            else
                                throw new Exception("Exits could not be found in the XML");

                            XElement items = room.Element("Items");

                            if (items != null)
                                foreach (XElement item in items.Elements())
                                {
                                    Item i = GameManager.GetGameItem(item.Attribute("ID").Value);

                                    if (i != null)
                                        r.AddItem(i);
                                }
                            else
                                throw new Exception("Items for the room could not be found in the XML");

                            Rooms[Convert.ToInt32(roomPosition[0]), Convert.ToInt32(roomPosition[1])] = r;
                        }
                    }
                    else
                        throw new Exception("Rooms could not be found in the XML");
                }
            }
            else
                throw new Exception("Levels could not be found in the XML");

            Player.PositionX = 0;
            Player.PositionY = 0;
        }
    }
}
