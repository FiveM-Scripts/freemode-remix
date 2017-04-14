using CitizenFX.Core;
using CitizenFX.Core.UI;
using Freeroam.Utils;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Freeroam.Holders
{
    class Level : BaseScript
    {
        private Text drawingText;

        private int xp;
        private int lvl;

        public Level()
        {
            xp = Storage.GetInt(Storage.XP);
            if (xp == 0) xp = 1;

            lvl = (int)Math.Ceiling((double)xp / 50);

            PointF drawingTextPos = new PointF(220f, 580f);
            float drawingTextScale = 0.6f;
            Color drawingTextColor = Color.FromArgb(255, 102, 178, 255);
            Font drawingTextFont = Font.ChaletComprimeCologne;
            Alignment drawingTextAlign = Alignment.Left;
            drawingText = new Text($"{Strings.LEVEL} {lvl}", drawingTextPos, drawingTextScale, drawingTextColor, drawingTextFont, drawingTextAlign, true, true);

            EventHandlers[Events.XP_ADD] += new Action<int>(AddXP);
            EventHandlers[Events.XP_GET] += new Action<Action<int>>(GetXP);

            Tick += OnTick;
        }

        private void AddXP(int amount)
        {
            SetXP(xp + amount);
        }

        private void SetXP(int newXP)
        {
            xp = newXP;

            int prevLvl = lvl;
            lvl = (int)Math.Ceiling((double)xp / 50);
            if (lvl > prevLvl) Screen.ShowNotification("~b~" + Strings.LEVEL_UP);

            drawingText.Caption = $"{Strings.LEVEL} {lvl}";

            Storage.SetInt(Storage.XP, xp);
        }

        private void GetXP(Action<int> cb)
        {
            cb(xp);
        }

        private async Task OnTick()
        {
            drawingText.Draw();

            await Task.FromResult(0);
        }
    }
}
