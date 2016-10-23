using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleEscape
{
    /// <summary>
    /// Klasse zur grundlegenden Verwaltung des Spiels
    /// </summary>
    static class GameManager
    {
        public static Item KeyGreenRoom = new Item("Schlüssel", "Ein Schlüssel, der den grünen Raum öffnet");

        /// <summary>
        /// Gibt an, ob das Spiel beendet werden soll
        /// </summary>
        public static bool CanQuit { get; set; }

        public static void GoToScreen(GameScreens Screen)
        {
            switch (Screen)
            {
                case GameScreens.CharacterCreation:
                    CreatePlayer();
                    break;
                case GameScreens.CommandNotFound:
                    ShowCommandNotFound();
                    break;
                case GameScreens.HelpList:
                    ShowHelpList();
                    break;
            }
        }

        static void CreatePlayer()
        {
            /*Console.Clear();

            do
            {
                TextBuffer.WriteLine("Gib einen Namen für deinen Spieler ein: ");
                Player.Name = Console.ReadLine();
                TextBuffer.WriteLine("Dein Spieler heißt " + Player.Name + ", möchtest du fortfahren? (ja/nein)");
            }
            while (!CommandManager.ReadCommand());*/
        }

        static void ShowCommandNotFound()
        {
            Console.Clear();
            Console.WriteLine("Befehl wurde nicht erkannt. Gib <Hilfe> ein, um eine Liste aller Befehle zu erhalten.");
        }

        /// <summary>
        /// Zeigt eine Liste mit allen verfügbaren Befehlen an
        /// </summary>
        static void ShowHelpList()
        {
            Console.Clear();
            Console.WriteLine("Verfügbare Befehle");
            Console.WriteLine("--------------------------------\n");
            Console.WriteLine("Nimm <Item>\t\tNimmt das Item auf, wenn genug Platz im Inventar ist.");
            Console.WriteLine("Ablegen <Item>\t\tLegt das Item ab.");
            Console.WriteLine("Hilfe <Item>\t\tRuft eine Beschreibung des eingegeben Items ab, wenn es sich im Inventar befindet.");
            Console.WriteLine("Gehe <Richtung>\t\tGeht in den angegebenen Raum (Nord, Süd, Ost, West)");
            Console.WriteLine("Öffnen <Richtung>\t\tÖffnet den Weg zur angegebenen Richtung, wenn das entsprechende Item im Inventar ist.");
            Console.WriteLine("Umsehen\t\tZeigt den aktuellen Raum an.");
            Console.WriteLine("Beenden\t\tBeendet das Spiel");
        }
    }

    /// <summary>
    /// Aufzählung, die bestimmte Bildschirme anzeigt
    /// </summary>
    enum GameScreens
    {
        CharacterCreation,
        CommandNotFound,
        HelpList
    }
}
