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
        /// <summary>
        /// Gibt den Pfad zur XML zurück oder legt ihn fest
        /// </summary>
        public static string FilePath { get; set; }

        /// <summary>
        /// Lädt alle Items, die das Spiel enthält, aus der Spieldatei
        /// </summary>
        /// <returns>Gibt eine Liste zurück, die die Items enthält</returns>
        public static List<Item> LoadItems()
        {
            List<Item> items = new List<Item>();
            XElement root = XElement.Load(FilePath).Element("Items");

            /*foreach (XElement element in root.Elements())
            {
                Item i = new Item(element.Attribute("ID").Value, element.Attribute("Name").Value, element.Attribute("Description").Value);
                items.Exists()
            }*/

            if (root != null)
                return root.Elements().Select(item => new Item(item.Attribute("ID").Value, item.Attribute("Name").Value, item.Attribute("Description").Value)).ToList();
            else
                throw new Exception("There are no items in the XML");
        }
    }
}
