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
            "inventar",
            "benutzen",
            "ablegen",
            "umsehen"
        };

        public static bool ReadCommand(/*ActionDelegate Action*/)
        {
            Console.Write("\nWas soll ich machen?\n>");
            string[] args = Console.ReadLine().ToLower().Split(' ');

            switch (args[0])
            {
                case "hilfe":
                    if (args.Length == 1)
                        GameManager.GoToScreen(GameScreens.HelpList);
                    /*else
                        Item.ShowInfo(GameManager.ItemList.Find(x => x.Name == args[1]));*/
                    break;
                case "ja":
                    return true;
                case "nein":
                    return false;
                case "nimm":
                    Player.TakeItem(args[1]);
                    break;
                case "ablegen":
                    Player.DropItem(args[1]);
                    break;
                case "gehe":
                    Player.MoveToDirection(args[1]);
                    break;
                case "beenden":
                    GameManager.CanQuit = true;
                    break;
                case "inventar":
                    Player.ShowInventory();
                    break;
                case "umsehen":
                    Player.CurrentRoom.ShowDescription();
                    break;
                default:
                    GameManager.GoToScreen(GameScreens.CommandNotFound);
                    break;
            }

            return false;
        }
    }
}
