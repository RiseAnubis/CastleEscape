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
    class Item
    {
        /// <summary>
        /// Die ID zur Identifizierung
        /// </summary>
        public string ID { get; }

        /// <summary>
        /// Der Name des Items
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Die Beschreibung des Items
        /// </summary>
        public string HelpDescription { get; }

        /// <summary>
        /// Erstellt ein neues Item
        /// </summary>
        /// <param name="ID">Die ID zur Identifizierung</param>
        /// <param name="Name">Der Name des Items</param>
        /// <param name="Description">Die Beschreibung des Items</param>
        public Item(string ID, string Name, string Description)
        {
            this.ID = ID;
            this.Name = Name;
            HelpDescription = Description;
        }
    }
}
