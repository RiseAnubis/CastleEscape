using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleEscape
{
    /// <summary>
    /// Stellt ein Item dar
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Der Name des Items
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Die Beschreibung des Items
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Erstellt ein neues Item
        /// </summary>
        /// <param name="Name">Der Name des Items</param>
        /// <param name="Description">Die Beschreibung des Items</param>
        public Item(string Name, string Description)
        {
            this.Name = Name;
            this.Description = Description;
        }
    }
}
