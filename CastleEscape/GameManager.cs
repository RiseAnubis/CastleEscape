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
        StartRoom
    }

    static class GameManager
    {
        public static void GoToScreen(GameScreens Screen)
        {
            switch (Screen)
            {
                case GameScreens.CharacterCreation:
                    CreatePlayer();
                    break;
            }
        }

        static void CreatePlayer()
        {
            Console.WriteLine("Gib einen Namen für deinen Spieler ein: ");
            Player.Name = Console.ReadLine();
            Console.WriteLine("Dein Spieler heißt " + Player.Name + ", möchtest du fortfahren?");
            CommandManager.ReadCommand();
        }

        static void LoadStartRoom()
        {
            
        }
    }
}
