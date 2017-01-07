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
        /// Gibt den Pfad zur XML zurück oder legt ihn fest
        /// </summary>
        public static string GameFilePath { get; set; }

        /// <summary>
        /// Lädt das Level und erstellt die Räume
        /// </summary>
        public static void Initialize()
        {
            LoadGameFile();
        }

        /// <summary>
        /// Lädt alle Items, die das Spiel enthält, aus der Spieldatei
        /// </summary>
        /// <returns>Gibt eine Liste zurück, die die Items enthält</returns>
        public static List<Item> LoadItems()
        {
            List<Item> items = new List<Item>();
            XElement root = XElement.Load(GameFilePath).Element("Items");

            if (root == null)
                throw new Exception("There is no item section in the XML");

            foreach (XElement item in root.Elements())
            {
                Item i = new Item(item.Attribute("ID").Value, item.Attribute("Name").Value, item.Attribute("Description").Value);

                if (items.Exists(x => x.ID == i.ID))
                    throw new Exception("There is more than one item with the same ID");

                items.Add(i);
            }

            return items;
        }

        /// <summary>
        /// Lädt die Spieldatei und baut die Levels mit den Räumen auf
        /// </summary>
        static void LoadGameFile()
        {
            string[] layout, roomPosition, startPosition;
            XElement level = XElement.Load("Game.xml");     // Liest die xml Root ein, welche "Level" sein sollte, ansonsten Exception auslösen

            if (level == null)
                throw new Exception("Levels could not be found in the XML");

            layout = level.Attribute("Layout").Value.Split(',');
            startPosition = level.Attribute("StartPosition").Value.Split(',');
            Rooms = new Room[Convert.ToInt32(layout[0]), Convert.ToInt32(layout[1])];
            XElement rooms = level.Element("Rooms");

            if (rooms == null)
                throw new Exception("Rooms could not be found in the XML");

            foreach (XElement room in rooms.Elements()) // Auslesen der Räume
            {
                roomPosition = room.Attribute("Position").Value.Split(',');
                Room r = new Room { Name = room.Attribute("Name").Value, Text = room.Element("Text").Value };
                XElement exits = room.Element("Exits");

                if (exits == null)
                    throw new Exception("Exits could not be found in the XML");

                foreach (XElement exit in exits.Elements()) // Auslesen der Ausgänge im Raum
                {
                    string e = exit.Attribute("Direction").Value; // TODO Möglichkeit, den String aus dem Struct mit dem Value zu verknüpfen?
                    bool isLocked = exit.Attribute("IsLocked").Value == "true";
                    r.AddExits(new[] { e });

                    if (isLocked)
                        r.LockExit(e, exit.Attribute("NecessaryItem").Value);
                }

                XElement items = room.Element("Items");

                if (items == null)
                    throw new Exception("Items for the room could not be found in the XML");

                foreach (XElement item in items.Elements()) // Auslesen der Items im Raum
                {
                    Item i = GameManager.GetGameItem(item.Attribute("ID").Value);

                    if (i != null)
                        r.AddItem(i);
                }

                Rooms[Convert.ToInt32(roomPosition[0]), Convert.ToInt32(roomPosition[1])] = r;
            }

            Player.PositionX = Convert.ToInt32(startPosition[0]);
            Player.PositionY = Convert.ToInt32(startPosition[1]);
        }
    }
}
