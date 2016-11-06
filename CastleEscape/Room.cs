﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleEscape
{
    /// <summary>
    /// Struktur, die die möglichen Ausgänge enthält
    /// </summary>
    struct Exits
    {
        public const string North = "nord";
        public const string East = "ost";
        public const string South = "süd";
        public const string West = "west";
    }

    /// <summary>
    /// Stellt einen Raum dar
    /// </summary>
    class Room
    {
        List<string> exits;
        Dictionary<string, string> lockedExits;
        List<Item> items;

        /// <summary>
        /// Der Name des Raums
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Die Beschreibung des Raumes, der beim Betreten angezeigt wird
        /// </summary>
        public string Text { get; set; }

        public Room()
        {
            exits = new List<string>();
            items = new List<Item>();
            lockedExits = new Dictionary<string, string>();
        }

        /// <summary>
        /// Fügt dem Raum einen oder mehrere Ausgänge hinzu
        /// </summary>
        /// <param name="Exits">Array mit den Ausgängen, die der Raum haben soll</param>
        public void AddExits(string[] Exits)
        {
            foreach (string exit in Exits.Where(exit => !exits.Contains(exit)))
                exits.Add(exit);
        }

        /// <summary>
        /// Schließt einen Ausgang ab
        /// </summary>
        /// <param name="Exit">Der abzuschließene Ausgang</param>
        /// <param name="ItemID">Das Item, mit dem der Ausgang geöffnet werden kann</param>
        public void LockExit(string Exit, string ItemID) => lockedExits.Add(Exit, ItemID);

        /// <summary>
        /// Gibt das Item zurück, mit dem der angegebene Ausgang geöffnet werden kann
        /// </summary>
        /// <param name="Exit"></param>
        /// <returns></returns>
        public Item GetItemToOpenExit(string Exit) => lockedExits.Where(x => x.Key == Exit).Select(x => GameManager.GetGameItem(x.Value)).FirstOrDefault();

        /// <summary>
        /// Zeigt eine Beschreibung des Raumes an mit den verfügbaren Items und Ausgängen
        /// </summary>
        public void ShowDescription()
        {
            TextBuffer.WriteLine(Text);
            TextBuffer.WriteLine("=".PadRight(Console.WindowWidth, '=') + "\n");
            TextBuffer.WriteLine("Verfügbare Items:");

            foreach (Item item in items)
                TextBuffer.WriteLine(item.Name);

            TextBuffer.WriteLine("\nVerfügbare Richtungen:");

            foreach (string exit in exits)
                TextBuffer.WriteLine(exit);

            TextBuffer.ShowBuffer();
        }

        /// <summary>
        /// Gibt an, ob der Raum durch den angegebenen Ausgang verlassen werden kann
        /// </summary>
        /// <param name="Direction">Der Ausgang, durch den der Raum verlassen werden soll</param>
        /// <returns></returns>
        public bool CanExit(string Direction) => exits.Contains(Direction);

        /// <summary>
        /// Gibt an, ob der angegebene Ausgang abgeschlossen ist
        /// </summary>
        /// <param name="Exit">Der zu prüfende Ausgang</param>
        /// <returns></returns>
        public bool IsExitLocked(string Exit) => lockedExits.ContainsKey(Exit);

        /// <summary>
        /// Öffnet den angegebenen Ausgang
        /// </summary>
        /// <param name="Exit"></param>
        public void OpenExit(string Exit) => lockedExits.Remove(Exit);

        /// <summary>
        /// Fügt dem Raum ein Item hinzu
        /// </summary>
        /// <param name="Item"></param>
        public void AddItem(Item Item) => items.Add(Item);

        /// <summary>
        /// Entfernt ein Item aus dem Raum
        /// </summary>
        /// <param name="Item"></param>
        public void RemoveItem(Item Item) => items.Remove(Item);

        /// <summary>
        /// Gibt ein im Raum befindliches Item zurück
        /// </summary>
        /// <param name="ItemName"></param>
        /// <returns></returns>
        public Item GetItem(string ItemName) => items.Find(x => string.Equals(x.Name, ItemName, StringComparison.CurrentCultureIgnoreCase));
    }
}
