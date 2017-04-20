using CitizenFX.Core;
using Freeroam.Utils;
using System.Threading.Tasks;

namespace Freeroam.Crimes
{
    class WantedLevelEscape : BaseScript
    {
        private int prevWantedLvl;

        public WantedLevelEscape()
        {
            Tick += OnTick;
        }

        private async Task OnTick()
        {
            Ped playerPed = Game.PlayerPed;
            if (playerPed != null)
            {
                if (playerPed.IsDead) prevWantedLvl = 0;
                else
                {
                    if (Game.Player.WantedLevel > 2) prevWantedLvl = Game.Player.WantedLevel;
                    else if (Game.Player.WantedLevel == 0 && prevWantedLvl > 0)
                    {
                        TriggerEvent(Events.MONEY_ADD, 20 * prevWantedLvl);
                        TriggerEvent(Events.XP_ADD, 3 * prevWantedLvl);
                        prevWantedLvl = 0;
                    }
                }
            }

            await Task.FromResult(0);
        }
    }
}
