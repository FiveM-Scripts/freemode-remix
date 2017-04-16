using CitizenFX.Core;
using CitizenFX.Core.UI;
using Freeroam.Utils;
using NativeUI;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Freeroam.Holders
{
    class Level : BaseScript
    {
        public static int XP { get; private set; }
        public static int LVL { get; private set; }

        public Level()
        {
            XP = Storage.GetInt(Storage.XP);
            if (XP == 0) XP = 1;

            LVL = (int)Math.Ceiling((double)XP / 50);

            EventHandlers[Events.XP_ADD] += new Action<int>(AddXP);

            Tick += OnTick;
        }

        private void AddXP(int amount)
        {
            SetXP(XP + amount);
        }

        private void SetXP(int newXP)
        {
            XP = newXP;

            int prevLvl = LVL;
            LVL = (int)Math.Ceiling((double)XP / 50);
            if (LVL > prevLvl) Screen.ShowNotification("~b~" + Strings.LEVEL_UP);

            Storage.SetInt(Storage.XP, XP);
        }

        private async Task OnTick()
        {
            DrawLevelText();

            await Task.FromResult(0);
        }

        private void DrawLevelText()
        {
            UIResText levelText = new UIResText($"{Strings.LEVEL} {LVL}", new PointF(220f, 580f), 0.6f, Color.FromArgb(255, 102, 178, 255),
                Font.ChaletComprimeCologne, UIResText.Alignment.Left);
            levelText.Draw();
        }
    }
}
