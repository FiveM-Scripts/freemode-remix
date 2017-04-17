using CitizenFX.Core;
using Freeroam.Holders;
using System.Collections.Generic;

namespace Freeroam.Utils
{
    internal struct CachedPlayer
    {
        public Player PLAYER { get; private set; }
        public int LEVEL { get; private set; }
        public int MONEY { get; private set; }

        public CachedPlayer(Player player, int lvl, int money)
        {
            PLAYER = player;
            LEVEL = lvl;
            MONEY = money;
        }
    }

    class CachedPlayers : BaseScript
    {
        private const int TIMEUNTILUPDATE = 20;

        public static List<CachedPlayer> PLAYERS { get; private set; }
        private int timeUntilUpdate;

        public CachedPlayers()
        {
            timeUntilUpdate--;
            if (timeUntilUpdate <= 0)
            {
                UpdatePlayers();
                timeUntilUpdate = TIMEUNTILUPDATE;
            }
        }

        private void UpdatePlayers()
        {
            PLAYERS = new List<CachedPlayer>();
            foreach (Player player in Players)
            {
                int playerLVL = Level.GetPlayerLVL(player);
                int playerMoney = Money.GetPlayerMoney(player);
                CachedPlayer cachedPlayer = new CachedPlayer(player, playerLVL, playerMoney);
                PLAYERS.Add(cachedPlayer);
            }
        }
    }
}
