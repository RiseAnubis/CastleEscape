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

        public int Size { get; }
        public bool IsFull { get; private set; }

        public Inventory(int Size)
        {
            this.Size = Size;
            items = new List<Item>();
        }

        public void Add(Item Item)
        {
            items.Add(Item);

            if (items.Count == Size)
                IsFull = true;
        }

        public void Remove(Item Item)
        {
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
