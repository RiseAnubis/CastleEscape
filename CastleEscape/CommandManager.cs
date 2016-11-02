using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleEscape
{
    /// <summary>
    /// Verarbeitet die eingegebenen Befehle
    /// </summary>
    static class CommandManager
    {
        /// <summary>
        /// Liest Eingaben des Spielers ein und verarbeitet diese
        /// </summary>
        public static void ReadCommand()
        {
            Console.Write("\nWas soll ich machen?\n>");
            string[] args = Console.ReadLine().ToLower().Trim().Split(' ');

            switch (args[0])
            {
                case "hilfe":
                    if (args.Length == 1)
                        GameManager.GoToScreen(GameScreens.HelpList);
                    /*else
                        Item.ShowInfo(GameManager.ItemList.Find(x => x.Name == args[1]));*/
                    break;
                case "nimm":
                    if (args.Length > 1)
                        Player.TakeItem(args[1]);
                    else
                    {
                        TextBuffer.WriteLine("Du musst ein Item angeben!");
                        TextBuffer.ShowBuffer();
                    }
                    break;
                case "ablegen":
                    if (args.Length > 1)
                        Player.DropItem(args[1]);
                    else
                    {
                        TextBuffer.WriteLine("Du musst ein Item angeben!");
                        TextBuffer.ShowBuffer();
                    }
                    break;
                case "gehe":
                    if (args.Length > 1)
                        Player.MoveToDirection(args[1]);
                    else
                    {
                        TextBuffer.WriteLine("Du musst eine Richtung angeben!");
                        TextBuffer.ShowBuffer();
                    }
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
                case "öffnen":
                    if (args.Length > 1)
                        Player.OpenExit(args[1]);
                    else
                    {
                        TextBuffer.WriteLine("Du musst eine Richtung angeben!");
                        TextBuffer.ShowBuffer();
                    }
                    break;
                case "benutzen":
                    break;
                default:
                    GameManager.GoToScreen(GameScreens.CommandNotFound);
                    break;
            }
        }
    }
}
