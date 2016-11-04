using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleEscape
{
    /// <summary>
    /// Klasse zur Verwaltung des Inventars des Spielers. Implementiert IEnurable zum Durchlaufen der Items, die sich im Inventar befinden
    /// </summary>
    class Inventory : IEnumerable
    {
        List<Item> items;

        /// <summary>
        /// Gibt die Größe des Inventars zurück
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Gibt an, wie viele Items sich im Inventar befinden
        /// </summary>
        public int ItemCount => items.Count;

        /// <summary>
        /// Gibt an, wie viel Platz noch im Inventar ist
        /// </summary>
        public int AvailableSize => Size - ItemCount;

        /// <summary>
        /// Gibt an, ob das Inventar voll ist
        /// </summary>
        public bool IsFull { get; private set; }

        /// <summary>
        /// Gibt an, ob das Inventar leer ist
        /// </summary>
        public bool IsEmpty { get; private set; } = true;

        /// <summary>
        /// Initialisiert die Klasse
        /// </summary>
        /// <param name="Size">Gibt die Startgröße den Inventars an</param>
        public Inventory(int Size)
        {
            this.Size = Size;
            items = new List<Item>();
        }

        /// <summary>
        /// Fügt dem Inventar ein Item hinzu
        /// </summary>
        /// <param name="Item">Das Item, das hinzugefügt werden soll</param>
        public void Add(Item Item)
        {
            if (!IsFull)
            {
                items.Add(Item);
                IsEmpty = false;
            }

            if (items.Count == Size)
                IsFull = true;
        }

        /// <summary>
        /// Entfernt ein Item
        /// </summary>
        /// <param name="Item">Das Item, das entfernt werden soll</param>
        public void Remove(Item Item)
        {
            if (items.Count > 0)
                items.Remove(Item);

            if (IsFull)
                IsFull = false;

            if (items.Count == 0)
                IsEmpty = true;
        }

        /// <summary>
        /// Leert das gesamte Inventar
        /// </summary>
        public void Clear() 
        {
            items.Clear();

            if (IsFull)
                IsFull = false;
        }

        /// <summary>
        /// Gibt an, ob das Item im Inventar vorhanden ist
        /// </summary>
        /// <param name="Item">Das zu überprüfende Item</param>
        /// <returns></returns>
        public bool Contains(Item Item) => items.Contains(Item);

        /// <summary>
        /// Gibt ein im Inventar befindliches Item zurück
        /// </summary>
        /// <param name="ID">Die ID des Items, das gesucht werden soll</param>
        /// <returns></returns>
        public Item GetItem(string ID) => items.Find(x => x.ID == ID);

        /// <summary>
        /// Durchläuft das Inventar und gibt die Items zurück
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator() => items.GetEnumerator();
    }
}
