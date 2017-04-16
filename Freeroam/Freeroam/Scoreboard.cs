using CitizenFX.Core;
using NativeUI;
using System.Drawing;
using System.Threading.Tasks;

namespace Freeroam
{
    class Scoreboard : BaseScript
    {
        private static float POS_X = 1200f;
        private static float POS_Y = 50f;
        private static float SCOREBOARD_LENGTH = 500f;

        private static float TITLE_HEIGHT = 50f;
        private static float PLAYERITEM_HEIGHT = 75f;

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
            UIResText titleText = new UIResText($"{playerAmount} Players", new PointF(POS_X + 5f, POS_Y + 5f), 0.5f);
            titleText.Draw();
        }

        private void DrawPlayerItems()
        {
            float nextPosY = POS_Y + TITLE_HEIGHT;
            foreach (Player player in Players)
            {
                UIResRectangle playerRec = new UIResRectangle(new PointF(POS_X, nextPosY), new SizeF(SCOREBOARD_LENGTH, PLAYERITEM_HEIGHT), Color.FromArgb(120, 102, 204, 255));
                playerRec.Draw();

                nextPosY += PLAYERITEM_HEIGHT;
            }
        }
    }
}
