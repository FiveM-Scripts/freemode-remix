using CitizenFX.Core;
using Freeroam.Holders;
using Freeroam.Utils;
using NativeUI;
using System.Drawing;
using System.Threading.Tasks;

namespace Freeroam
{
    class Scoreboard : BaseScript
    {
        private const float POS_X = 1500f;
        private const float POS_Y = 50f;
        private const float SCOREBOARD_LENGTH = 400f;

        private const float TITLE_HEIGHT = 35f;
        private const float PLAYERITEM_HEIGHT = 50f;

        private bool drawScoreboard = false;

        public Scoreboard()
        {
            Tick += OnTick;
        }

        private async Task OnTick()
        {
            if (Game.IsControlJustReleased(1, Control.MultiplayerInfo))
            {
                drawScoreboard = !drawScoreboard;
            }

            if (drawScoreboard)
            {
                DrawScoreboard();
            }

            await Task.FromResult(0);
        }

        private void DrawScoreboard()
        {
            DrawTitle();
            DrawPlayerItems();
        }

        private void DrawTitle()
        {
            UIResRectangle titleRec = new UIResRectangle(new PointF(POS_X, POS_Y), new SizeF(SCOREBOARD_LENGTH, TITLE_HEIGHT), Color.FromArgb(220, 0, 0, 0));
            titleRec.Draw();

            int playerAmount = 0;
            foreach (Player player in Players) playerAmount++;
            UIResText titleText = new UIResText($"{playerAmount} Players", new PointF(POS_X + 5f, POS_Y + 2f), 0.4f);
            titleText.Draw();
        }

        private void DrawPlayerItems()
        {
            float nextPosY = POS_Y + TITLE_HEIGHT;
            foreach (CachedPlayer cachedPlayer in CachedPlayers.PLAYERS)
            {
                Player player = cachedPlayer.PLAYER;
                DrawPlayerRec(player, POS_X, nextPosY);
                DrawPlayerName(player, POS_X + 5f, nextPosY + 7f);
                DrawPlayerLevel(player, POS_X + 290f, nextPosY + 7f);

                nextPosY += PLAYERITEM_HEIGHT;
            }
        }

        private void DrawPlayerRec(Player player, float x, float y)
        {
            int r, g, b;
            Util.GetPlayerRGBColor(player, out r, out g, out b);
            UIResRectangle playerRec = new UIResRectangle(new PointF(x, y), new SizeF(SCOREBOARD_LENGTH, PLAYERITEM_HEIGHT), Color.FromArgb(120, r, g, b));
            playerRec.Draw();
        }

        private void DrawPlayerName(Player player, float x, float y)
        {
            UIResText playerNameText = new UIResText(player.Name, new PointF(x, y), 0.45f);
            playerNameText.DropShadow = true;
            playerNameText.Draw();
        }

        private void DrawPlayerLevel(Player player, float x, float y)
        {
            int playerLVL = Level.GetPlayerLVL(player);
            UIResText playerNameText = new UIResText($"LVL {playerLVL}", new PointF(x, y), 0.45f, Color.FromArgb(255, 102, 178, 255));
            playerNameText.DropShadow = true;
            playerNameText.Draw();
        }
    }
}
