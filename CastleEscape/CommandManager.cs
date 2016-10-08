using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleEscape
{
    static class CommandManager
    {
        static List<string> commands = new List<string>
        {
            "Hilfe",
            "Nimm",
            "Gehe",
            "Ja",
            "Nein"
        };

        public static void ReadCommand()
        {

            string[] args = Console.ReadLine().Split(' ');

            if (commands.Contains(args[0]))
            {
                if (args.Length > 1)
                {

                }
            }
            else
            {
                Console.WriteLine("Befehl wurde nicht erkannt. Gib <Hilfe> ein, um eine Liste aller Befehle zu erhalten.");
                Console.ReadLine();
            }
        }
    }
}
