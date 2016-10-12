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
            "hilfe",
            "nimm",
            "gehe",
            "ja",
            "nein",
            "zurück",
            "inventar",
            "benutzen",
            "ablegen",
            "raum"
        };

        public static bool ReadCommand(/*ActionDelegate Action*/)
        {
            string[] args = Console.ReadLine().ToLower().Split(' ');

            switch (args[0])
            {
                case "hilfe":
                    if (args.Length == 1)
                        GameManager.GoToScreen(GameScreens.HelpList);
                    else
                    {
                        Item.ShowItemInfo(GameManager.ItemList.Find(x => x.Name == args[1]));
                    }
                    break;
                case "ja":
                    return true;
                case "nein":
                    return false;
                case "nimm":
                    //Player.TakeItem();
                    break;
                default:
                    GameManager.GoToScreen(GameScreens.CommandNotFound);
                    break;
            }

            return false;
        }

        static void ExecuteCommand(string Command, string[] Arguments)
        {
            
        }
    }
}
