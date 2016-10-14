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
        /// Gibt an, ob das Inventar voll ist
        /// </summary>
        public bool IsFull { get; private set; }

        /// <summary>
        /// Initialisiert die Klasse
        /// </summary>
        /// <param name="Size">Gibt die Startgröße den Inventars an</param>
        public Inventory(int Size)
        {
            this.Size = Size;
            items = new List<Item>();
        }

        public void Add(Item Item)
        {
            if (!IsFull)
                items.Add(Item);

            if (items.Count == Size)
                IsFull = true;
        }

        public void Remove(Item Item)
        {
            if (items.Count > 0)
                items.Remove(Item);

            if (IsFull)
                IsFull = false;
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
        /// Durchläuft das Inventar und gibt die Items zurück
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator() => items.GetEnumerator();
    }
}
