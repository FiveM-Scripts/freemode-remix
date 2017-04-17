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
        public const string PNAME_LEVEL = "_PLAYER_LEVEL";

        public static int XP { get; private set; }
        public static int LVL { get; private set; }

        public Level()
        {
            XP = Storage.GetInt(Storage.XP);
            if (XP == 0) XP = 1;
            LVL = (int)Math.Ceiling((double)XP / 50);

            EventHandlers[Events.XP_ADD] += new Action<int>(AddXP);

            EntityDecoration.RegisterProperty(PNAME_LEVEL, DecorationType.Int);

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

        public static int GetPlayerLVL(Player player)
        {
            Ped playerPed = player.Character;
            if (playerPed == null || !EntityDecoration.ExistOn(playerPed, PNAME_LEVEL)) return 0;
            else return EntityDecoration.Get<int>(playerPed, PNAME_LEVEL);
        }

        private async Task OnTick()
        {
            DrawLevelText();

            if (Game.PlayerPed != null) EntityDecoration.Set(Game.PlayerPed, PNAME_LEVEL, LVL);

            await Task.FromResult(0);
        }

        private void DrawLevelText()
        {
            UIResText levelText = new UIResText($"{Strings.LEVEL} {LVL}", new PointF(320f, 863f), 0.6f, Color.FromArgb(255, 102, 178, 255),
                Font.ChaletComprimeCologne, UIResText.Alignment.Left);
            levelText.DropShadow = true;
            levelText.Draw();
        }
    }
}
