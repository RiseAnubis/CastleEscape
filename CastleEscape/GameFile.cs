using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CastleEscape
{
    /// <summary>
    /// Hilfsklasse zum Zugriff auf die Spieldatei
    /// </summary>
    static class GameFile
    {
        public static string FilePath { get; set; }

        /// <summary>
        /// Lädt alle Items, die das Spiel enthält, aus der Spieldatei
        /// </summary>
        /// <returns>Gibt eine Liste zurück, die die Items enthält</returns>
        public static List<Item> LoadItems()
        {
            XElement root = XElement.Load(FilePath).Element("Items");
            return root.Elements().Select(item => new Item(item.Attribute("ID").Value, item.Attribute("Name").Value, item.Attribute("Description").Value)).ToList();
        } 
    }
}
