using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleEscape
{
    enum GameScreens
    {
        CharacterCreation,
        StartRoom,
        CommandNotFound,
        HelpList
    }

    /// <summary>
    /// Klasse zur grundlegenden Verwaltung des Spiels
    /// </summary>
    static class GameManager
    {
        public static List<Item> ItemList { get; } = new List<Item>
        {
            new Item("Schlüssel", "Ein Schlüssel, der eine Tür im Schloss öffnet"),
            new Item("Schwert", "Eine Waffe, die zum Verteidigen vor Monstern schützt"),
        };

        public static List<Room> RoomList { get; } = new List<Room>
        {
            new Room { }
        };

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
                case GameScreens.StartRoom:
                    LoadStartRoom();
                    break;
            }
        }

        static void CreatePlayer()
        {
            Console.Clear();

            do
            {
                TextBuffer.WriteLine("Gib einen Namen für deinen Spieler ein: ");
                Player.Name = Console.ReadLine();
                TextBuffer.WriteLine("Dein Spieler heißt " + Player.Name + ", möchtest du fortfahren? (ja/nein)");
            }
            while (!CommandManager.ReadCommand());
        }

        static void LoadStartRoom()
        {
            Room startRoom = new Room();
            TextBuffer.WriteLine("Du wachst in einer Zelle auf. Die Zellentür ist geöffnet. Im Raum siehst du 2 Türen.");
        }

        static void ShowCommandNotFound()
        {
            Console.Clear();
            Console.WriteLine("Befehl wurde nicht erkannt. Gib <Hilfe> ein, um eine Liste aller Befehle zu erhalten oder <Zurück>, um zum vorhergehenden Bildschirm zu wechseln.");
            CommandManager.ReadCommand();
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
            Console.WriteLine("Gehe <Richtung>\t\tGeht in den angegebenen Raum (Nord, Süd, Ost, West)");
            Console.WriteLine("Zurück\t\tGeht zum vorherigen Bildschirm zurück.");
            Console.WriteLine("Hilfe <Item>\t\tRuft eine Beschreibung des eingegeben Items ab, wenn es sich im Inventar befindet.");
            Console.WriteLine("Beenden\t\tBeendet das Spiel");
            CommandManager.ReadCommand();
        }
    }
}
